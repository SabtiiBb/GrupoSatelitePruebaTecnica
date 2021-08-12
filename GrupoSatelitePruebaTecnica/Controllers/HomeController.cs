using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoSatelitePruebaTecnica.DataContext;
using GrupoSatelitePruebaTecnica.Models;
using GrupoSatelitePruebaTecnica.Services;

namespace GrupoSatelitePruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        readonly AlumnoRepository _manejoAlumnoServices = new AlumnoRepository();
        public ActionResult Index()
        {
            AlumnoVM model = _manejoAlumnoServices.GetListadoAlumnos();
            return View(model);
        }

        public ActionResult CrearAlumno()
        {
            ViewBag.GradosLst = _manejoAlumnoServices.GetGrados();
            return View();
        }

        [HttpPost]
        public ActionResult CrearAlumno(AlumnoVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }else if (_manejoAlumnoServices.crearAlumno(model))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult EditarAlumno(int id)
        {
            AlumnoVM model = _manejoAlumnoServices.getAlumno(id);
            ViewBag.GradosLst = _manejoAlumnoServices.GetGrados();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarAlumno(AlumnoVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }else if (_manejoAlumnoServices.EditarAlumno(model))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        public JsonResult EliminarAlumno(int id)
        {
            bool Exito = _manejoAlumnoServices.EliminarAlumno(id) ? true : false;
            return Json(Exito, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDeatilsModal(int id)
        {
            AlumnoVM model = _manejoAlumnoServices.getAlumno(id);
            string partial = RenderPartialToString("~/Views/Home/_PartialDetalleAlumno.cshtml", model);

            return Json(new { partial }, JsonRequestBehavior.AllowGet);
        }

        public string RenderPartialToString(string ViewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, ViewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);

                return writer.ToString();
            }
        }
    }
}