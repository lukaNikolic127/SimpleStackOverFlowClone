using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data.UnitOfWork;
using Forum.Domain;
using Forum.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.WebApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MemberController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: MemberController
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                try
                {
                    Member member = unitOfWork.Member.GetByUsernameAndPassword(
                    new Member { Username = model.Username, Password = model.Password });
                    HttpContext.Session.SetInt32("member_id", member.MemberId);
                    HttpContext.Session.SetString("username", member.Username);
                    return RedirectToAction("Index", "Topic");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Wrong credentials");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MemberController/Create
        [ActionName("Register")]
        public ActionResult Create()
        {
            return View("Register");
        }

        // POST: MemberController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ActionName("Register")]
        public ActionResult Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            else
            {
                try
                {
                    if (unitOfWork.Member.IsUsernameTaken(model.Username))
                    {
                        ModelState.AddModelError("UsernameUniqueError", "Username alreday exists.");
                        return View("Register");
                    }
                    if(model.Password != model.PasswordCheck)
                    {
                        ModelState.AddModelError("PasswordRepeatError", "Passwords don't match.");
                        return View("Register");
                    }
                    Member newMember = new Member 
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Username = model.Username,
                        Password = model.Password
                    };
                    unitOfWork.Member.Add(newMember);
                    unitOfWork.Commit();
                    Member member = unitOfWork.Member.GetByUsernameAndPassword(newMember);
                    HttpContext.Session.SetInt32("member_id", member.MemberId);
                    HttpContext.Session.SetString("username", member.Username);
                    return RedirectToAction("Index", "Topic");
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
