FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["api.csproj", "."]
RUN dotnet restore "./api.csproj"

COPY . .

ARG SQL_SERVER_CONNECTION_STRING
ARG JWT_USER_SECRET
ARG COMMUNICATION_SERVICES_CONNECTION_STRING
ARG COMMUNICATION_SERVICES_FROM_EMAIL

ENV SQL_SERVER_CONNECTION_STRING=$SQL_SERVER_CONNECTION_STRING
ENV JWT_USER_SECRET=$JWT_USER_SECRET
ENV COMMUNICATION_SERVICES_CONNECTION_STRING=$COMMUNICATION_SERVICES_CONNECTION_STRING
ENV COMMUNICATION_SERVICES_FROM_EMAIL=$COMMUNICATION_SERVICES_FROM_EMAIL

RUN dotnet build "./api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=https://+:443;http://+:80
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=123
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/etc/pfx/360area.pfx

ENTRYPOINT ["dotnet", "api.dll"]


