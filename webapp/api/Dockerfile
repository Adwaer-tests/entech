FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /api
EXPOSE 5000
EXPOSE 5443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ["./host/host.csproj", "host/"]

RUN dotnet restore "host/host.csproj"

COPY . .
RUN dotnet build "host/host.csproj" -c Release -o build

FROM build AS publish
RUN dotnet publish "host/host.csproj" -c Release -o publish

FROM base AS final
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "host.dll"]
