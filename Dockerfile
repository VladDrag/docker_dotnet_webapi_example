FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#BE SURE TO SET THE CORRECT PORT FOR DOTNET APPLICATIONS IN THE >-----RUNTIME------< ENV
FROM mcr.microsoft.com/dotnet/sdk:7.0
ENV DOTNET_URLS=http://+:5000

WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "VaultBrecher.dll"]

# docker-compose up --build