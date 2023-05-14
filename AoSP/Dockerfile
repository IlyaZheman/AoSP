FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AoSP/AoSP.csproj", "AoSP/"]
RUN dotnet restore "AoSP/AoSP.csproj"
COPY . .
WORKDIR "/src/AoSP"
RUN dotnet build "AoSP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AoSP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AoSP.dll"]
