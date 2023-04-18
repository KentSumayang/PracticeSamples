using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityWebApplication.Controllers
{
    public class PersonalInfoController : Controller
    {
        // GET: PersonalInfoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonalInfoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonalInfoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalInfoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonalInfoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonalInfoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonalInfoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonalInfoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
