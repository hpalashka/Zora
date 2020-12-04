#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Zora.Notifications/Zora.Notifications.csproj", "Zora.Notifications/"]
COPY ["Shared/Zora.Shared.Infrastucture/Zora.Shared.Infrastructure.csproj", "Shared/Zora.Shared.Infrastucture/"]
COPY ["Shared/Zora.Shared.Application/Zora.Shared.Application.csproj", "Shared/Zora.Shared.Application/"]
COPY ["Students/Zora.Students.Domain/Zora.Students.Domain.csproj", "Students/Zora.Students.Domain/"]
COPY ["Shared/Zora.Shared.Domain/Zora.Shared.Domain.csproj", "Shared/Zora.Shared.Domain/"]
RUN dotnet restore "Zora.Notifications/Zora.Notifications.csproj"
COPY . .
WORKDIR "/src/Zora.Notifications"
RUN dotnet build "Zora.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.Notifications.dll"]