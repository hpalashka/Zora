#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Students/Zora.Students.Startup/Zora.Students.Startup.csproj", "Students/Zora.Students.Startup/"]
COPY ["Students/Zora.Students.Application/Zora.Students.Application.csproj", "Students/Zora.Students.Application/"]
COPY ["Shared/Zora.Shared.Application/Zora.Shared.Application.csproj", "Shared/Zora.Shared.Application/"]
COPY ["Students/Zora.Students.Domain/Zora.Students.Domain.csproj", "Students/Zora.Students.Domain/"]
COPY ["Shared/Zora.Shared.Domain/Zora.Shared.Domain.csproj", "Shared/Zora.Shared.Domain/"]
COPY ["Students/Zora.Students.Web/Zora.Students.Web.csproj", "Students/Zora.Students.Web/"]
COPY ["Shared/Zora.Shared.Web/Zora.Shared.Web.csproj", "Shared/Zora.Shared.Web/"]
COPY ["Students/Zora.Students.Infrastructure/Zora.Students.Infrastructure.csproj", "Students/Zora.Students.Infrastructure/"]
COPY ["Shared/Zora.Shared.Infrastucture/Zora.Shared.Infrastructure.csproj", "Shared/Zora.Shared.Infrastucture/"]
RUN dotnet restore "Students/Zora.Students.Startup/Zora.Students.Startup.csproj"
COPY . .
WORKDIR "/src/Students/Zora.Students.Startup"
RUN dotnet build "Zora.Students.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.Students.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.Students.Startup.dll"]