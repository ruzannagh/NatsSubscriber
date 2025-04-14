FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and projects
COPY NatsSubscriber.sln .
COPY src/API/NatsSubscriber.csproj src/API/
COPY src/Business/BL.NatsSubscriber.csproj src/Business/
COPY src/Data/Data.NatsSubscriber.csproj src/Data/
RUN dotnet restore "NatsSubscriber.sln"

# Copy everything else
COPY . .

# Publish from correct directory with full path
WORKDIR "/src/API"
RUN dotnet publish "/src/API/NatsSubscriber.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]