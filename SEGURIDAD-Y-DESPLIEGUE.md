# Seguridad y despliegue

## Antes de subir el código
- No publicar `Web.config`, `Web.Release.config` ni archivos que contengan contraseñas.
- Buscar cadenas como `Password=`, `User ID=`, `connectionString` y claves FTP en todo el proyecto.
- Si una clave se publicó accidentalmente, rotarla. Borrarla en un commit no la elimina del historial.
- No subir `bin`, `obj`, `.vs` ni respaldos con datos.

## Publicar en Somee
1. Respaldar privadamente el `Web.config` del servidor.
2. Compilar y publicar Release.
3. Comprobar si el FTP sobrescribió `Web.config` y restaurar la conexión remota desde Somee si es necesario.
4. Verificar HTTPS, menús y consultas SQL.
5. No compartir operaciones de modificación de datos reales mientras no exista autenticación.

`TrustServerCertificate=True` omite la validación de identidad del servidor SQL. Es una solución temporal de laboratorio, no la configuración aconsejada para producción.