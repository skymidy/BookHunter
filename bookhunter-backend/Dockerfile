#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["bookhunter-frontend/nuget.config", "bookhunter-frontend/"]
COPY ["bookhunter-backend/bookhunter-backend.csproj", "bookhunter-backend/"]
COPY ["BookHunter-Frontend/bookhunter-frontend.esproj", "BookHunter-Frontend/"]
RUN dotnet restore "bookhunter-backend/bookhunter-backend.csproj"
COPY . .
WORKDIR "/src/bookhunter-backend"
RUN dotnet build "bookhunter-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bookhunter-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bookhunter-backend.dll"]