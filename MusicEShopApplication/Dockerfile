FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY MusicEShopApplication.sln ./

COPY MusicEShop.Web/MusicEShop.Web.csproj MusicEShop.Web/
COPY MusicEShop.Domain/MusicEShop.Domain.csproj MusicEShop.Domain/
COPY MusicEShop.Repository/MusicEShop.Repository.csproj MusicEShop.Repository/
COPY MusicEShop.Service/MusicEShop.Service.csproj MusicEShop.Service/

RUN dotnet restore MusicEShop.Web/MusicEShop.Web.csproj

COPY . .

WORKDIR /src
RUN dotnet publish MusicEShop.Web/MusicEShop.Web.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

RUN adduser --disabled-password --gecos "" appuser
USER appuser

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "MusicEShop.Web.dll"]
