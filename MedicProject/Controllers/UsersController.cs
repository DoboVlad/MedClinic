using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedicProject.Data;
using MedicProject.DTO;
using MedicProject.Interfaces;
using MedicProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MedicProject.Controllers
{       
        [Route("api/[controller]")]
        [ApiController]
    public class UsersController:ControllerBase
    {      
        private readonly DatabaseContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public UsersController(DatabaseContext context,ITokenService tokenService,IMapper mapper){
            _context=context;
            _tokenService=tokenService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getPatients")]
        // return all patients for a medic
        public async Task<ActionResult<IEnumerable<PatientDTO>>> getUsers(int idMedic){
            var users = await _context.USERS.Where(p => p.doctorId == idMedic).ToListAsync();
            var patients = _mapper.Map<IEnumerable<PatientDTO>>(users);
            return Ok(patients);
        }

        [HttpGet]
        [Route("getDoctors")]
        //returns all doctors from database
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> getDoctors(){
            var users = await _context.USERS.Where(d => d.isMedic == 1).ToListAsync();
            var doctors = _mapper.Map<IEnumerable<DoctorDTO>>(users);
            return Ok(doctors);
        }

        [HttpGet]
        [Route("getUser")]
        //returns one specific user from the database (for user Myaccount and fordoctors to see the selected pacient's data)
        public async Task<PatientDTO> getUser(int id){
        var user = await _context.USERS.FindAsync(id);
        return  _mapper.Map<PatientDTO>(user);
        }

        [HttpGet]
        [Route("getDoctor")]
        //returns a doctor's data (for doctor Myaccount) (the data includes photo and description)
        public async Task<DoctorDTO> getDoctor(int id){
        var user = await _context.USERS.FindAsync(id);
        return  _mapper.Map<DoctorDTO>(user);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        //deletes an user from the database
        public async Task deleteUser(int id){
            var user=await _context.USERS.FindAsync(id);
            _context.USERS.Remove(user);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        [Route("getUnapprovedUsers")]
        //returns all unapproved users of a doctor
        public async Task<ActionResult<IEnumerable<PatientDTO>>> getUnapprovedUsers(int id){
               var users = await _context.USERS.Where(p => p.doctorId == id).Where(p => p.isApproved == 0).ToListAsync();
               var unapprovedusers = _mapper.Map<IEnumerable<PatientDTO>>(users);
               return Ok(unapprovedusers);
        }

        [HttpPut]
        [Route("ApproveUser")]
        //approves a selected user
        public async Task ApproveUser(int id){
            var user=await _context.USERS.FindAsync(id);
            user.isApproved=1;
            _context.USERS.Update(user);
           await  _context.SaveChangesAsync();

        }

        [HttpPut]
        [Route("updateUser")]
        //updates a user (user Myaccount)
        public async Task UpdateUser(PatientDTO patientDTO){
           // var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
           // var user = await _context.USERS.Where(p => p.email==useremail).FirstAsync();
           var user=await _context.USERS.FindAsync(patientDTO.Id);
           user.firstName=patientDTO.firstName;
           user.lastName=patientDTO.lastName;
           user.phoneNumber=patientDTO.phoneNumber;
           user.email=patientDTO.email.ToLower();
           _context.USERS.Update(user);
          await  _context.SaveChangesAsync();

        }

        [HttpPut]
        [Route("updateMedic")]
        //updates a doctor (doctor myaccount)
        public async Task UpdateMedic(DoctorDTO doctorDTO){
            // var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
           // var user = await _context.USERS.Where(p => p.email==useremail).FirstAsync();
           var user=await _context.USERS.SingleOrDefaultAsync(x=> x.Id == doctorDTO.Id);
           user.firstName=doctorDTO.firstName;
           user.lastName=doctorDTO.lastName;
           user.phoneNumber=doctorDTO.phoneNumber;
           user.email=doctorDTO.email.ToLower();
           user.description=doctorDTO.description;
           user.photo=doctorDTO.photo;
            _context.USERS.Update(user);
          await  _context.SaveChangesAsync();

        }

         [HttpPost]
         [Route("ChangePassword")]
        public async Task<ActionResult> changePassword(ChangePasswordDTO changePasswordDTO){
            // var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
           // var user = await _context.USERS.Where(p => p.email==useremail).FirstAsync();
            var user = await _context.USERS.SingleOrDefaultAsync(x=> x.Id == changePasswordDTO.Id);
            using var hmac1= new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac1.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.oldPassword));
                for(int i=0;i<computedHash.Length;i++){
                    if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Parola incorecta");
                }
            using var hmac=new HMACSHA512();
            user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.newPassword));
            user.PasswordSalt=hmac.Key;
            _context.USERS.Update(user);
            await  _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        [Route("searchUser")]
        //search for patients of a specific doctor
        public async Task<ActionResult<IEnumerable<PatientDTO>>> searchUser(string str,int id){

            var s=str.ToLower();
            var users = await _context.USERS.Where(p => p.firstName.ToLower().Contains(s) || p.lastName.ToLower().Contains(s) || Convert.ToString(p.firstName.ToLower()) +" "+ Convert.ToString(p.lastName.ToLower()) == s || Convert.ToString(p.lastName.ToLower())+" "+ Convert.ToString(p.firstName.ToLower()) == s).Where(x => x.doctorId== id).Where(x => x.isMedic==0).ToListAsync();
            var userstoreturn = _mapper.Map<IEnumerable<PatientDTO>>(users);
            return Ok(userstoreturn);
        
        }



        [HttpGet]
        [Route("searchDoctor")]
        //search for doctors
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> searchDoctor(string str){
            var s = str.ToLower();
        var users = await _context.USERS.Where(p => p.firstName.ToLower().Contains(s) || p.lastName.ToLower().Contains(s) || Convert.ToString(p.firstName.ToLower()) +" "+ Convert.ToString(p.lastName.ToLower()) == s || Convert.ToString(p.lastName.ToLower())+" "+ Convert.ToString(p.firstName.ToLower()) == s).Where(x => x.isMedic==1).ToListAsync();
        var userstoreturn = _mapper.Map<IEnumerable<DoctorDTO>>(users);
        return Ok(userstoreturn);
       }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO RegisterDTO){
            if ( await UserExists(RegisterDTO.email)) return BadRequest("There is already an existing account with this email");
            using var hmac=new HMACSHA512();
            
            var user = new User{
                firstName=RegisterDTO.firstName,
                lastName=RegisterDTO.lastName,
                email=RegisterDTO.email.ToLower(),
                cnp=RegisterDTO.cnp,
                dateOfBirth=RegisterDTO.dateOfBirth,
                phoneNumber=RegisterDTO.phoneNumber,
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(RegisterDTO.password)),
                PasswordSalt=hmac.Key,
                isApproved=0,
                isMedic=0,
                validated=0,
                doctorId=RegisterDTO.doctorId,
                
            };
            _context.USERS.Add(user);
            await _context.SaveChangesAsync();
            //send verification email
            await EmailVerification(user.email);
            return user;

        }
        private async Task<bool> UserExists(string email){
            return await _context.USERS.AnyAsync(x => x.email == email.ToLower());
    }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO LoginDto){
                var user=await _context.USERS.SingleOrDefaultAsync(x => x.email==LoginDto.email);
                if(user==null) return Unauthorized("Invalid credentials");

                using var hmac= new HMACSHA512(user.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(LoginDto.password));
                for(int i=0;i<computedHash.Length;i++){
                    if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid credentials");
                }
               
              
              if(user.isApproved==0 || user.validated==0) 
              return Unauthorized("Your account has not been aproved or validated yet!");
              
               return new UserDTO
            {
                id = user.Id,
                email=user.email,
                firstName=user.firstName,
                lastName=user.lastName,
                role=user.isMedic,
                token=_tokenService.CreateToken(user)
            };
        }

        [HttpGet]
        [Route("ForgotPassword")]
        //sends token to an email 
        public async Task<ActionResult> ForgotPassword(string email)
        {
           

                string resetCode = Guid.NewGuid().ToString();
                var user = _context.USERS.Where(u => u.email == email.ToLower()).FirstOrDefault();
                if (await UserExists(email))
                {
                    user.Token = resetCode;

                   _context.USERS.Update(user);
                    await  _context.SaveChangesAsync();

                    var subject = "Password Reset Request";
                    var body = "Hi " + user.firstName + ", <br/> You recently requested to reset your password for your account. Use the following token to confirm your identity. " +

                         " <br/><br/>"+resetCode+"<br/><br/>" +
                         "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";

                    SendEmail(user.email, body, subject);  
                }
                else
                {
                    
                    return BadRequest("User doesn't exists.");
                }
            

              return Ok("Reset password link has been sent to your email id.");
        }

        private void SendEmail(string emailAddress, string body, string subject)
        {
            MailMessage mm = new MailMessage("medclinic121@gmail.com", emailAddress);
            
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("medclinic121@gmail.com", "parolamedclinic");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            
        }

        
        [HttpPost]
        [Route("ResetPassword")]
        //resets a user's password if he correctly introduced
          public async Task<ActionResult> ResetPassword(ResetPasswordDTO model)
        {
                    var user = await _context.USERS.Where(a => a.Token == model.resetCode).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        using var hmac=new HMACSHA512();
                         user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(model.newPassword));
                         user.PasswordSalt=hmac.Key;
                    
                        
                         user.Token = "";
                        _context.USERS.Update(user);
                        await  _context.SaveChangesAsync();
                    }
                
            
            else
            {
                return BadRequest("Token invalid");
            }
          
            return Ok();
        }
        //sends verification token to an email
       public async Task<ActionResult> EmailVerification(string email)
        {
           
                string resetCode = Guid.NewGuid().ToString();
                var user = _context.USERS.Where(u => u.email == email.ToLower()).FirstOrDefault();
                if (await UserExists(email))
                {
                     user.Token = resetCode;

                   _context.USERS.Update(user);
                    await  _context.SaveChangesAsync();
                    var subject = "Email verification";
                    var body = "Hi " + user.firstName + ", <br/> You recently registered on our website. Use the following token to verify and activate your account. " +

                         " <br/><br/>"+resetCode+"<br/><br/>" +
                         "If you did registered on our website, please ignore this email or reply to let us know.<br/><br/> Thank you";

                    SendEmail(user.email, body, subject);  
                }
                else
                {
                    
                    return BadRequest("User doesn't exist.");
                }
            

              return Ok("Verification code has been sent to your email");
        }

       [HttpPost]
       [Route("VerifyAccount")]
        //user has to introduce the validation string sent to him via email to verify his account and activate it 
        public async Task<ActionResult> VerifyAccount(string token){
            //find the user with the sent unique token
             var user = await _context.USERS.Where(a => a.Token == token).FirstOrDefaultAsync();
              //if a user is found his account is validated, now he can login
              if (user != null)
              {
                  user.Token="";
                  user.validated=1;
                  _context.USERS.Update(user);
                  await  _context.SaveChangesAsync();

              }
               else
            {
                return BadRequest("Token invalid");
            }
          
            return Ok("Your account was verified");
        }



}
}