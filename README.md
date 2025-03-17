# Gestión de Facturas - [Tu Nombre y Apellido]

## Descripción

Esta aplicación web ASP.NET Core MVC permite la gestión de facturas, incluyendo la creación, lectura, actualización y eliminación de registros. Además, calcula y muestra el estado de cada factura según su fecha de vencimiento (Vigente, Próxima a vencer, Vencida).

## Tecnologías Utilizadas

* **Backend:**
    * ASP.NET Core 8 MVC
    * C#
    * SQL Server Express 2022
    * SqlClient
* **Frontend:**
    * HTML/CSS
    * DataTables
    * Google Material Icons
    * Sweet Alert

## Requisitos Previos

* .NET Core 8 SDK
* SQL Server Express 2022

## Configuración de la Base de Datos

1.  Importa el script `sql_invoices_management.sql` en tu instancia de SQL Server para crear la base de datos y las tablas necesarias.
2.  Abre el archivo `appsettings.json` en la raíz del proyecto.
3.  Modifica la cadena de conexión SQL para que coincida con la configuración de tu instancia de SQL Server.

    ```json
    "ConnectionStrings": {
        "sql": "Server=Duvan\\SQLSERVER;Database=invoicesManagement;User Id=sa;Password=sqlserver;Encrypt=False;"
    },
    ```

## Ejecución de la Aplicación

1.  Clona este repositorio en tu máquina local.
2.  Abre el proyecto en Visual Studio o tu editor de código preferido.
3.  Asegúrate de que la cadena de conexión en `appsettings.json` esté configurada correctamente.
4.  Ejecuta la aplicación e ingresa al puerto correspondiente.

## Uso de la Aplicación

* **Lista de Facturas:**
    * Al iniciar la aplicación, se muestra una tabla con todas las facturas registradas.
    * La tabla incluye información como ID, nombre de la factura, número de factura, fecha de vencimiento, total, estado, descripción, fecha de creación y fecha de actualización.
    * Puedes ordenar la tabla por cualquier columna, utilizar la paginación y buscar facturas específicas mediante el input de busqueda.
    * Cada fila tiene botones para editar y eliminar la factura correspondiente.
    * El campo estado calcula los estados de factura (Vigente, Proxima a vencer, Vencida)
* **Crear Factura:**
    * Haz clic en el botón "Insertar" en la cabecera de la página para acceder al formulario de creación.
    * Completa los campos requeridos (Nombre factura, número de factura, fecha de vencimiento, total factura, descripción).
    * El campo "Total Factura" acepta solo números y muestra el valor en formato de moneda.
    * Se realizan validaciones para evitar números de factura duplicados, campos vacíos y manejo de errores en general, mostrando alertas personalizadas.
    * Al insertar una factura correctamente, se redirigira al index y se mostrará una alerta personalizada
* **Editar Factura:**
    * Haz clic en el botón "Editar" en la fila de la factura que deseas modificar.
    * Realiza los cambios necesarios en el formulario de edición.
    * El campo "Total Factura" tiene la misma funcionalidad que en la vista de creación.
    * Se aplican las mismas validaciones que en la creación.
* **Eliminar Factura:**
    *En esta aplicación, los registros de facturas no se eliminan físicamente de la base de datos. En su lugar, se implementa un **borrado lógico**. 
    Esto significa que cuando se elimina una factura, su estado se actualiza para marcarla como eliminada, pero el registro permanece en la base de datos.
    
    * Haz clic en el botón "Eliminar" en la fila de la factura que deseas eliminar.
    * Confirma la eliminación en la alerta que aparece.
    * Se realizan validaciones y manejo de errores.

## Estructura del Proyecto

* El proyecto sigue el patrón de diseño MVC.
* Se utilizan procedimientos almacenados en la base de datos para la gestión de datos.

## Script de la Base de Datos

* El script para la creacion de la base de datos se compartió en el correo, tiene como nombre `sql_invoices_management.sql`

## Información Adicional

* Todas las vistas y formularios incluyen validaciones para manejar errores y duplicidades.
* Se usan librerias de terceros para mejorar la experiencia del usuario, como datatables, sweet alert, y google material icons.
* Los estilos pueden visualizarse en la ruta /wwwroot/css/site.css ya que no se utilizó bootstrap

## Autor

* Oscar Alvarez