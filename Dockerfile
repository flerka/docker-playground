FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/*.csproj ./TestDocker/
WORKDIR /app/TestDocker
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app/
COPY src/. ./TestDocker/
WORKDIR /app/TestDocker
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/TestDocker/out ./
ENTRYPOINT ["dotnet", "TestDocker.dll"]