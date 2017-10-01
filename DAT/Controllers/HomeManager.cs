using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAT.Controllers
{
    public class HomeManager : Controller
    {
        public ActionResult Crear()
        {

            return View();             
        }
        
        
        // GET: HomeManager
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeManager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
