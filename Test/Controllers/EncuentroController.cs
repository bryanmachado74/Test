using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class EncuentroController : Controller
    {
        public ActionResult ListarEncuentros()
        {
            EncuentroModel variModel = new EncuentroModel();
            ModelState.Clear();

            return View(variModel.listarEncuentro());
        }

        public ActionResult ListarEncuentrosAdmin()
        {
            EncuentroModel variModel = new EncuentroModel();
            ModelState.Clear();

            return View(variModel.listarEncuentro());
        }

        public ActionResult ObtenerEncuentroUSOdds(int id) 
        {
            EncuentroModel variModel = new EncuentroModel();
            ModelState.Clear();
            Entity.Encuentro encuentro = variModel.obtenerEncuentro(id);
            encuentro.USOdds();

            return View(encuentro);
        }

        public ActionResult ObtenerEncuentroDecimalOdds(int id)
        {
            EncuentroModel variModel = new EncuentroModel();
            ModelState.Clear();
            Entity.Encuentro encuentro = variModel.obtenerEncuentro(id);
            encuentro.DecimalOdds();

            return View(encuentro);
        }

        public ActionResult ObtenerEncuentro(int id)
        {
            EncuentroModel variModel = new EncuentroModel();
            ModelState.Clear();

            return View(variModel.obtenerEncuentro(id));
        }

        public ActionResult InsertarEncuentro()
        {
            Models.EncuentroModel encuentroModel = new Models.EncuentroModel();
            return View();
        }

        [HttpPost]
        public ActionResult InsertarEncuentro(Entity.Encuentro encuentro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EncuentroModel model = new EncuentroModel();
                    if (model.insertarEncuentro(encuentro))
                    {
                        TempData["success"] = "true";
                        return RedirectToAction("ListarEncuentros");
                    }
                    else
                    {
                        TempData["error"] = "false";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ActualizarEncuentro(int id)
        {
            EncuentroModel variModel = new EncuentroModel();
            ModelState.Clear();

            return View(variModel.obtenerEncuentro(id));
        }

        [HttpPost]
        public ActionResult ActualizarEncuentro(Entity.Encuentro encuentro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EncuentroModel model = new EncuentroModel();
                    if (model.actualizarEncuentro(encuentro))
                    {
                        TempData["success"] = "true";
                        return RedirectToAction("ListarEncuentros");
                    }
                    else
                    {
                        TempData["error"] = "false";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BorrarEncuentro(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EncuentroModel e_model = new EncuentroModel();
                    if (e_model.BorrarEncuentro(id))
                    {
                        ViewBag.AlertMsg = "Encuentro Eliminado";
                    }
                }
                return RedirectToAction("ListarEncuentros");
            }//end try
            catch
            {
                return RedirectToAction("ListarEncuentros");
            }//catch

        }
    }
}