using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.SessionState;
using FSPP_MVCApp.Models;
using FSPP_MVCApp.Helpers;
using System.Linq;

namespace FSPP_MVCApp.Controllers
{
    public class BailiffController : Controller
    {
        // Имитация хранилища данных (в реальном приложении использовалась бы база данных)
        private static List<BailiffModel> _bailiffData = new List<BailiffModel>
        {
            new BailiffModel
            {
                Id = 1,
                DebtorName = "Иванов Иван Иванович",
                DebtAmount = 15000.50m,
                CaseOpenDate = DateTime.Now.AddMonths(-2),
                CaseNumber = "ИП-123456/21",
                CaseStatus = "В производстве",
                ContactPhone = "+7 (900) 123-45-67"
            },
            new BailiffModel
            {
                Id = 2,
                DebtorName = "Петров Петр Петрович",
                DebtAmount = 67500.75m,
                CaseOpenDate = DateTime.Now.AddMonths(-5),
                CaseNumber = "ИП-789012/21",
                CaseStatus = "Завершено",
                ContactPhone = "+7 (900) 765-43-21"
            }
        };

        // GET: Bailiff/Index
        public ActionResult Index()
        {
            ViewData["UseExternalHelper"] = true; // Флаг для выбора типа вспомогательного метода
            return View(_bailiffData);
        }

        // GET: Bailiff/Details/5
        public ActionResult Details(int id)
        {
            BailiffModel bailiff = _bailiffData.Find(b => b.Id == id);
            if (bailiff == null)
            {
                return HttpNotFound();
            }
            return View(bailiff);
        }

        // GET: Bailiff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bailiff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BailiffModel bailiff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bailiff.Id = _bailiffData.Count > 0 ? _bailiffData.Max(b => b.Id) + 1 : 1;
                    _bailiffData.Add(bailiff);
                    
                    // Сохраняем номер текущего экземпляра в сессии
                    Session["CurrentBailiffId"] = bailiff.Id;
                    
                    return RedirectToAction("Index");
                }
                return View(bailiff);
            }
            catch
            {
                return View(bailiff);
            }
        }

        // GET: Bailiff/Edit/5
        public ActionResult Edit(int id)
        {
            BailiffModel bailiff = _bailiffData.Find(b => b.Id == id);
            if (bailiff == null)
            {
                return HttpNotFound();
            }
            return View(bailiff);
        }

        // POST: Bailiff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BailiffModel bailiff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int index = _bailiffData.FindIndex(b => b.Id == id);
                    if (index >= 0)
                    {
                        _bailiffData[index] = bailiff;
                        
                        // Сохраняем номер текущего экземпляра в сессии
                        Session["CurrentBailiffId"] = bailiff.Id;
                        
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                return View(bailiff);
            }
            catch
            {
                return View(bailiff);
            }
        }
        
        // Действие для демонстрации использования вспомогательных методов
        public ActionResult HelperDemo()
        {
            // Передаем значение для выбора типа вспомогательного метода
            ViewData["UseExternalHelper"] = false; // Сначала используем внутренний helper
            return View();
        }
        
        // GET: Bailiff/ToggleHelper
        public ActionResult ToggleHelper()
        {
            bool useExternalHelper = Session["UseExternalHelper"] != null ? 
                                    !(bool)Session["UseExternalHelper"] : true;
            Session["UseExternalHelper"] = useExternalHelper;
            ViewData["UseExternalHelper"] = useExternalHelper;
            
            return RedirectToAction("HelperDemo");
        }
    }
} 