using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
    
                long id = dao.Insert(product);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    
                    ModelState.AddModelError("", "Thêm sản phẩm thành công");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pc = new ProductDao().ViewDetail(id);
           
            return View(pc);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
           
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
  
                var result = dao.Update(product);
              
                if (result)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm thành công");
                }

            }

            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
      

    }
}