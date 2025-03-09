FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080  

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SABA.web/SABA.web.csproj", "SABA.web/"]
COPY ["SABA.Persistance/SABA.Persistance.csproj", "SABA.Persistance/"]
COPY ["SABA.Core/SABA.Core.csproj", "SABA.Core/"]
COPY ["SABA.Services/SABA.Services.csproj", "SABA.Services/"]
RUN dotnet restore "./SABA.web/./SABA.web.csproj"
COPY . .
WORKDIR "/src/SABA.web"
RUN dotnet build "./SABA.web.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SABA.web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SABA.web.dll"]