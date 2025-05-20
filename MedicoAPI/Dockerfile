# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY MedicoAPI.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Optional: specify exposed port for documentation (e.g., 8080)
EXPOSE 8080

# Optional: ensure ASP.NET Core binds to all IPs and correct port
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MedicoAPI.dll"]
