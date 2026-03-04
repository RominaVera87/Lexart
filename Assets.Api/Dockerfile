# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore ./Lexart.sln
RUN dotnet publish ./Assets.Api/Assets.Api.csproj -c Release -o /app/publish

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Importante: esto ayuda a que el loader encuentre .so en /app/native
ENV LD_LIBRARY_PATH=/app/native

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Assets.Api.dll"]