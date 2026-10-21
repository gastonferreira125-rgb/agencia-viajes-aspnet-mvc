using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace PaquetesViajes.Controllers
{
    public class EstadoController : Controller
    {
        [HttpGet]
        public ActionResult Listado()
        {
            try
            {
                List<Estado> oLista = LogicaEstado.ListarEstados();

                return View(oLista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new List<Estado>());
            }
        }

        [HttpGet]
        public ActionResult Consultar(string pCodigoEstado)
        {
            try
            {
                Estado oEstado = LogicaEstado.Buscar(pCodigoEstado);

                if (oEstado != null)
                {
                    return View(oEstado);
                }
                else
                {
                    throw new Exception("El Estado no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Estado());
            }
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View(new Estado());
        }

        [HttpPost]
        public ActionResult Agregar(Estado pEstado)
        {
            try
            {
                // Validamos el objeto
                pEstado.Validar();

                // Mandamos a dar de alta
                LogicaEstado.Agregar(pEstado);

                return RedirectToAction("Listado", "Estado");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(pEstado);
            }
        }

        [HttpGet]
        public ActionResult Modificar(string pCodigoEstado)
        {
            try
            {
                Estado oEstado = LogicaEstado.Buscar(pCodigoEstado);

                if (oEstado != null)
                {
                    return View(oEstado);
                }
                else
                {
                    throw new Exception("El Estado no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Estado());
            }
        }

        [HttpPost]
        public ActionResult Modificar(Estado pEstado)
        {
            try
            {
                // Validamos el objeto
                pEstado.Validar();

                // Mandamos a modificar
                LogicaEstado.Modificar(pEstado);

                return RedirectToAction("Listado", "Estado");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(pEstado);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(string pCodigoEstado)
        {
            try
            {
                Estado oEstado = LogicaEstado.Buscar(pCodigoEstado);

                if (oEstado != null)
                {
                    return View(oEstado);
                }
                else
                {
                    throw new Exception("El Estado no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Estado());
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Estado pEstado)
        {
            try
            {
                // En eliminar NO validamos el objeto
                LogicaEstado.Eliminar(pEstado);

                return RedirectToAction("Listado", "Estado");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(pEstado);
            }
        }

        [HttpGet]
        public ActionResult PaquetesPorEstado(string pCodigoEstado)
        {
            try
            {
                List<PaqueteViaje> oLista =
                    LogicaPaqueteViaje.ListarPaquetesPorEstado(pCodigoEstado);

                return View(oLista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new List<PaqueteViaje>());
            }
        }

        [HttpGet]
        public ActionResult HospedajesPorEstado(string pCodigoEstado)
        {
            try
            {
                List<Hospedaje> oLista =
                    LogicaHospedaje.ListarHospedajesPorEstado(pCodigoEstado);

                return View(oLista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new List<Hospedaje>());
            }
        }
    }
}