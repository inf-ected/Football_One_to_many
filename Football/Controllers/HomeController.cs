using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Football.Models;
using System.Data.Entity;
using Newtonsoft.Json;

namespace Football.Controllers
{

    public class HomeController : Controller
    {
        SoccerContext db = new SoccerContext();

        public ActionResult Index()
        {
            //var players = db.Players.Include(p => p.Team);
            var players = db.Players.Include(p=>p.Team);
            
            return View(players.ToList());
        }

        [HttpGet]
        public ActionResult Create() {
            //формируем список команд для передачи в представление
            SelectList teams = new SelectList(db.Teams, "Id", "Name");
            ViewBag.Teams = teams;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Player player) {
            //Добавляем игрока в таблицу
            db.Players.Add(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            Player p = db.Players.Find(id);
            if (p != null) {
                //Создаем список команд для передачи в представление
                SelectList teams = new SelectList(db.Teams,"Id","Name",p.TeamId);
                ViewBag.Teams = teams;
                return View(p);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id) {
            if (id == null)
                return HttpNotFound();
            Player p = db.Players.Find(id);
            if (p != null)
            {
                db.Players.Remove(p);
                db.SaveChanges();
            }
                return RedirectToAction("Index");
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}