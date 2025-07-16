FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR src
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["UsersApi/UsersApi.csproj", "UsersApi/"]
COPY ["UsersDatabase/UsersDatabase.csproj", "UsersDatabase/"]
COPY ["UsersDomain/UsersDomain.csproj", "UsersDomain/"]
COPY ["SharedDomain/SharedDomain.csproj", "SharedDomain/"]
COPY ["UsersApplication/UsersApplication.csproj", "UsersApplication/"]
RUN dotnet restore "UsersApi/UsersApi.csproj"
COPY . .
WORKDIR "/src/UsersApi"
RUN dotnet build "UsersApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UsersApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersApi.dll"]