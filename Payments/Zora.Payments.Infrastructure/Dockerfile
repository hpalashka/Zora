#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Payments/Zora.Payments.Infrastructure/Zora.Payments.Infrastructure.csproj", "Payments/Zora.Payments.Infrastructure/"]
COPY ["Payments/Zora.Payments.Domain/Zora.Payments.Domain.csproj", "Payments/Zora.Payments.Domain/"]
COPY ["Shared/Zora.Shared.Domain/Zora.Shared.Domain.csproj", "Shared/Zora.Shared.Domain/"]
COPY ["Payments/Zora.Payments.Application/Zora.Payments.Application.csproj", "Payments/Zora.Payments.Application/"]
COPY ["Shared/Zora.Shared.Application/Zora.Shared.Application.csproj", "Shared/Zora.Shared.Application/"]
COPY ["Students/Zora.Students.Domain/Zora.Students.Domain.csproj", "Students/Zora.Students.Domain/"]
COPY ["Shared/Zora.Shared.Infrastucture/Zora.Shared.Infrastructure.csproj", "Shared/Zora.Shared.Infrastucture/"]
RUN dotnet restore "Payments/Zora.Payments.Infrastructure/Zora.Payments.Infrastructure.csproj"
COPY . .
WORKDIR "/src/Payments/Zora.Payments.Infrastructure"
RUN dotnet build "Zora.Payments.Infrastructure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.Payments.Infrastructure.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.Payments.Infrastructure.dll"]