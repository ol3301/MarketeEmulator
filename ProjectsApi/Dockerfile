FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["ProjectsApi/ProjectsApi.csproj", "ProjectsApi/"]
COPY ["ProjectsDomain/ProjectsDomain.csproj", "ProjectsDomain/"]
COPY ["ProjectsApplication/ProjectsApplication.csproj", "ProjectsApplication/"]
COPY ["SharedDomain/SharedDomain.csproj", "SharedDomain/"]
COPY ["UsersDatabase/UsersDatabase.csproj", "UsersDatabase/"]
COPY ["UsersDomain/UsersDomain.csproj", "UsersDomain/"]
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
