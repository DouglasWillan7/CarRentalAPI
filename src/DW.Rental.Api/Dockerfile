FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


# Copy the project files and restore dependencies
COPY src/DW.Rental.Api/*.csproj ./DW.Rental.Api/
COPY src/DW.Rental.Domain/*.csproj ./DW.Rental.Domain/
COPY src/DW.Rental.Data/*.csproj ./DW.Rental.Data/
COPY src/DW.Rental.Shareable/*.csproj ./DW.Rental.Shareable/
COPY src/DW.Rental.IoC/*.csproj ./DW.Rental.IoC/
COPY src/DW.Rental.Bucket/*.csproj ./DW.Rental.Bucket/

# Restore NuGet packages
RUN dotnet restore ./DW.Rental.Api/DW.Rental.Api.csproj

# Copy the entire solution directory
COPY src/ .
WORKDIR /app/DW.Rental.Api
RUN dotnet build -c Release -o /app/build

# Build the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DW.Rental.Api.dll"]