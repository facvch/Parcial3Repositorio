# Parcial 3 - Programacion Aplicada II - AUSI 3ro ESCMB 2025
Proyecto parcial 3 para la materia Programacion aplicada 2, que permite jugar al juego de "Picas y Famas", usa una API Rest para el backend y Blazor Web Assembly para el frontend. 

# Como ejecutar el proyecto

## Frontend

1. Abrir el archivo "PicasYFamas.BlazorApp.sln" que esta en la carpeta Frontend del repositorio. 
2. Abrir la terminal sobre el proyecto (en VisualStudio 2022) y ejecutar **dotnet restore**. 
3. En la misma terminal ejecutar **dotnet build** (Para asegurarse que la restauracion se realizo sin errores)
4. Ejecutar el proyecto mediante el comando **dotnet run** y posteriormente accedemos a la URL http://localhost:7402 (Creo, si no fijate en la consola que puerto esta usando)

## Backend

1. Abrir el proyecto ingresando a la carpeta Backend y ejecutando el archivo "HybridDDDArchitecture.sln".
2. Abrir la terminal sobre el proyecto y restaurar los packetes Nugets del proyecto con **dotnet restore**. 
3. En la terminal realizar un Rebuild del proyecto con **dotnet build** (Para asegurarse que la restauracion se realizo sin errores)
4. Configurar la conexion a la base de datos. Si estamos utilizando SQL Server, SQLite, MySQL, MariaDB o Postgres, asegurarse de ejecutar las migraciones correspondientes con EF.
4. 1. Si estas usando SQLite (recomendado) ejecutá: 
4. 1. 1. **dotnet ef migrations add InitialCreate --project ../Infrastructure --startup-project ../Template-API --context Infrastructure.Repositories.Sql.StoreDbContext --output-dir Migrations**
4. 1. 2. **dotnet ef database update --project ../Infrastructure --startup-project ../Template-API --context Infrastructure.Repositories.Sql.StoreDbContext**
4. 2. Si estas usando alguna otra no se que decirte, podés agregar la info acá si lo haces funcionar. 

## Recomendaciones

1. Si vas a usar SQLite vas a tener que instalar **"SQLite and SQLServer Compact Toolbox"** (versión probada v4.9.46). Esto te permite ver las bases de datos .db
2. Si vas a hacer los graficos para PowerBI vas a tener que instalar **"Devart ODBC SQLite"**, es pago pero hay un trial mensual(suficiente para el parcial)
