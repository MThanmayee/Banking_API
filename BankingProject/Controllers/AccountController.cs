using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingProject.Models;
using System.Net.Mail;
using System.Text;

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
        [HttpPut("register")]
        public IActionResult Register(Account user)
        {
            var user1 = db.Account.Where(x => x.AccountNumber == user.AccountNumber).FirstOrDefault();
            if (user1 != null)
            {
                user1.Password = user.Password;
                user1.Otp = Generate_otp();

                db.SaveChanges();
                SendMail("samidhajadhav52@gmail.com", user1.Email, "OTP Verification", "Your OTP is " + user1.Otp);


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
            var from = db.Transactions.Where(x => x.FromAccount == AccountNumber);
            var to = db.Transactions.Where(x => x.ToAccount == AccountNumber);

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

        [HttpGet("singletransaction")]
        public IActionResult singletransaction(int transactionId)
        {
            var A = db.Transactions.Find(transactionId);
            if (A != null)
            {
                return Ok(A);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult NewBenificiary(Benificiaries benificiaries)
        {
            var benificiary = db.Benificiaries.Find(benificiaries.ToAccount);
            if (benificiary == null)
            {
                db.Benificiaries.Add(benificiaries);
                db.SaveChanges();
                return Ok(new { status = "added" });
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("getbenificiary")]
        public IActionResult GetBenificiary(long accountnumber)
        {
            var q = db.Benificiaries.Where(x => x.FromAccount == accountnumber);
            return Ok(q);
        }

        [HttpPost("newtransaction")]
        public IActionResult NewTransaction(Transactions transactions)
        {
            db.Transactions.Add(transactions);
            db.SaveChanges();
            var accountnumber1 = db.Account.Find(transactions.FromAccount);
            var accountnumber2 = db.Account.Find(transactions.ToAccount);
            var transaction = db.Transactions.Find(transactions.TransactionId);
            if (accountnumber1 != null && accountnumber2 != null)
            {
                transaction.ClosingBalance = accountnumber1.Balance - transactions.Amount;
                transaction.ToClosingBalance = accountnumber2.Balance + transactions.Amount;
                accountnumber1.Balance = accountnumber1.Balance - transactions.Amount;
                accountnumber2.Balance = accountnumber2.Balance + transactions.Amount;

                db.SaveChanges();
                return Ok();
            }

            else if (accountnumber1 != null)
            {
                transaction.ClosingBalance = accountnumber1.Balance - transactions.Amount;
                accountnumber1.Balance = accountnumber1.Balance - transactions.Amount;
                db.SaveChanges();
                return Ok();
            }


            else
            {
                return BadRequest();
            }
          
            return Ok();
        }
        //[HttpPut("accountbalance")]
        //public IActionResult Updatebalance(Transactions transactions)
        //{
        //    // var account = db.Account.Find(accountnumber);
        //    var accountnumber1 = db.Account.Find(transactions.FromAccount);
        //    var accountnumber2 = db.Account.Find(transactions.ToAccount);
        //    var transaction = db.Transactions.Find(transactions.TransactionId);
        //    if (accountnumber1 != null && accountnumber2 != null)
        //    {
        //        transaction.ClosingBalance = accountnumber1.Balance - transactions.Amount;
        //        transaction.ToClosingBalance = accountnumber2.Balance + transactions.Amount;
        //        accountnumber1.Balance = accountnumber1.Balance - transactions.Amount;
        //        accountnumber2.Balance = accountnumber2.Balance + transactions.Amount;
                
        //        db.SaveChanges();
        //        return Ok();
        //    }
           
        //    else if (accountnumber1 != null)
        //    {
        //        transaction.ClosingBalance = accountnumber1.Balance - transactions.Amount;
        //        accountnumber1.Balance = accountnumber1.Balance - transactions.Amount;
        //        db.SaveChanges();
        //        return Ok();
        //    }


        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpGet("getbydate")]
        public IActionResult GetbyDate(DateTime fromdate, DateTime todate, long accountnumber)
        {
            var q = db.Transactions.Where(x => x.Date >=fromdate && x.Date<= todate && x.FromAccount==accountnumber).ToList();
            //var f = db.Transactions.Where(x => x.Date == fromdate);
            //var t = db.Transactions.Where(x => x.Date == todate);
            //var q = (from r in db.Transactions
            //             //where fromdate == r.Date && todate == r.Date
            //         where 
            //         select new
            //         {
            //             r.TransactionId,
            //             r.FromAccount,
            //             r.ToAccount,
            //             r.Amount,
            //             r.Date,
            //             r.TransactionType,
            //             r.MaturityInstructions,
            //             r.Remarks,
            //             r.ClosingBalance
            //         }
            //    ).ToList();
            return Ok(q);
        }

        [HttpPost("adminlogin")]
        public IActionResult adminlogin(Admin user)
        {
            Admin user1 = db.Admin.Where(x => x.Id == user.Id && x.Password == user.Password).FirstOrDefault();
            if (user1 == null)
            {
                return BadRequest();

            }
            else
            {
                return Ok();
            }

        }

        [HttpGet("displayinfo")]
        public IActionResult GetInfo()
        {

            return Ok(db.UserProfile);
        }

        [NonAction]
        protected string Generate_otp()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < 6; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }

        [NonAction]
        public long Generate_Acc()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < 8; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return long.Parse("2021" + strrandom);
        }

        [NonAction]
        protected int Generate_Custid()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < 8; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return Convert.ToInt32(strrandom);
        }
        [NonAction]
        public void SendMail(string from, string To, string subject, string msg)
        {
            MailMessage mail = new MailMessage(from, To);
            mail.Subject = subject;
            mail.Body = msg;

            //Attachment attachment = new Attachment(@"");
            //mail.Attachments.Add(attachment);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "samidhajadhav52@gmail.com",
                Password = "mummydaddy"
            };
            client.EnableSsl = true;
            client.Send(mail);

        }

        [HttpPost("approve")]
        public IActionResult Approve(UserProfile user)
        {

            var user2 = db.Account.Where(x => x.Email == user.EmailId).FirstOrDefault();
            Random r = new Random();
            var user3 = db.UserProfile.Where(x => x.EmailId == user.EmailId).FirstOrDefault();

            if (user2 == null)
            {
                user3.AccountStatus = "yes";
                user2 = new Account();
                user2.AccountNumber = Generate_Acc();
                user2.CustomerId = r.Next(11111111, 99999999);
                user2.Email = user.EmailId;
                user2.UserName = user.FirstName;
                user2.Balance = 1000;
                user2.AccountType = "savings";
                db.Account.Add(user2);
                db.SaveChanges();
                return Ok(db.Account);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("confirmotp")]
        public IActionResult confirmotp(Account user)
        {
            var user1 = db.Account.Where(x => x.AccountNumber == user.AccountNumber).FirstOrDefault();
            if (user1 != null && user1.Otp == user.Otp)
            {
                user1.TransactionPassword = user.TransactionPassword;

                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }




        [HttpPut("forgotuid")]
        public IActionResult forgotuid(Account user)
        {
            var user1 = db.Account.Where(x => x.AccountNumber == user.AccountNumber).FirstOrDefault();
            if (user1 != null)
            {
                user1.Otp = Generate_otp();
                db.SaveChanges();
                SendMail("samidhajadhav52@gmail.com", user1.Email, "OTP Verification", "Your OTP is " + user1.Otp);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("confirmotp1")]
        public IActionResult confirmotp1(Account user)
        {
            var user1 = db.Account.Where(x => x.AccountNumber == user.AccountNumber).FirstOrDefault();
            if (user1 != null && user1.Otp == user.Otp)
            {

                SendMail("samidhajadhav52@gmail.com", user1.Email, "CustomerID", "Your Customer ID is " + user1.CustomerId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("forgotpwd")]
        public IActionResult forgotpwd(Account user)
        {
            var user1 = db.Account.Where(x => x.CustomerId == user.CustomerId).FirstOrDefault();
            if (user1 != null)
            {
                user1.Otp = Generate_otp();
                db.SaveChanges();
                SendMail("samidhajadhav52@gmail.com", user1.Email, "OTP Verification", "Your OTP is " + user1.Otp);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("confirmotp2")]
        public IActionResult confirmotp2(Account user)
        {
            var user1 = db.Account.Where(x => x.AccountNumber == user.AccountNumber).FirstOrDefault();
            if (user1 != null && user1.Otp == user.Otp)
            {
                user1.Password = user.Password;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



    }

}
