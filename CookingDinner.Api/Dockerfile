FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CookingDiner.Api/CookingDiner.Api.csproj", "CookingDiner.Api/"]
RUN dotnet restore "CookingDiner.Api/CookingDiner.Api.csproj"
COPY . .
WORKDIR "/src/CookingDiner.Api"
RUN dotnet build "CookingDiner.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CookingDiner.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookingDiner.Api.dll"]
