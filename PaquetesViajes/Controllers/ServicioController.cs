using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace PaquetesViajes.Controllers
{
    public class ServicioController : Controller
    {
        [HttpGet]
        public ActionResult Listado()
        {
            try
            {
                List<Servicio> oLista = LogicaServicio.ListarServicios();

                return View(oLista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new List<Servicio>());
            }
        }


        [HttpGet]
        public ActionResult Consultar(
            int pNumeroOmnibus,
            DateTime pFechaHoraPartida)
        {
            try
            {
                Servicio oServicio = LogicaServicio.Buscar(
                    pNumeroOmnibus,
                    pFechaHoraPartida
                );

                if (oServicio != null)
                {
                    return View(oServicio);
                }
                else
                {
                    throw new Exception("El Servicio no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Servicio());
            }
        }


        [HttpGet]
        public ActionResult Agregar()
        {
            return View(new Servicio());
        }


        [HttpPost]
        public ActionResult Agregar(Servicio pServicio)
        {
            try
            {
                // Validamos en el Controller
                pServicio.Validar();

                LogicaServicio.Agregar(pServicio);

                return RedirectToAction("Listado", "Servicio");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(pServicio);
            }
        }


        [HttpGet]
        public ActionResult Modificar(
            int pNumeroOmnibus,
            DateTime pFechaHoraPartida)
        {
            try
            {
                Servicio oServicio = LogicaServicio.Buscar(
                    pNumeroOmnibus,
                    pFechaHoraPartida
                );

                if (oServicio != null)
                {
                    return View(oServicio);
                }
                else
                {
                    throw new Exception("El Servicio no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Servicio());
            }
        }


        [HttpPost]
        public ActionResult Modificar(Servicio pServicio)
        {
            try
            {
                // Validamos en el Controller
                pServicio.Validar();

                LogicaServicio.Modificar(pServicio);

                return RedirectToAction("Listado", "Servicio");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(pServicio);
            }
        }


        [HttpGet]
        public ActionResult Eliminar(
            int pNumeroOmnibus,
            DateTime pFechaHoraPartida)
        {
            try
            {
                Servicio oServicio = LogicaServicio.Buscar(
                    pNumeroOmnibus,
                    pFechaHoraPartida
                );

                if (oServicio != null)
                {
                    return View(oServicio);
                }
                else
                {
                    throw new Exception("El Servicio no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Servicio());
            }
        }


        [HttpPost]
        public ActionResult Eliminar(Servicio pServicio)
        {
            try
            {
                // En Eliminar NO validamos
                LogicaServicio.Eliminar(pServicio);

                return RedirectToAction("Listado", "Servicio");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(pServicio);
            }
        }
    }
}