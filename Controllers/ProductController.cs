﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_NetMVC.Models;

namespace ASP_NetMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyDbContext context;
      
        public ProductController(MyDbContext context)
           
        {
           
            if (!context.Product.Any())
            {
                context.Product.Add(new Product()
                {
                    name = "Cuong",
                    price = "100",
                });
                context.Product.Add(new Product()
                {
                    name = "Truc",
                    price = "200;"
                });
                context.Product.Add(new Product()
                {
                    name = "Hang",
                    price = "1200"
                }
                  );
                context.SaveChanges();
            }
        }
        [HttpGet]
        public IActionResult Index(MyDbContext context)
        {
            return View(context.Product.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(MyDbContext context, int Id)
        {
            var exitProduct = context.Product.Find(Id);
            if (exitProduct == null)
            {
                return NotFound();
            }
            return View(exitProduct);
        }
        public IActionResult GetList()
        {
            return View(context.Product.ToList());
        }
        public IActionResult Update(MyDbContext context, Product product)
        {
            var exisProduct = context.Product.Find(product._id);
            if (exisProduct == null)
            {
                return NotFound();
            }
            exisProduct.name = product.name;
            exisProduct.price = product.price;
            context.Product.Update(exisProduct);
            context.SaveChanges();
            TempData["message"] = "Update success";
            return Redirect("Index");
        }
        [HttpPost]
        public IActionResult Save(MyDbContext contex, Product product)
        {
            contex.Product.Add(product);
            contex.SaveChanges();
            TempData["message"] = "Insert success";
            return Redirect("Index");
           
        }
        [HttpDelete]
        public IActionResult Delete(MyDbContext contex , int Id)
        {
            var product = context.Product.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            contex.Product.Remove(product);
            contex.SaveChanges();
            return Json(product);
        }
        [HttpDelete]
        public IActionResult DeleteMany(string ids)
        {
            return Json(ids);
        }
    }

    

}
