#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Payments/Zora.Payments.Startups/Zora.Payments.Startups.csproj", "Payments/Zora.Payments.Startups/"]
COPY ["Payments/Zora.Payments.Infrastructure/Zora.Payments.Infrastructure.csproj", "Payments/Zora.Payments.Infrastructure/"]
COPY ["Payments/Zora.Payments.Domain/Zora.Payments.Domain.csproj", "Payments/Zora.Payments.Domain/"]
COPY ["Shared/Zora.Shared.Domain/Zora.Shared.Domain.csproj", "Shared/Zora.Shared.Domain/"]
COPY ["Payments/Zora.Payments.Application/Zora.Payments.Application.csproj", "Payments/Zora.Payments.Application/"]
COPY ["Shared/Zora.Shared.Application/Zora.Shared.Application.csproj", "Shared/Zora.Shared.Application/"]
COPY ["Students/Zora.Students.Domain/Zora.Students.Domain.csproj", "Students/Zora.Students.Domain/"]
COPY ["Shared/Zora.Shared.Infrastucture/Zora.Shared.Infrastructure.csproj", "Shared/Zora.Shared.Infrastucture/"]
COPY ["Zora.Identity/Zora.Identity.csproj", "Zora.Identity/"]
COPY ["Shared/Zora.Shared.Web/Zora.Shared.Web.csproj", "Shared/Zora.Shared.Web/"]
COPY ["Payments/Zora.Payments.Web/Zora.Payments.Web.csproj", "Payments/Zora.Payments.Web/"]
RUN dotnet restore "Payments/Zora.Payments.Startups/Zora.Payments.Startups.csproj"
COPY . .
WORKDIR "/src/Payments/Zora.Payments.Startups"
RUN dotnet build "Zora.Payments.Startups.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.Payments.Startups.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.Payments.Startups.dll"]