using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using EntidadesCompartidas;
using Logica;

namespace PaquetesViajes.Controllers
{
    public class PaqueteViajeController : Controller
    {
        [HttpGet]
        public ActionResult Listado()
        {
            try
            {
                List<PaqueteViaje> oLista =
                    LogicaPaqueteViaje.ListarPaquetesDisponibles();

                return View(oLista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new List<PaqueteViaje>());
            }
        }


        [HttpGet]
        public ActionResult Consultar(int pIdPaquete)
        {
            try
            {
                PaqueteViaje oPaquete =
                    LogicaPaqueteViaje.Buscar(pIdPaquete);

                if (oPaquete != null)
                {
                    return View(oPaquete);
                }
                else
                {
                    throw new Exception(
                        "El Paquete de Viaje no existe."
                    );
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(new PaqueteViaje());
            }
        }


        [HttpGet]
        public ActionResult Agregar()
        {
            try
            {
                // Estados
                ViewBag.ListaEstados = new SelectList(
                    LogicaEstado.ListarEstados(),
                    "CodigoEstado",
                    "Nombre"
                );


                // Hospedajes
                ViewBag.ListaHospedajes = new SelectList(
                    LogicaHospedaje.ListarHospedajes(),
                    "CodigoInterno",
                    "Nombre"
                );


                // Servicios
                // Como Servicio tiene clave compuesta,
                // armamos manualmente el Value.
                List<SelectListItem> oListaServicios =
                    new List<SelectListItem>();


                foreach (Servicio unServicio
                    in LogicaServicio.ListarServiciosFuturos())
                {
                    SelectListItem unItem =
                        new SelectListItem();

                    unItem.Value =
                        unServicio.NumeroOmnibus.ToString()
                        + "|"
                        + unServicio.FechaHoraPartida.ToString(
                            "yyyyMMddHHmmss"
                        );

                    unItem.Text =
                        "Ómnibus "
                        + unServicio.NumeroOmnibus.ToString()
                        + " - "
                        + unServicio.FechaHoraPartida.ToString(
                            "dd/MM/yyyy HH:mm"
                        );

                    oListaServicios.Add(unItem);
                }


                ViewBag.ListaServicios = oListaServicios;


                return View(new PaqueteViaje());
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                ViewBag.ListaEstados = new SelectList(
                    new List<Estado>(),
                    "CodigoEstado",
                    "Nombre"
                );

                ViewBag.ListaHospedajes = new SelectList(
                    new List<Hospedaje>(),
                    "CodigoInterno",
                    "Nombre"
                );

                ViewBag.ListaServicios =
                    new List<SelectListItem>();


                return View(new PaqueteViaje());
            }
        }


        [HttpPost]
        public ActionResult Agregar(
            PaqueteViaje pPaqueteViaje,
            string pCodigoEstado,
            string pCodigoHospedaje,
            string pServicioSeleccionado)
        {
            try
            {
                // Buscamos el Estado seleccionado
                Estado oEstado =
                    LogicaEstado.Buscar(pCodigoEstado);


                // Buscamos el Hospedaje seleccionado
                Hospedaje oHospedaje =
                    LogicaHospedaje.Buscar(pCodigoHospedaje);


                // Verificamos que haya seleccionado Servicio
                if (string.IsNullOrEmpty(pServicioSeleccionado))
                {
                    throw new Exception(
                        "Debe seleccionar un Servicio."
                    );
                }


                // El Value llega por ejemplo:
                // 22|20261005090000
                string[] partes =
                    pServicioSeleccionado.Split('|');


                if (partes.Length != 2)
                {
                    throw new Exception(
                        "El Servicio seleccionado no es válido."
                    );
                }


                // Primera parte de la clave
                int numeroOmnibus =
                    Convert.ToInt32(partes[0]);


                // Segunda parte de la clave
                DateTime fechaHoraPartida =
                    DateTime.ParseExact(
                        partes[1],
                        "yyyyMMddHHmmss",
                        CultureInfo.InvariantCulture
                    );


                // Buscamos el Servicio usando su clave completa
                Servicio oServicio =
                    LogicaServicio.Buscar(
                        numeroOmnibus,
                        fechaHoraPartida
                    );


                if (oEstado == null)
                {
                    throw new Exception(
                        "El Estado seleccionado no existe."
                    );
                }

                if (oHospedaje == null)
                {
                    throw new Exception(
                        "El Hospedaje seleccionado no existe."
                    );
                }

                if (oServicio == null)
                {
                    throw new Exception(
                        "El Servicio seleccionado no existe."
                    );
                }


                // Construimos el paquete completo.
                // El constructor calcula CantidadDias
                // y los tres precios.
                PaqueteViaje oPaqueteViaje =
                    new PaqueteViaje(
                        pPaqueteViaje.Titulo,
                        pPaqueteViaje.Descripcion,
                        pPaqueteViaje.CantidadNoches,
                        oEstado,
                        oServicio,
                        oHospedaje
                    );


                // Validamos en el Controller,
                // como indicó el profesor.
                oPaqueteViaje.Validar();


                // Si es válido, pasa a Lógica
                LogicaPaqueteViaje.Agregar(
                    oPaqueteViaje
                );


                return RedirectToAction(
                    "Listado",
                    "PaqueteViaje"
                );
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;


                // Si hay error hay que volver a cargar
                // los dropdowns porque ViewBag no conserva
                // sus datos después del POST.
                ViewBag.ListaEstados = new SelectList(
                    LogicaEstado.ListarEstados(),
                    "CodigoEstado",
                    "Nombre",
                    pCodigoEstado
                );


                ViewBag.ListaHospedajes = new SelectList(
                    LogicaHospedaje.ListarHospedajes(),
                    "CodigoInterno",
                    "Nombre",
                    pCodigoHospedaje
                );


                List<SelectListItem> oListaServicios =
                    new List<SelectListItem>();


                foreach (Servicio unServicio
                    in LogicaServicio.ListarServiciosFuturos())
                {
                    string valor =
                        unServicio.NumeroOmnibus.ToString()
                        + "|"
                        + unServicio.FechaHoraPartida.ToString(
                            "yyyyMMddHHmmss"
                        );


                    SelectListItem unItem =
                        new SelectListItem();

                    unItem.Value = valor;

                    unItem.Text =
                        "Ómnibus "
                        + unServicio.NumeroOmnibus.ToString()
                        + " - "
                        + unServicio.FechaHoraPartida.ToString(
                            "dd/MM/yyyy HH:mm"
                        );


                    if (valor == pServicioSeleccionado)
                    {
                        unItem.Selected = true;
                    }


                    oListaServicios.Add(unItem);
                }


                ViewBag.ListaServicios =
                    oListaServicios;


                return View(pPaqueteViaje);
            }
        }


        [HttpGet]
        public ActionResult PaquetesPorServicio(
            int pNumeroOmnibus,
            DateTime pFechaHoraPartida)
        {
            try
            {
                List<PaqueteViaje> oLista =
                    LogicaPaqueteViaje.ListarPaquetesPorServicio(
                        pNumeroOmnibus,
                        pFechaHoraPartida
                    );

                return View(oLista);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(
                    new List<PaqueteViaje>()
                );
            }
        }
    }
}