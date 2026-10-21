# Agencia de Viajes — Proyecto Final BIOS

Proyecto académico realizado por Romina, Javier y Gastón en la carrera de Analista de Sistemas.

**[Ver demostración](https://proyectoviajes.somee.com)**

> Demo educativa en hosting gratuito. Utilizar solamente datos de prueba.

## Tecnologías
- C# y .NET Framework 4.5.2
- ASP.NET MVC 5 y Razor
- Arquitectura en cuatro capas
- SQL Server 2019, ADO.NET y procedimientos almacenados
- Bootstrap 3, jQuery, HTML y CSS
- IIS y despliegue FTP en Somee

## Arquitectura
- `EntidadesCompartidas`: entidades del dominio.
- `Persistencia`: operaciones SQL mediante ADO.NET.
- `Logica`: reglas de negocio.
- `PaquetesViajes`: interfaz ASP.NET MVC (controladores y vistas).

## Funcionalidades
- Altas, consultas, modificaciones, eliminaciones y listados de Estados, Hospedajes, Servicios y Paquetes de Viaje.
- Consultas de paquetes y hospedajes por estado.
- Menú responsive para dispositivos móviles.

## Base de datos
Cuatro tablas principales: `Estados`, `Hospedajes`, `Servicios` y `PaquetesViaje`. Se verificaron 22 procedimientos almacenados y 43 registros de prueba durante la preparación del despliegue.

## Ejecución local
1. Abrir la solución en Visual Studio.
2. Crear la base de datos local en SQL Server con los scripts del proyecto.
3. Configurar la cadena `ConexionBD` en un `Web.config` local (no incluido en el repositorio).
4. Compilar y ejecutar con IIS Express.

## Seguridad
**Nunca subir el `Web.config` de producción ni contraseñas de Somee.** Consultar [SEGURIDAD-Y-DESPLIEGUE.md](SEGURIDAD-Y-DESPLIEGUE.md).

La aplicación es una demostración educativa, no un sistema de producción.
