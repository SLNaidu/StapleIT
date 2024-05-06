using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StapleIT.DAL;
using StapleIT.Models;
using System;

namespace StapleIT.Controllers
{
    public class LoginController : Controller
    {
        private StapleITContext _context;
        public LoginController(StapleITContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(User user)
        {
           
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
           

        }
        public IActionResult login(string Email, string password)
        {
            List<User> emailList = _context.User.ToList();
            var user = emailList.Where(v => v.EmailAdd == Email).FirstOrDefault();
          
            
            if (user != null && user.Password == password)
            {
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                if (user == null)
                {
                    TempData["msg"] = "No Account found...!";
                }
                else
                {
                    TempData["msg"] = "Password is wrong...!";
                }
                
                return RedirectToAction("Index", "Login");

            }


           
        }

        
    }
}
