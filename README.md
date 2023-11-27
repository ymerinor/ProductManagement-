# ProductManagement

Bienvenido al proyecto ProductManagement. Este proyecto implementa una solución basada en TDD (Desarrollo Guiado por Pruebas) y sigue la arquitectura hexagonal. La aplicación consta de una API, contenerizada en Docker, y utiliza una base de datos también contenerizada.

## Configuración del Entorno

### Requisitos Previos
Asegúrate de tener instalados los siguientes requisitos previos:
- Docker
- SQL Server Management Studio (u otra herramienta similar) para ejecutar scripts SQL

### Configuración de la Base de Datos
1. Navega a la carpeta `BaseDatos` del proyecto.
2. Ejecuta el siguiente comando en la terminal para iniciar la base de datos en Docker:
    ```bash
    docker-compose up -d
    ```
3. Después de iniciar la base de datos, ejecuta el archivo `ProductManagement.sql` en tu herramienta de administración de SQL para crear la base de datos y la tabla necesaria.

### Configuración del Proyecto API
2. Ejecuta el siguiente comando en la cliente de SQL usando,esto para crear la base de dato y tabla configurada para el proyecto:
    ```bash
    create database ProductManagement;
    CREATE TABLE Products
    (
      ProductId int primary key identity(1,1),
      Name varchar(60),
      StatusName  bit not null,
      Stock decimal not null,
      Description varchar(100),
      Price decimal not null,
      Discount decimal not null,
      FinalPrice decimal not null,
      DateRegistration Datetime,
      DateUpdate Datetime
    );
    ```

## Uso de la API
La API estará disponible en la URL [http://localhost:puerto](http://localhost:puerto), donde "puerto" es el puerto que hayas configurado en tu archivo `docker-compose.yml`.

### Ejecutar Pruebas
El proyecto está diseñado con TDD. Puedes ejecutar las pruebas utilizando tu entorno de desarrollo preferido.

## Contribuciones
¡Contribuciones son bienvenidas! Si encuentras errores o tienes mejoras que sugerir, no dudes en abrir un problema o enviar una solicitud de extracción.
