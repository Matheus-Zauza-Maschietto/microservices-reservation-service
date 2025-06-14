FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
EXPOSE 5081
ENV ASPNETCORE_URLS=http://+:5081

COPY . ./

RUN dotnet restore

RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "ReservationsService.Api.dll"]