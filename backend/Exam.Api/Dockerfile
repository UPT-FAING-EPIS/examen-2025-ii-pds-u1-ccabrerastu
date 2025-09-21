# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo de proyecto y restaura dependenciasss
COPY ["Exam.Api.csproj", "."]
RUN dotnet restore "Exam.Api.csproj"

# Copia todo el contenido y publica
COPY . .
RUN dotnet publish "Exam.Api.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Exam.Api.dll"]
