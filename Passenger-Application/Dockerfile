FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Passenger-Application/Passenger-Application.csproj", "Passenger-Application/"]
RUN dotnet restore "Passenger-Application/Passenger-Application.csproj"
COPY . .
WORKDIR "/src/Passenger-Application"
RUN dotnet build "Passenger-Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Passenger-Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Passenger-Application.dll"]
