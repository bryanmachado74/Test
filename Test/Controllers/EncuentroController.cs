using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Entity;
using Test.Models;

namespace Test.Controllers
{
    public class EncuentroController : Controller
    {
        public ActionResult ListarEncuentros(string id)
        {
            
            
            if (id != null)
            {
                if (id.Equals("Admin")) { return RedirectToAction("ListarEncuentrosAdmin"); }
            }

            EncuentroModel variModel = new EncuentroModel();

            List<Encuentro> list = variModel.listarEncuentro();
            foreach (Encuentro encuentro in list)
            {
                encuentro.actualizarEstado();
            }

            ModelState.Clear();

            return View(list);
        }

        public ActionResult ListarEncuentrosAdmin()
        {
            EncuentroModel variModel = new EncuentroModel();

            List<Encuentro> list = variModel.listarEncuentro();
            foreach (Encuentro encuentro in list)
            {
                encuentro.actualizarEstado();
            }

            ModelState.Clear();

            return View(list);
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
            TempData["error"] = "false";
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

                    //Valido que el nombre de los equipos sean distintos
                    if (encuentro.local.Equals(encuentro.visitante))
                    {
                        TempData["success"] = "false";
                        TempData["msj"] = "Error en los Datos! Los nombres de Local y Visita deben ser distintos.";
                        return View();
                    }

                    //Valido si el encuentro ya existe verificando el nombre de los dos equipos y la jornada.
                    List<Encuentro> lista = model.listarEncuentro();
                    foreach (Encuentro registro in lista)
                    {
                        if (encuentro.local.Equals(registro.local) || encuentro.local.Equals(registro.visitante))
                        {
                            if (encuentro.visitante.Equals(registro.local) || encuentro.visitante.Equals(registro.visitante))
                            {
                                if (encuentro.jornada == registro.jornada)
                                {
                                    TempData["success"] = "false";
                                    TempData["msj"] = "Error en los datos! Este encuentro ya existe.";
                                    return View();
                                }
                            }
                        }
                    }

                    if (model.insertarEncuentro(encuentro))
                    {
                        TempData["success"] = "true";
                        TempData["msj"] = "El Encuentro se a creado satisfactoriamente.";
                        return RedirectToAction("ListarEncuentrosAdmin");
                    }
                    else
                    {
                        TempData["success"] = "false";
                        TempData["msj"] = "Ocurrio un error al intentar crear el registro. Pongase en contacto con soporte tecnico.";
                    }
                }
                return View();
            }
            catch
            {
                TempData["success"] = "false";
                TempData["msj"] = "Ocurrio un error al intentar crear el registro. Pongase en contacto con soporte tecnico.";
                return View();
            }
        }

        public ActionResult ActualizarEncuentro(int id)
        {
            EncuentroModel variModel = new EncuentroModel();
            Encuentro encuentro = variModel.obtenerEncuentro(id);
            encuentro.actualizarEstado();

            ModelState.Clear();
            
            return View(encuentro);
        }

        [HttpPost]
        public ActionResult ActualizarEncuentro(Encuentro encuentro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EncuentroModel model = new EncuentroModel();

                    encuentro.actualizarJugado();

                    if (model.actualizarEncuentro(encuentro))
                    {
                        TempData["success"] = "true";
                        TempData["msj"] = "Encuentro actualizado correctamente.";
                        return RedirectToAction("ListarEncuentrosAdmin");
                    }
                    else
                    {
                        TempData["success"] = "false";
                        TempData["msj"] = "Ocurrio un error al intentar actualizar el registro. Pongase en contacto con soporte tecnico.";
                    }
                }
                return View();
            }
            catch
            {
                TempData["success"] = "false";
                TempData["msj"] = "Ocurrio un error al intentar actualizar el registro. Pongase en contacto con soporte tecnico.";
                return View();
            }
        }

        public ActionResult BorrarEncuentro(int id)
        {
            try
            {
                ApuestaModel apuestaModel = new ApuestaModel();
                List<Apuesta> list = apuestaModel.listarApuesta();

                foreach (Apuesta registro in list) 
                {
                    if (registro.encuentro == id) {
                        TempData["success"] = "false";
                        TempData["msj"] = "Accion invalida! El encuentro posee apuestas asociadas";
                        return RedirectToAction("ListarEncuentrosAdmin");
                    }
                }

                if (ModelState.IsValid)
                {
                    EncuentroModel e_model = new EncuentroModel();
                    if (e_model.BorrarEncuentro(id))
                    {
                        //ViewBag.AlertMsg = "Encuentro Eliminado";
                        TempData["success"] = "true";
                        TempData["msj"] = "Encuentro eliminado correctamente";
                    }
                }
                return RedirectToAction("ListarEncuentrosAdmin");
            }//end try
            catch
            {
                TempData["success"] = "false";
                TempData["msj"] = "Ocurrio un error al intentar borrar el registro. Pongase en contacto con soporte tecnico.";
                return RedirectToAction("ListarEncuentrosAdmin");
            }//catch

        }
    }
}