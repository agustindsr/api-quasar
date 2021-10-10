#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Meli.Quasar.Api/Meli.Quasar.Api.csproj", "Meli.Quasar.Api/"]
COPY ["Meli.Quasar.Repository/Meli.Quasar.Repository.csproj", "Meli.Quasar.Repository/"]
COPY ["Meli.Quasar.DataAccess.Interface/Meli.Quasar.DataAccess.Interface.csproj", "Meli.Quasar.DataAccess.Interface/"]
COPY ["Meli.Quasar.Domain/Meli.Quasar.Domain.csproj", "Meli.Quasar.Domain/"]
COPY ["Meli.Quasar.Common/Meli.Quasar.Common.csproj", "Meli.Quasar.Common/"]
COPY ["Meli.Quasar.Service.Interface/Meli.Quasar.Service.Interface.csproj", "Meli.Quasar.Service.Interface/"]
COPY ["Meli.Quasar.Service/Meli.Quasar.Service.csproj", "Meli.Quasar.Service/"]
RUN dotnet restore "Meli.Quasar.Api/Meli.Quasar.Api.csproj"
COPY . .
WORKDIR "/src/Meli.Quasar.Api"
RUN dotnet build "Meli.Quasar.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meli.Quasar.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meli.Quasar.Api.dll"]