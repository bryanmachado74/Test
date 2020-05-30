using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Entity;
using Test.Models;

namespace Test.Controllers
{
    public class ApuestaController : Controller
    {
        public ActionResult ListarApuesta()
        {
            ApuestaModel variModel = new ApuestaModel();
            ModelState.Clear();
            List<Apuesta> list = variModel.listarApuesta();

            foreach (Apuesta apuesta in list) 
            {
                EncuentroModel encuentroModel = new EncuentroModel();
                Encuentro encuentro = encuentroModel.obtenerEncuentro(apuesta.encuentro);

                if (apuesta.eleccion.Equals("Local"))
                    apuesta.calcularGanacia(encuentro.probabilidad_local);
                else if(apuesta.eleccion.Equals("Empate"))
                    apuesta.calcularGanacia(encuentro.probabilidad_empate);
                else
                    apuesta.calcularGanacia(encuentro.probabilidad_visita);
            }

            return View(list);
        }

        public ActionResult ObtenerApuesta(int id)
        {
            ApuestaModel variModel = new ApuestaModel();
            Apuesta apuesta = variModel.obtenerApuesta(id);
            
            EncuentroModel encModel = new EncuentroModel();
            Encuentro encuentro = encModel.obtenerEncuentro(apuesta.encuentro);
            ViewData["local"] = encuentro.local;
            ViewData["visita"] = encuentro.visitante;
            ViewData["mlocal"] = encuentro.marcador_local;
            ViewData["mvisita"] = encuentro.marcador_visitante;
            encuentro.actualizarEstado();
            ViewData["estado"] = encuentro.estado;

            //calcular la ganancia
            if (apuesta.eleccion.Equals("Local"))
                apuesta.calcularGanacia(encuentro.probabilidad_local);
            else if (apuesta.eleccion.Equals("Empate"))
                apuesta.calcularGanacia(encuentro.probabilidad_empate);
            else
                apuesta.calcularGanacia(encuentro.probabilidad_visita);

            ModelState.Clear();

            return View(apuesta);
        }

        public ActionResult InsertarApuesta(int id)
        {
            EncuentroModel modEncuentro = new EncuentroModel();
            Entity.Encuentro encuentro = modEncuentro.obtenerEncuentro(id);
            //Session["Nombre"] = "hola mundo";

            Entity.Apuesta apuesta = new Entity.Apuesta();

            apuesta.cliente = 1; // valor quemado debido a que no esta implementada la sesion
            apuesta.encuentro = encuentro.id;

            TempData["LocalD"] = 100 / encuentro.probabilidad_local;
            TempData["EmpateD"] = 100 / encuentro.probabilidad_empate;
            TempData["VisitaD"] = 100 / encuentro.probabilidad_visita;

            int odds = Convert.ToInt32(Request.QueryString["odds"]);
            if (odds == 1)
                encuentro.DecimalOdds();
            else
                encuentro.USOdds();

            TempData["Local"] = encuentro.probabilidad_local;
            TempData["Empate"] = encuentro.probabilidad_empate;
            TempData["Visita"] = encuentro.probabilidad_visita;

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "Local", Value = "Local" });
            items.Add(new SelectListItem() { Text = "Empate", Value = "Empate" });
            items.Add(new SelectListItem() { Text = "Visitante", Value = "Visitante" });

            //SelectList list = new SelectList(items, "", "");
            ViewData["eleccion"] = items;


            //ApuestaModel encuentroModel = new ApuestaModel();
            return View(apuesta);
        }

        [HttpPost]
        public ActionResult InsertarApuesta(Apuesta apuesta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApuestaModel model = new ApuestaModel();
                    if (model.insertarApuesta(apuesta))
                    {
                        TempData["success"] = "true";
                        TempData["msj"] = "La Apuesta se a creado satisfactoriamente.";
                        return RedirectToAction("ListarApuesta");
                    }
                    else
                    {
                        TempData["success"] = "false";
                        TempData["msj"] = "Ha ocurrido un error! Pongase en contacto con el administrador.";
                    }
                }
                return View();
            }
            catch
            {
                TempData["success"] = "false";
                TempData["msj"] = "Ha ocurrido un error! Pongase en contacto con el administrador.";
                return View();
            }
        }

        //public ActionResult ActualizarApuesta(int id)
        //{
        //    ApuestaModel variModel = new ApuestaModel();
        //    ModelState.Clear();

        //    return View(variModel.obtenerApuesta(id));
        //}

        //[HttpPost]
        //public ActionResult ActualizarApuesta(Entity.Apuesta apuesta)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            ApuestaModel model = new ApuestaModel();
        //            if (model.actualizarApuesta(apuesta))
        //            {
        //                TempData["success"] = "true";
        //                return RedirectToAction("ListarApuesta");
        //            }
        //            else
        //            {
        //                TempData["error"] = "false";
        //            }
        //        }
        //        return View();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult BorrarApuesta(int id)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            ApuestaModel e_model = new ApuestaModel();
        //            if (e_model.BorrarApuesta(id))
        //            {
        //                ViewBag.AlertMsg = "Apuesta Eliminado";
        //            }
        //        }
        //        return RedirectToAction("ListarApuesta");
        //    }//end try
        //    catch
        //    {
        //        return RedirectToAction("ListarApuesta");
        //    }//catch

        //}
    }//end class
}//end namespace