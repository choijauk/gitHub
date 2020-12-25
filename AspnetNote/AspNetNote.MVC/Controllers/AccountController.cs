using AspNetNote.MVC.DataContext;
using AspNetNote.MVC.Models;
using AspNetNote.MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetNote.MVC.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // ID, 비밀번호 - 필수
            if (ModelState.IsValid)
            {
                using (var db = new AspNetNoteDbContext())
                {
                    // Linq 쿼리식
                    // => : A go to B
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && 
                                                        u.UserPassword.Equals(model.UserPassword));

                    if(user != null)
                    {
                        // 로그인에 성공했을 때

                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo); //Session 설정
                        return RedirectToAction("LoginSuccess", "Home"); // 로그인 성공 페이지로 이동
                        
                    }
                }

                // 로그인에 실패했을 때
                ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
            }
            
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            //HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 회원 가입
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 회원가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid)
            {
                // Java try(SqlSession){} catch(){}
                // C# using문 사용(DB 열고 닫고)
                using (var db = new AspNetNoteDbContext())
                {
                    db.Users.Add(model); //메모리까지 올려서
                    db.SaveChanges();    //실제 DB에 Update
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}
