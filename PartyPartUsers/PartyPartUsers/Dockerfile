FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY bin/Release/netcoreapp3.1/publish/ PartyPartUsers/
WORKDIR /PartyPartUsers
ENTRYPOINT ["dotnet", "PartyPartUsers.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /PartyPartUsers

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /PartyPartUsers
COPY --from=build-env /PartyPartUsers/out .
ENTRYPOINT ["dotnet", "PartyPartUsers.dll"]