FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 10000

# Configura la app para escuchar en todos los interfaces
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 👇 Copia el archivo .csproj al subdirectorio correspondiente
COPY ["Uttt.Micro.Libro/Uttt.Micro.Libro.csproj", "Uttt.Micro.Libro/"]

# 👇 Restauración usando la ruta correcta
RUN dotnet restore "./Uttt.Micro.Libro/Uttt.Micro.Libro.csproj"

# 👇 Copiar el resto del código
COPY . .

# 👇 Ir a la carpeta del proyecto antes de compilar
WORKDIR "/src/Uttt.Micro.Libro"
RUN dotnet build "Uttt.Micro.Libro.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Uttt.Micro.Libro.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uttt.Micro.Libro.dll"]
