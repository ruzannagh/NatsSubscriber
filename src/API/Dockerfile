# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all .csproj files and restore
COPY *.sln .
COPY src/API/*.csproj ./API/
COPY src/Business/*.csproj ./Business/
COPY src/Data/*.csproj ./Data/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]