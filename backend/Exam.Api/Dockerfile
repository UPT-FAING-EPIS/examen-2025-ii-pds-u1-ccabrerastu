FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Exam.Api/Exam.Api.csproj", "Exam.Api/"]
RUN dotnet restore "Exam.Api/Exam.Api.csproj"
COPY . .
WORKDIR "/src/Exam.Api"
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Exam.Api.dll"]