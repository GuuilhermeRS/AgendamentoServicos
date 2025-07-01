FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AgendamentoServicos.Api/AgendamentoServicos.Api.csproj", "AgendamentoServicos.Api/"]
COPY ["AgendamentoServicos.Core/AgendamentoServicos.Core.csproj", "AgendamentoServicos.Core/"]
RUN dotnet restore "AgendamentoServicos.Api/AgendamentoServicos.Api.csproj"
COPY . .
WORKDIR "/src/AgendamentoServicos.Api"
RUN dotnet build "AgendamentoServicos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AgendamentoServicos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendamentoServicos.Api.dll"]
