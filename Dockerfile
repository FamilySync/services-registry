FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG version
WORKDIR /src

COPY ["FamilySync.Services.Registry/FamilySync.Services.Registry.csproj", "FamilySync.Services.Registry/"]
COPY ["NuGet.config", "FamilySync.Services.Registry/"]

RUN dotnet restore "FamilySync.Services.Registry/FamilySync.Services.Registry.csproj"

COPY . .

RUN dotnet publish "FamilySync.Services.Registry/FamilySync.Services.Registry.csproj" -c Release -o out /p:Version=$version

FROM mcr.microsoft.com/dotnet/aspnet:7.0 
WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY --from=build /src/out .
ENTRYPOINT ["dotnet", "FamilySync.Services.Registry.dll"]