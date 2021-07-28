using BotDetect.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommomConstants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Email, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.Email);
                    var userSession = new UserLogin();
                    userSession.Email = user.Email;
                    userSession.UserID = user.ID;
                    Session.Add(CommomConstants.USER_SESSION, userSession);
                    return Redirect( "/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                } 
            }
            //return Redirect("/lien-he");
            return View(model);
        }
        [HttpPost]
        //[SimpleCaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                if(dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.Name = model.Name;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreatedData = DateTime.Now;
                    user.Status = true;
                    var result = dao.Insert(user);
                    if(result>0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công");
                    }
                    return Redirect("/dang-nhap");
                }

            }

            return View(model);
            
        }

        private ActionResult Redirect()
        {
            throw new NotImplementedException();
        }
    }
}