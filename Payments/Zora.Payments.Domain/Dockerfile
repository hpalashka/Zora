#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Payments/Zora.Payments.Domain/Zora.Payments.Domain.csproj", "Payments/Zora.Payments.Domain/"]
COPY ["Shared/Zora.Shared.Domain/Zora.Shared.Domain.csproj", "Shared/Zora.Shared.Domain/"]
RUN dotnet restore "Payments/Zora.Payments.Domain/Zora.Payments.Domain.csproj"
COPY . .
WORKDIR "/src/Payments/Zora.Payments.Domain"
RUN dotnet build "Zora.Payments.Domain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.Payments.Domain.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.Payments.Domain.dll"]