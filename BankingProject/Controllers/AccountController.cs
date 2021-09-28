using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingProject.Models;

namespace BankingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        BankingProjectContext db;
        public AccountController(BankingProjectContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.UserProfile);
        }

        [HttpPost("OpenNewAccount")]
        public IActionResult OpenAccount(UserProfile user)
        {
            var existUser = db.UserProfile.Find(user.EmailId);
            if (existUser == null)
            {
                db.UserProfile.Add(user);
                db.SaveChanges();
                return Ok(new { status = "registered" });
            }
            else
            {
                return Ok(new { status = "already exist" });
            }
        }
        [HttpPost("login")]
        public IActionResult Login(Account user)
        {
            Account user1 = db.Account.Where(x => x.CustomerId == user.CustomerId && x.Password == user.Password).FirstOrDefault();
            if (user1 == null)
            {
                return BadRequest();

            }
            else
            {
                return Ok();
            }
        }
        [HttpPost("register")]
        public IActionResult Register(Account user)
        {
            Account user1 = db.Account.Where(x => x.AccountNumber == user.AccountNumber).FirstOrDefault();
            if (user1 == null)
            {
                db.Account.Add(user);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult getall()
        {
            return Ok(db.Account);
        }
        /*
                [HttpPost("Login")]
                public IActionResult Login(UserProfile user)
                {
                    var existUser = db.UserProfile.Where(userOb => (userOb.Email == user.Emai) && (userOb.Pass == user.Pass)).FirstOrDefault();
                    //var existUser = db.Users.Find(user.Email);
                    //if(existUser.Pass == user.Pass)
                    if (existUser != null)
                    {
                        return Ok(new { status = "successful" });
                    }
                    return Ok(new { status = "unsuccessful" });
                }*/
    }
}
