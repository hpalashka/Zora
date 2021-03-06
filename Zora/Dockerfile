#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Zora/Zora.Web.csproj", "Zora/"]
COPY ["Students/Zora.Students.Application/Zora.Students.Application.csproj", "Students/Zora.Students.Application/"]
COPY ["Shared/Zora.Shared.Application/Zora.Shared.Application.csproj", "Shared/Zora.Shared.Application/"]
COPY ["Students/Zora.Students.Domain/Zora.Students.Domain.csproj", "Students/Zora.Students.Domain/"]
COPY ["Shared/Zora.Shared.Domain/Zora.Shared.Domain.csproj", "Shared/Zora.Shared.Domain/"]
COPY ["Shared/Zora.Shared.Web/Zora.Shared.Web.csproj", "Shared/Zora.Shared.Web/"]
COPY ["Payments/Zora.Payments.Application/Zora.Payments.Application.csproj", "Payments/Zora.Payments.Application/"]
COPY ["Payments/Zora.Payments.Domain/Zora.Payments.Domain.csproj", "Payments/Zora.Payments.Domain/"]
COPY ["Zora.EmailService/Zora.EmailService.csproj", "Zora.EmailService/"]
COPY ["Shared/Zora.Shared.Infrastucture/Zora.Shared.Infrastructure.csproj", "Shared/Zora.Shared.Infrastucture/"]
RUN dotnet restore "Zora/Zora.Web.csproj"
COPY . .
WORKDIR "/src/Zora"
RUN dotnet build "Zora.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.Web.dll"]