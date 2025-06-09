FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5081

ENV ASPNETCORE_URLS=http://+:5081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ReservationService.Api/ReservationService.Api.csproj", "ReservationService.Api/"]
RUN dotnet restore "ReservationService.Api/ReservationService.Api.csproj"
COPY . .
WORKDIR "/src/ReservationService.Api"
RUN dotnet build "ReservationService.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ReservationService.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReservationService.Api.dll"]
