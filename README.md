# TareasAPI-Ventus

\# 🚀 SQL Server 2019 en Docker (Windows)



Este documento explica cómo: 

\- descargar la imagen oficial de \*\*SQL Server 2019\*\* y crear un contenedor SQL Server en Docker sobre Windows.

\- correr y conectar el proyecto a SQL Server

\---



\## 📋 Requisitos previos

\- Tener instalado \[Docker Desktop](https://www.docker.com/products/docker-desktop).

\- Tener instalado \[SQL Server Management Studio (SSMS)](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) para conectarse a SQL Server.

\- PowerShell o Símbolo del sistema.

\- Clonar el proyecto, y abrirlo con Visual Studio

\---





\## 🛠️ Pasos para configurar SQL Server 2019



Entra a la terminal de Windows, a cmd o Powershell

\### 1. Descargar la imagen de SQL Server 2019

Ejecuta el siguiente comando para obtener la imagen oficial desde el registro de Microsoft:



docker pull mcr.microsoft.com/mssql/server:2019-latest



\### 2. Verificar que la imagen se descargó

docker images



Deberías ver mcr.microsoft.com/mssql/server:2019-latest en la lista.



\### 3. Crear el contenedor

docker run -e "ACCEPT\_EULA=Y" -e "SA\_PASSWORD=TuPasswordSegura123!" `

\-p 1433:1433 --name sql2019 `

\-d mcr.microsoft.com/mssql/server:2019-latest





ACCEPT\_EULA=Y: Acepta la licencia de SQL Server.

SA\_PASSWORD: Contraseña del usuario sa (mínimo 8 caracteres, con mayúsculas, minúsculas, números y símbolos).

\-p 1433:1433: Expone el puerto 1433.

\--name sql2019: Nombre del contenedor.

\-d: Ejecuta en segundo plano.



\### 4. Verificar que está corriendo

docker ps



\### 5. Conectarse al servidor

Con SQL Server Management Studio (SSMS):



Servidor: localhost o 1433

Usuario: sa

Contraseña: la que definiste, en este caso seria TuPasswordSegura123!



Después de conectarte, en el Explorador de objetos, haz clic derecho sobre el localhost, 

y elige la opción "Nueva Consulta"(o New Query, según el lenguaje de tu SSMS)



Te abrirá una "Hoja de Trabajo"



\### 6. Correr script de init.sql

Primero, abre archivo init.sql que se encuentra en carpeta TareasAPI del proyecto

Segundo, copia el contenido

Tercero, pega el contenido en la "Hoja de Trabajo" del SSMS

Cuarto, clic en "Ejecutar"



\### 7. Correr proyecto TareasAPI-Ventus

Primero, abre el proyecto clonado desde Visual Studio, y esperar a que se configure

Segundo, el proyecto ya cuenta con la configuración necesaria para conectarse al contenedor

Finalmente, correr el proyecto





