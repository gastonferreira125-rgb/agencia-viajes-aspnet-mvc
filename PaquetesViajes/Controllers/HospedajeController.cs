using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace PaquetesViajes.Controllers
{
    public class HospedajeController : Controller
    {
        [HttpGet]
        public ActionResult Listado()
        {
            try
            {
                List<Hospedaje> oLista =
                    LogicaHospedaje.ListarHospedajes();

                return View(oLista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new List<Hospedaje>());
            }
        }

        [HttpGet]
        public ActionResult Consultar(string pCodigoInterno)
        {
            try
            {
                Hospedaje oHospedaje =
                    LogicaHospedaje.Buscar(pCodigoInterno);

                if (oHospedaje != null)
                {
                    return View(oHospedaje);
                }
                else
                {
                    throw new Exception("El Hospedaje no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Hospedaje());
            }
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            try
            {
                ViewBag.ListaEstados = new SelectList(
                    LogicaEstado.ListarEstados(),
                    "CodigoEstado",
                    "Nombre"
                );

                return View(new Hospedaje());
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                ViewBag.ListaEstados = new SelectList(
                    new List<Estado>(),
                    "CodigoEstado",
                    "Nombre"
                );

                return View(new Hospedaje());
            }
        }

        [HttpPost]
        public ActionResult Agregar(
            Hospedaje pHospedaje,
            string CodigoEstado)
        {
            try
            {
                // Buscamos el Estado seleccionado
                pHospedaje.Estado =
                    LogicaEstado.Buscar(CodigoEstado);

                // Validamos el objeto ya completo
                pHospedaje.Validar();

                // Mandamos a dar de alta
                LogicaHospedaje.Agregar(pHospedaje);

                return RedirectToAction("Listado", "Hospedaje");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                ViewBag.ListaEstados = new SelectList(
                    LogicaEstado.ListarEstados(),
                    "CodigoEstado",
                    "Nombre",
                    CodigoEstado
                );

                return View(pHospedaje);
            }
        }

        [HttpGet]
        public ActionResult Modificar(string pCodigoInterno)
        {
            try
            {
                Hospedaje oHospedaje =
                    LogicaHospedaje.Buscar(pCodigoInterno);

                if (oHospedaje == null)
                {
                    throw new Exception("El Hospedaje no existe.");
                }

                ViewBag.ListaEstados = new SelectList(
                    LogicaEstado.ListarEstados(),
                    "CodigoEstado",
                    "Nombre",
                    oHospedaje.Estado.CodigoEstado
                );

                return View(oHospedaje);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                ViewBag.ListaEstados = new SelectList(
                    new List<Estado>(),
                    "CodigoEstado",
                    "Nombre"
                );

                return View(new Hospedaje());
            }
        }

        [HttpPost]
        public ActionResult Modificar(
            Hospedaje pHospedaje,
            string CodigoEstado)
        {
            try
            {
                // Buscamos el Estado seleccionado
                pHospedaje.Estado =
                    LogicaEstado.Buscar(CodigoEstado);

                // Validamos el objeto ya completo
                pHospedaje.Validar();

                // Mandamos a modificar
                LogicaHospedaje.Modificar(pHospedaje);

                return RedirectToAction("Listado", "Hospedaje");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                ViewBag.ListaEstados = new SelectList(
                    LogicaEstado.ListarEstados(),
                    "CodigoEstado",
                    "Nombre",
                    CodigoEstado
                );

                return View(pHospedaje);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(string pCodigoInterno)
        {
            try
            {
                Hospedaje oHospedaje =
                    LogicaHospedaje.Buscar(pCodigoInterno);

                if (oHospedaje != null)
                {
                    return View(oHospedaje);
                }
                else
                {
                    throw new Exception("El Hospedaje no existe.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new Hospedaje());
            }
        }

        [HttpPost]
        public ActionResult Eliminar(Hospedaje pHospedaje)
        {
            try
            {
                // En eliminar NO validamos el objeto
                LogicaHospedaje.Eliminar(pHospedaje);

                return RedirectToAction("Listado", "Hospedaje");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(pHospedaje);
            }
        }
    }
}