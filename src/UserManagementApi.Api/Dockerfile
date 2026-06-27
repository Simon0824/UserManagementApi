FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5249

ENV ASPNETCORE_URLS=http://+:5249

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/UserManagementApi.Api/UserManagementApi.Api.csproj", "src/UserManagementApi.Api/"]
RUN dotnet restore "src/UserManagementApi.Api/UserManagementApi.Api.csproj"
COPY . .
WORKDIR "/src/src/UserManagementApi.Api"
RUN dotnet build "UserManagementApi.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "UserManagementApi.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagementApi.Api.dll"]
