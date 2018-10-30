using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientCoreApi.Models;
using PatientCoreApi.Data;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace PatientCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase

    {
        public class Credentials
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }
        //public class PatientRegistration
        //{
        //    public string  FirstName { get; set; }
        //    public string LastName { get; set; }
        //    public string Mobile { get; set; }
        //    public string Email { get; set; }
        //    public string Password { get; set; }
        //    public string Address { get; set; }





        //}



        //readonly PatientDbContext patientDbContext;
        //readonly UserDbContext   userDbContext;
         readonly UserManager<IdentityUser> userManager;
         readonly SignInManager<IdentityUser> signInManager;
        //PatientDbContext patientDbContext;
        //UserDbContext userDbContext;


        public AccountController( UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] Credentials cred)
        {

           

            var user = new IdentityUser
            {
                UserName = cred.Email,
                Email=cred.Email


            };
          var result= await userManager.CreateAsync(user, cred.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

                 await signInManager.SignInAsync(user, isPersistent: false);

               
                 var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the my secret phrase"));
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(signingCredentials: signingCredentials);
                return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));          
       

        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Credentials credentials)
        {
            var result = await signInManager.PasswordSignInAsync(credentials.Email, credentials.Password,false,false);
            if (!result.Succeeded)
                return BadRequest();

            var user = await userManager.FindByEmailAsync(credentials.Email);
            return Ok( CreateToken(user));
        }

        string CreateToken(IdentityUser user)
        {
            var claims = new Claim[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
           };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}