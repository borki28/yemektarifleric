using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using yemektarifleric.Models;

namespace yemektarifleric.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        yemektarifleriDBContext db = new yemektarifleriDBContext();
        public IActionResult Index(int id )
        {
            
            var sayfa = db.Sayfalars.Where(a => a.Silindi == false && a.Aktif == true && a.SayfaId == id).FirstOrDefault();
            return View(sayfa);
        }
        public IActionResult Tarif()
        {
             var tarifler = db.YemekTarifleris.Where(y => y.Silindi == false && y.Aktif == true ).ToList();
            return View(tarifler);
        }
        public IActionResult Tarif(int id)
        {
         
            var tarifler = db.YemekTarifleris.Where(y => y.Silindi == false && y.Aktif == true &&y.TarifId==id).ToList();
            return View(tarifler);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
