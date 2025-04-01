FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app/out/ . 

ENV ConnectionStrings__DefaultConnection="Data Source=SIDONYA\\SQLEXPRESS;Initial Catalog=todolist;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

EXPOSE 80

ENTRYPOINT ["dotnet", "api.dll"]