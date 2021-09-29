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
        [HttpGet("display")]
        public IActionResult GetDetails(string EmailId)
        {
            return Ok(db.UserProfile.Where(x=>x.EmailId==EmailId).FirstOrDefault());
        }

        [HttpGet("getnumber")]
        public IActionResult GetNumber(int customerid)
        {
            var q = db.Account.Where(x => x.CustomerId == customerid).FirstOrDefault();
            return Ok(q);
        }

        [HttpGet("Summary")]
        public IActionResult GetAccountById(long accountnumber)
        {

            Account account1 = db.Account.Find(accountnumber);
            if (account1 != null)
                return Ok(account1);
            else
                return BadRequest();


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


        [HttpGet("transactions")]
        public IActionResult GetTransaction(long AccountNumber)
        {
            Transactions from = db.Transactions.Where(x => x.FromAccount == AccountNumber).FirstOrDefault();
            Transactions to = db.Transactions.Where(x => x.ToAccount == AccountNumber).FirstOrDefault();

            var A = from t1 in db.Transactions
                    where t1.FromAccount == AccountNumber || t1.ToAccount == AccountNumber
                    select t1;
                    
            if(from != null && to != null)
            {
                return Ok(A);
            }
            else if(from != null)
            {
                return Ok(from);
            }
            else if(to!= null)
            {
                return Ok(to);
            }
            else
            {
                return BadRequest();
            }
        }
    }



}
