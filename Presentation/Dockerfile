FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5043

ENV ASPNETCORE_URLS=http://+:5043
USER app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy solution and project files
COPY ["MySolution.sln", "."]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restore all projects
RUN dotnet restore "LLM-Advisor.sln"

# Copy the rest of the source code
COPY . .

WORKDIR "/src/Presentation"
RUN dotnet build "Presentation.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Presentation.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
