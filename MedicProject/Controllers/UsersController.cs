using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        
         private readonly IWebHostEnvironment _webHostEnvironment;
       
       //inject dependencies
        public UsersController(DatabaseContext context,ITokenService tokenService,IMapper mapper,IWebHostEnvironment hostEnvironment){
            _context=context;
            _tokenService=tokenService;
            _mapper = mapper;
            _webHostEnvironment = hostEnvironment;
        }

       
      
      
        [Authorize]
        [HttpGet]
        [Route("getPatients")]
        // return all patients for a medic
        public async Task<ActionResult<IEnumerable<PatientDTO>>> getPatients(){

           //get the current logged in user info
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
           
           //verify if the user is medic
            if(user.isMedic==1)
            {
                //return all the patients of the loged in medic
                var users = await _context.users.Where(p => p.doctorId == user.Id).ToListAsync();
                var patients = _mapper.Map<IEnumerable<PatientDTO>>(users);
                return Ok(patients);
            }
            //if the patient is not medic he is not authorized 
            else return Unauthorized();
        }

      
      
      
      
      
        [AllowAnonymous]
        [HttpGet]
        [Route("getDoctors")]
        //returns all doctors from database
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> getDoctors()
        {
            var users = await _context.users.Where(d => d.isMedic == 1).ToListAsync();
            var doctors = _mapper.Map<IEnumerable<DoctorDTO>>(users);
            return Ok(doctors);
        }

      
       
       
       
       
       
        [Authorize]
        [HttpGet]
        [Route("MyAccount")]
        //returns one specific user from the database (for user Myaccount and fordoctors to see the selected pacient's data)
        public async Task<ActionResult<AccountDTO>> getUser()
        {
             //get the current loged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Include(p => p.doctor).Where(p => p.email==useremail).FirstAsync();
        
                //verify if the user is a patient
            if(user.isMedic==0)
                return Ok(_mapper.Map<AccountDTO>(user));
        
            //if the user is not a patient he is unauthorized
            else return Unauthorized();
        }

        
     
     
     
     
     
        [Authorize]
        [HttpGet]
        [Route("MyAccountMedic")]
        //returns a doctor's data (for doctor Myaccount) (the data includes photo and description)
        public async Task<ActionResult<DoctorDTO>> getDoctor()
        {
            //get the currentloged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
        
            //verify if the user is a doctor
            return  Ok(_mapper.Map<DoctorDTO>(user));
        }

      
      
      
      
      
        [Authorize]
        [HttpGet]
        [Route("getPatientInfo")]
        //returns one specific user from the database (more info page about the user)
        public async Task<ActionResult<PatientDTO>> getPatientInfo(int id)
        {
            //get the current loged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
        
            //verify if the user is a medic
            if(user.isMedic==1)
            { 
                //get the patient with the requested id only if it is the logged in user's patient
                var patient= await _context.users.Where(x => x.Id==id && x.doctorId==user.Id).FirstOrDefaultAsync();
                return Ok(_mapper.Map<PatientDTO>(patient));
            }
            
            //if the user is not a doctor he is unauthorized
            else return Unauthorized();
        }

        
     
     
     
     
     
     
     
        [AllowAnonymous]
        [HttpGet]
        [Route("getMedicInfo")]
        //returns a doctor's data (for doctor MoreInfo page)
        public async Task<ActionResult<DoctorDTO>> getDoctorInfo()
        {
            //get the doctor with the requested id
             var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
             var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
             var medic = await _context.users.Where(d => d.isMedic == 1 && d.Id==user.doctorId).FirstOrDefaultAsync();
             return  Ok(_mapper.Map<DoctorDTO>(medic));
        }

       
      
      
      
      
      
      
        [Authorize]
        [HttpDelete]
        [Route("DeletePatient")]
        //deletes an user from the database
        public async Task<ActionResult> deletePatient(int id)
        {
           //get the currently logged in user
           var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
           var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
           
            //verify if the user is a docotr
           if(user.isMedic==1)
           {
               //get the user with the requested id who is also patient of the logged in medic
               var patient= await _context.users.Where(x => x.Id==id && x.doctorId==user.Id).FirstOrDefaultAsync();
               _context.users.Remove(patient);
               await _context.SaveChangesAsync();
               return Ok();
            }
           
            //if the user is not a doctor he is unauthorized to delete patients
            else return Unauthorized();
        }

      



        [Authorize]
        [HttpDelete]
        [Route("DeleteAccount")]
        //deletes an user from the database
        public async Task<ActionResult> deleteAccount()
        {
           //get the currently logged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
            _context.users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        
        [Authorize]
        [HttpGet]
        [Route("getApprovedUsers")]
        public async Task<ActionResult<PatientDTO>> getApprovedUsers()
        {

            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();

            if(user.isMedic==1)
            {
                //get the list of approved users of the loged in medic
                var users = await _context.users
                    .Where(p => p.doctorId == user.Id)
                    .Where(p => p.isApproved == 1)
                    .Where(p => p.validated == 1)
                    .Include(p => p.Appointments)
                    .ToListAsync();
                
                var approvedusers = _mapper.Map<IEnumerable<PatientDTO>>(users);
            
                return Ok(approvedusers);
            }
            else return Unauthorized();
        }
        
        
        
        [Authorize]
        [HttpGet]
        [Route("getUnapprovedUsers")]
        //returns all unapproved users of a doctor
        public async Task<ActionResult<IEnumerable<PatientDTO>>> getUnapprovedUsers()
        {
              //get the currently logged in user
               var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
               var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
               
               //verify if the user is a doctor
               if(user.isMedic==1)
               {
                   //get the list of unapproved users of the loged in medic
                   var users = await _context.users.Where(p => p.doctorId == user.Id)
                                .Where(p => p.isApproved == 0)
                                .Where(p => p.validated == 1)
                                .ToListAsync();
                   var unapprovedusers = _mapper.Map<IEnumerable<PatientDTO>>(users);
                   return Ok(unapprovedusers);
               }
               
               //if the user is not a medic then he is unauthorized to see unapproved users
               else return Unauthorized();
        }

       
      
      
      
      
      
        [Authorize]
        [HttpPut]
        [Route("ApproveUser")]
        //approves a selected user
        public async Task<ActionResult> ApproveUser(int id)
        {
            //get the currently logged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
            
            //check if the user is a doctor
            if(user.isMedic==1) 
            {
                //get the requested user
                var patient=await _context.users.FindAsync(id);
                
                //check if the user is a patient of the loged in doctor
                if(patient.doctorId==user.Id)
                {
                    //approve the user's account
                    patient.isApproved=1;
                    _context.users.Update(patient);
                    await  _context.SaveChangesAsync();
                }
                return Ok();
            }
            else return Unauthorized();
        }

      
       
       
       
       
       
       
       
        [Authorize]
        [HttpPut]
        [Route("updateUser")]
        //updates a user (user Myaccount)
        public async Task<ActionResult> UpdateUser(UpdatePatientDTO patientDTO)
        {
            //get the currently logged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
            
            var patient = await _context.users.FirstOrDefaultAsync(u => u.email == patientDTO.email);
            //CHECK IF THE USER IS A PATIENT
            if(user.isMedic==1 && patient != null)
            {
                //update the user
                patient.firstName=patientDTO.firstName;
                patient.lastName=patientDTO.lastName;
                patient.phoneNumber=patientDTO.phoneNumber;
                patient.email=patientDTO.email.ToLower();
                _context.users.Update(patient);
                await _context.SaveChangesAsync();
                return Ok();
            }
          else return Unauthorized();
        }



       
        [Authorize]
        [HttpPut]
        [Route("updateMedic")]
        //updates a doctor (doctor myaccount)
        public async Task<ActionResult> UpdateMedic(UpdateDoctorDTO doctorDTO)
        {
           //get the currently logged in user
           var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
           var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
           
            //CHECK IF THE USER IS A doctor
           if(user.isMedic==1)
           {
               //upload photo
               string uniqueFileName = UploadedFile(doctorDTO); 

               user.firstName=doctorDTO.firstName;
               user.lastName=doctorDTO.lastName;
               user.phoneNumber=doctorDTO.phoneNumber;
               user.email=doctorDTO.email.ToLower();
               user.description=doctorDTO.description;
               if (doctorDTO.photo != null)
               {
                   user.photo=uniqueFileName;
               }  
               _context.users.Update(user);
               await  _context.SaveChangesAsync();
               return Ok();
           }    
           else return Unauthorized();
        }




        private string UploadedFile(UpdateDoctorDTO doctorDTO)  
        {  
            string uniqueFileName = null;  
  
            if (doctorDTO.photo != null)  
            {
                //the path to the iamge folder (if the folder is after webroot)
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");  
                
                //random unique random+ uploaded filename for the filename stored in the app
                uniqueFileName = Guid.NewGuid().ToString() + "_" + doctorDTO.photo.FileName;  
                
                //filePath -the path to the folder where the photo is saved + the filename
                string filePath = Path.Combine(uploadFolder, uniqueFileName);  
                
                //copy the content of the sent file in the filepath 
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    doctorDTO.photo.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
        }  







        
        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> changePassword(ChangePasswordDTO changePasswordDTO)
        {
            //get the currently logged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
            
            //hmac object with the logged in user's salt 
            using var hmac1= new HMACSHA512(user.PasswordSalt);
            
            //compute hash for oldpassword 
            var computedHash = hmac1.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.oldPassword));
            //check if the password matches with the one from database
            for(int i=0;i<computedHash.Length;i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Parola incorecta");
            }
            
            //if the password matches then the password from database is updated with the newPassword
            using var hmac=new HMACSHA512();
            user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDTO.newPassword));
            user.PasswordSalt=hmac.Key;
            _context.users.Update(user);
            await  _context.SaveChangesAsync();
            return Ok();
        }

        
        [Authorize]
        [HttpGet]
        [Route("searchUser")]
        //search for patients of a specific doctor
        public async Task<ActionResult<IEnumerable<PatientDTO>>> searchUser(string str)
        {
            //get the currently logged in user
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var medic = await _context.users.Where(x => x.email==useremail).FirstOrDefaultAsync();
            
            //check if the user is a doctor
            if(medic.isMedic==1)
            {
                var s=str.ToLower();
                var users = await _context.users.Where(p => p.firstName.ToLower().Contains(s) || p.lastName.ToLower().Contains(s) || Convert.ToString(p.firstName.ToLower()) +" "+ Convert.ToString(p.lastName.ToLower()) == s || Convert.ToString(p.lastName.ToLower())+" "+ Convert.ToString(p.firstName.ToLower()) == s).Where(x => x.doctorId== medic.Id).Where(x => x.isMedic==0).ToListAsync();
                var userstoreturn = _mapper.Map<IEnumerable<PatientDTO>>(users);
                return Ok(userstoreturn);
            }
            else return Unauthorized();
        
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("searchDoctor")]
        //search for doctors
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> searchDoctor(string str)
        {
            var s = str.ToLower();
            var users = await _context.users.Where(p => p.firstName.ToLower().Contains(s) || p.lastName.ToLower().Contains(s) || Convert.ToString(p.firstName.ToLower()) +" "+ Convert.ToString(p.lastName.ToLower()) == s || Convert.ToString(p.lastName.ToLower())+" "+ Convert.ToString(p.firstName.ToLower()) == s).Where(x => x.isMedic==1).ToListAsync();
            var userstoreturn = _mapper.Map<IEnumerable<DoctorDTO>>(users);
            return Ok(userstoreturn);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO RegisterDTO)
        {
            if ( await UserExists(RegisterDTO.email)) return BadRequest("There is already an existing account with this email");
            using var hmac=new HMACSHA512();
            
            var user = new User
            {
                firstName=RegisterDTO.firstName,
                lastName=RegisterDTO.lastName,
                email=RegisterDTO.email,
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

            _context.users.Add(user);
            await _context.SaveChangesAsync();
            
            //send verification email
            await EmailVerification(user.email);
            return user;
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.users.AnyAsync(x => x.email == email.ToLower());
        }
      
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO LoginDto)
        {
            var user=await _context.users.SingleOrDefaultAsync(x => x.email==LoginDto.email);
            if(user==null) return Unauthorized("Invalid credentials");

            using var hmac= new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(LoginDto.password));
            for(int i=0;i<computedHash.Length;i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid credentials");
            }
               
            return new UserDTO
            {
                id = user.Id,
                email=user.email,
                firstName=user.firstName,
                lastName=user.lastName,
                isMedic=user.isMedic,
                isApproved = user.isApproved,
                validated = user.validated,
                token=_tokenService.CreateToken(user)
            };
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("ForgotPassword")]
        //sends token to an email 
        public async Task<ActionResult> ForgotPassword(string email)
        {
            string resetCode = Guid.NewGuid().ToString();
            var user = _context.users.Where(u => u.email == email.ToLower()).FirstOrDefault();
            if (await UserExists(email))
            {
                user.Token = resetCode;

               _context.users.Update(user);
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


        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        //resets a user's password if he correctly introduced
        public async Task<ActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var user = await _context.users.Where(a => a.Token == model.resetCode).FirstOrDefaultAsync();
            if (user != null)
            {
                using var hmac=new HMACSHA512();
                user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(model.newPassword));
                user.PasswordSalt=hmac.Key;
        
            
                user.Token = "";
                _context.users.Update(user);
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
           var user = _context.users.Where(u => u.email == email.ToLower()).FirstOrDefault();
           if (await UserExists(email))
           {
               user.Token = resetCode;
               _context.users.Update(user);
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


       [AllowAnonymous]
       [HttpPost]
       [Route("getInfoAboutAccount")]
        public async Task<ActionResult<InfoAccountDTO>> getInfo()
        {
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var account = await _context.users.Where(x => x.email==useremail).FirstOrDefaultAsync();

            var info = _mapper.Map<InfoAccountDTO>(account);

            return info;
        } 





       [AllowAnonymous]
       [HttpPost]
       [Route("VerifyAccount")]
       //user has to introduce the validation string sent to him via email to verify his account and activate it 
       public async Task<ActionResult> VerifyAccount(string token)
       {
           //find the user with the sent unique token
           var user = await _context.users.Where(a => a.Token == token).FirstOrDefaultAsync();
           
            //if a user is found his account is validated, now he can login
           if (user != null)
           {
               user.Token="";
               user.validated=1;
               _context.users.Update(user);
               await  _context.SaveChangesAsync();
           }
           else
           {
                return BadRequest("Token invalid");
           }
           
            var userToReturn = new UserDTO
            {
                id = user.Id,
                email=user.email,
                firstName=user.firstName,
                lastName=user.lastName,
                isMedic=user.isMedic,
                isApproved = user.isApproved,
                validated = user.validated,
                token=_tokenService.CreateToken(user)
            };

            return Ok(userToReturn);
       }

    }
}