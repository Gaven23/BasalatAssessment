#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BasalatAssessment/BasalatAssessment.Vehicle.csproj", "BasalatAssessment/"]
COPY ["BasalatAssessment.Domain/BasalatAssessment.Domain.csproj", "BasalatAssessment.Domain/"]
COPY ["BasalatAssessment.Shared/BasalatAssessment.Common.csproj", "BasalatAssessment.Shared/"]
COPY ["BasalatAssessment.Stock.Data/BasalatAssessment.Stock.Data.csproj", "BasalatAssessment.Stock.Data/"]
COPY ["BasalatAssessment.Vehicle.Data.Tracking/BasalatAssessment.Vehicle.Data.Tracking.csproj", "BasalatAssessment.Vehicle.Data.Tracking/"]
COPY ["BasalatAssessment.Vehicle.Data/BasalatAssessment.Vehicle.Data.csproj", "BasalatAssessment.Vehicle.Data/"]
RUN dotnet restore "./BasalatAssessment/BasalatAssessment.Vehicle.csproj"
COPY . .
WORKDIR "/src/BasalatAssessment"
RUN dotnet build "./BasalatAssessment.Vehicle.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BasalatAssessment.Vehicle.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasalatAssessment.Vehicle.dll"]