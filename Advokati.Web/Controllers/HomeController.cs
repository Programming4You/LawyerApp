using System.Linq;
using System.Web.Mvc;
using Advokati.Infrastructure;
using Advokati.Web.Models;

namespace Advokati.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly KorisnikRepository _db = new KorisnikRepository();
        private readonly TaskRepository _taskDb = new TaskRepository();
        private readonly UlogaRepository _uDb = new UlogaRepository();

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string username, string password, int ulogaId=2)
        {

           if (_db.FindByUsernamePassword(username, password) != null)
            {
                if (username == "admin" && password == "admin")
                {
                    Session["admin"] = username;
                    return RedirectToAction("Index", "Advokats", new { area = "" });
                }
                else if (_uDb.FindById(ulogaId) != null && ulogaId == 2)
                {
                    var obj = _db.GetKorisnici().FirstOrDefault(u => u.Username == username);
                    Session["UserID"] = obj.Id.ToString();

                    return RedirectToAction("Index", "Advokats", new { area = "" });
                }
            }

            TempData["ErrorMessage"] = "Uneti podaci nisu tačni! Pokušajte ponovo.";
            return RedirectToAction("Index");
        }

        
        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Index", "Home");
        }





        [HttpGet]
        public ActionResult GetData()
        {
            var peraPeric =
                (from t in _taskDb.GetTasks().Where(a => a.AdvokatId == 16) select t);
            int ppCount = peraPeric.Count();

            var mikaMikic =
                (from t in _taskDb.GetTasks().Where(a => a.AdvokatId == 2) select t);
            int mmCount = mikaMikic.Count();

            var lazaLazic =
                (from t in _taskDb.GetTasks().Where(a => a.AdvokatId == 3) select t);
            int llCount = lazaLazic.Count();

            var lukaLukic =
                (from t in _taskDb.GetTasks().Where(a => a.AdvokatId == 4) select t);
            int lukaCount = lukaLukic.Count();

            var marijaMaric =
                (from t in _taskDb.GetTasks().Where(a => a.AdvokatId == 5) select t);
            int marijaCount = marijaMaric.Count();

            var jovanJovic =
                (from t in _taskDb.GetTasks().Where(a => a.AdvokatId == 11) select t);
            int jovanCount = jovanJovic.Count();


            Ratio obj = new Ratio
            {
                PeraPeric = ppCount,
                MikaMikic = mmCount,
                LazaLazic = llCount,
                LukaLukic = lukaCount,
                MarijaMaric = marijaCount,
                JovanJovic = jovanCount
            };


            ViewBag.PeraPeric = ppCount;
            ViewBag.MikaMikic = mmCount;
            ViewBag.LazaLazic = llCount;
            ViewBag.LukaLukic = lukaCount;
            ViewBag.MarijaMaric = marijaCount;
            ViewBag.JovanJovic = jovanCount;


            return View(obj);
        }



    }
}