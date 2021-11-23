using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AmigoSecreto.Models;

namespace AmigoSecretoSystem.Controllers
{
    public class AmigoSecretoController : Controller
    {
        private Context db = new Context();

        // GET: AmigoSecreto
        public ActionResult Index()
        {
            return View(db.AmigoSecreto.ToList());
        }

        // GET: AmigoSecreto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmigoSecreto.Models.AmigoSecreto amigoSecreto = db.AmigoSecreto.Find(id);
            if (amigoSecreto == null)
            {
                return HttpNotFound();
            }
            return View(amigoSecreto);
        }

        // GET: AmigoSecreto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AmigoSecreto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,DataHora")] AmigoSecreto.Models.AmigoSecreto amigoSecreto)
        {
            if (ModelState.IsValid)
            {
                db.AmigoSecreto.Add(amigoSecreto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amigoSecreto);
        }

        // GET: AmigoSecreto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmigoSecreto.Models.AmigoSecreto amigoSecreto = db.AmigoSecreto.Find(id);
            if (amigoSecreto == null)
            {
                return HttpNotFound();
            }
            return View(amigoSecreto);
        }

        // POST: AmigoSecreto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,DataHora")] AmigoSecreto.Models.AmigoSecreto amigoSecreto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amigoSecreto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amigoSecreto);
        }

        // GET: AmigoSecreto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmigoSecreto.Models.AmigoSecreto amigoSecreto = db.AmigoSecreto.Find(id);
            if (amigoSecreto == null)
            {
                return HttpNotFound();
            }
            return View(amigoSecreto);
        }

        // POST: AmigoSecreto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmigoSecreto.Models.AmigoSecreto amigoSecreto = db.AmigoSecreto.Find(id);
            db.AmigoSecreto.Remove(amigoSecreto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
