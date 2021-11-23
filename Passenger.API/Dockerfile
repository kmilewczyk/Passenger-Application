FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Passenger.Api/Passenger.Api.csproj", "Passenger.Api/"]
RUN dotnet restore "Passenger.Api/Passenger.Api.csproj"
COPY . .
WORKDIR "/src/Passenger.Api"
RUN dotnet build "Passenger.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Passenger.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Passenger.Api.dll"]
