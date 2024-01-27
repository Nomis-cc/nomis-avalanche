#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Directory.Packages.props", "src/"]
COPY ["src/Directory.Build.props", "src/"]
COPY ["src/Directory.Build.targets", "src/"]
COPY ["src/Common/Nomis.Web.Server.Common/Nomis.Web.Server.Common.csproj", "src/Common/Nomis.Web.Server.Common/"]
RUN dotnet restore "./src/Common/Nomis.Web.Server.Common/./Nomis.Web.Server.Common.csproj"
COPY . .
WORKDIR "/src/src/Common/Nomis.Web.Server.Common"
RUN dotnet build "./Nomis.Web.Server.Common.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Nomis.Web.Server.Common.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nomis.Web.Server.Common.dll"]