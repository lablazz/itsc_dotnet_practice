# --- Base runtime image ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["itsc_dotnet_practice.csproj", "./"]
RUN dotnet restore "itsc_dotnet_practice.csproj"
COPY . .

RUN dotnet build "itsc_dotnet_practice.csproj" -c $BUILD_CONFIGURATION -o /app/build

# --- Publish stage ---
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "itsc_dotnet_practice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Final image using base ---
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "itsc_dotnet_practice.dll"]
