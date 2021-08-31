# Lista de Comandos

dotnet new sln -n curso-net

dotnet new -i MSBuild.Sdk.SqlProj.Templates

dotnet new sqlproj -n app-db

dotnet new webapi -n web-api

dotnet build

cd web-api

dotnet run

### Crear certificado para desarrollo

dotnet dev-certs https --trust

dotnet publish -o C:\pub


### Script crear cliente
cd app-db/

dotnet new table -o model -n Cliente

dotnet new -i MSBuild.Sdk.SqlProj.Templates (por si no funciona)

dotnet new table -o model -n Cuenta

dotnet new table -o model -n Produto

dotnet new table -o model -n Cartera

dotnet new table -o model -n Precio

#Herramientas para usar entity Identity

dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

dotnet tool install --global dotnet-ef

dotnet tool update --global dotnet-ef

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add "Modelo Auth" --context AuthDbContext --output-dir "Migrations/Auth"

dotnet ef database update -c AuthDbContext

### Esto es para deshacer ultima modificacion

dotnet ef database update "Nombre de la ultima migracion buena" -c AuthDbContext
