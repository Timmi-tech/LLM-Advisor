# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5043
ENV ASPNETCORE_URLS=http://+:5043

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy solution and project files
COPY ["LLM-Advisor.sln", "."]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restore dependencies for ALL projects
RUN dotnet restore "Presentation/Presentation.csproj"
RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Application/Application.csproj"
RUN dotnet restore "Infrastructure/Infrastructure.csproj"

# Copy the rest of the source code
COPY . .

# Build Presentation (which references the others)
WORKDIR "/src/Presentation"
RUN dotnet build "Presentation.csproj" -c $configuration -o /app/build

# Publish stage
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Presentation.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
