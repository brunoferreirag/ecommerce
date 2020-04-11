#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Ecommerce.Api.Customers/Ecommerce.Api.Customers.csproj", "Ecommerce.Api.Customers/"]
RUN dotnet restore "Ecommerce.Api.Customers/Ecommerce.Api.Customers.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api.Customers"
RUN dotnet build "Ecommerce.Api.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Customers.dll"]
