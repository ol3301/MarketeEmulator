﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["ProjectsApi/ProjectsApi.csproj", "ProjectsApi/"]
COPY ["Postgres/Postgres.csproj", "Postgres/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["UseCases/UseCases.csproj", "UseCases/"]
COPY ["Mongo/Mongo.csproj", "Mongo/"]
RUN dotnet restore "ProjectsApi/ProjectsApi.csproj"
COPY . .
WORKDIR "/src/ProjectsApi"
RUN dotnet build "ProjectsApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProjectsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectsApi.dll"]
