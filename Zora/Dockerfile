#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Zora/Zora.Web.csproj", "Zora/"]
COPY ["Zora.EmailService/Zora.EmailService.csproj", "Zora.EmailService/"]
COPY ["Zora.Shared/Zora.Shared.csproj", "Zora.Shared/"]
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