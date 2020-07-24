#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Zora.Identity/Zora.Identity.csproj", "Zora.Identity/"]
COPY ["Zora.Shared/Zora.Shared.csproj", "Zora.Shared/"]
RUN dotnet restore "Zora.Identity/Zora.Identity.csproj"
COPY . .
WORKDIR "/src/Zora.Identity"
RUN dotnet build "Zora.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zora.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zora.Identity.dll"]