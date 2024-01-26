FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Shnakeus/Shnakeus.csproj", "Shnakeus/"]
RUN dotnet restore "Shnakeus/Shnakeus.csproj"
COPY . .
WORKDIR "/src/Shnakeus"
RUN dotnet build "Shnakeus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shnakeus.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shnakeus.dll"]
