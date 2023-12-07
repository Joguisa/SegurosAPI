# SegurosAPI
Este backend proporciona servicios RESTful para consultar clientes, seguros y la relación entre ellos. También permite cargar datos masivos desde un archivo Excel o TXT a la tabla de clientes y ofrece consultas para obtener información sobre seguros asociados a un cliente o asegurados asociados a un seguro.

## Estructura del Proyecto

La estructura del proyecto está organizada en las siguientes carpetas:

- `Controllers/`: Controladores que gestionan los endpoints de la API.
- `DTOs/`: Objetos de transferencia de datos para definir la estructura de datos que se envía y recibe.
- `Models/`: Modelos de datos que representan las entidades de la base de datos.
- `Services/`: Servicios que implementan la lógica de negocio.
  - `Contrato/`: Interfaces que definen los contratos para los servicios.
  - `Implementación/`: Implementaciones concretas de los servicios.

## Requisitos del Sistema

- .NET SDK 6.0

## Configuración del Proyecto

Asegúrate de tener instalado el .NET SDK 6.0 antes de continuar.

1. Clona el repositorio:

```bash
git clone https://github.com/Joguisa/SegurosAPI.git
```
## Dependencias
Asegúrate de restaurar las dependencias antes de ejecutar el proyecto y crear tu database con el script en el repo:
```bash
dotnet restore
```
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore

## Swagger
La documentación de la API está disponible a través de Swagger. Puedes acceder a ella en `https://localhost:5001/swagger`.

## Contribuir
Si deseas contribuir a este proyecto, sigue estos pasos:

- Realiza un fork del proyecto.
- Crea una nueva rama para tus cambios.
- Realiza tus cambios y haz commits.
- Abre un pull request.

## Licencia
Este proyecto está bajo la Licencia ISC.
