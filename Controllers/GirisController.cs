using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;
using yemektarifleric.Models;

namespace yemektarifleric.Controllers
{
    public class GirisController : Controller
    {
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(Kullanicilar k, string ReturnUrl)
        {
            yemektarifleriDBContext db = new yemektarifleriDBContext();
            var kullanici = db.Kullanicilars.FirstOrDefault
                (kul => kul.Eposta == k.Eposta && kul.Parola == MD5Sifrele(k.Parola) && kul.Silindi == false && kul.Aktif == true);
            if (kullanici != null)
            {
                string yetki = (bool)kullanici.Yetki ? "Yonetici" : "Uye";
                var talepler = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,kullanici.Eposta.ToString()),
                    new Claim(ClaimTypes.Role,yetki),
                    new Claim(ClaimTypes.NameIdentifier,kullanici.KullaniciId.ToString())
                };
                ClaimsIdentity kimlik = new ClaimsIdentity(talepler, "Login");
                ClaimsPrincipal kural = new ClaimsPrincipal(kimlik);
                await HttpContext.SignInAsync(kural);
                if (!String.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    if ((bool)kullanici.Yetki)
                    {
                        return Redirect("/Yonetim/Index");
                    }

                    else
                    {
                        return Redirect("/Home/Index");
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
        public static string MD5Sifrele(string sifrelenecekMetin)
        {

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            //dizinin hash'ini hesaplattık.
            dizi = md5.ComputeHash(dizi);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            StringBuilder sb = new StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
        }
    }
}
