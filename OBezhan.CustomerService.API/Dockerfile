FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY OBezhan.CustomerService.API/OBezhan.CustomerService.API.csproj OBezhan.CustomerService.API/
RUN dotnet restore OBezhan.CustomerService.API/OBezhan.CustomerService.API.csproj

COPY . .
WORKDIR /src/OBezhan.CustomerService.API
RUN dotnet build OBezhan.CustomerService.API.csproj --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish OBezhan.CustomerService.API.csproj --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OBezhan.CustomerService.API.dll"]