#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ["LOC.DataAccess/LOC.DataAccess.csproj", "LOC.DataAccess/"]
COPY ["LOC.Services/LOC.Services.csproj", "LOC.Services/"]
COPY ["LOC.Utilities/LOC.Utilities.csproj", "LOC.Utilities/"]
COPY ["LOC.Entities/LOC.Entities.csproj", "LOC.Entities/"]
COPY ["LOC.API/LOC.API.csproj", "LOC.API/"]
RUN dotnet restore "LOC.API/LOC.API.csproj"
COPY . .
WORKDIR "/app/LOC.API"
RUN dotnet build "LOC.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LOC.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LOC.API.dll"]