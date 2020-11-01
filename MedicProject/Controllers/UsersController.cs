using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        public UsersController(DatabaseContext context,ITokenService tokenService){
            _context=context;
            _tokenService=tokenService;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<IEnumerable>> getAllUsers(){
            return await _context.USERS.ToListAsync();
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO RegisterDTO){
            if ( await UserExists(RegisterDTO.email)) return BadRequest("There is allready an existing account with this email");
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
                doctorId=RegisterDTO.doctorId
            };
            //daca doctorid nu e un medic? 
            //daca cineva introduce un email random? poate ar trebui trimis email pt a-l valida ?
            _context.USERS.Add(user);
            await _context.SaveChangesAsync();
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
              
              if(user.isApproved==0) 
              return Unauthorized("Contul dumneavoastra nu a fost inca aprobat de catre medicul ales.");
              
               return new UserDTO
            {
                email=user.email,
                firstName=user.firstName,
                lastName=user.lastName,
                role=user.isMedic,
                token=_tokenService.CreateToken(user)
            };
        }
}
}