#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Zora.OutstandingPayments/Zora.OutstandingPayments.csproj", "Zora.OutstandingPayments/"]
COPY ["Zora.Shared/Zora.Shared.csproj", "Zora.Shared/"]
RUN dotnet restore "Zora.OutstandingPayments/Zora.OutstandingPayments.csproj"
COPY . .
WORKDIR "/src/Zora.OutstandingPayments"
RUN dotnet build "Zora.OutstandingPayments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.OutstandingPayments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.OutstandingPayments.dll"]