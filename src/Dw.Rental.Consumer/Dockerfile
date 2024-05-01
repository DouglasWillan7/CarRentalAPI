FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


# Copy the project files and restore dependencies
COPY src/Dw.Rental.Consumer/*.csproj ./Dw.Rental.Consumer/
COPY src/DW.Rental.Domain/*.csproj ./DW.Rental.Domain/
COPY src/DW.Rental.Data/*.csproj ./DW.Rental.Data/
COPY src/DW.Rental.Shareable/*.csproj ./DW.Rental.Shareable/
COPY src/DW.Rental.IoC/*.csproj ./DW.Rental.IoC/
COPY src/DW.Rental.Bucket/*.csproj ./DW.Rental.Bucket/

# Restore NuGet packages
RUN dotnet restore ./Dw.Rental.Consumer/Dw.Rental.Consumer.csproj

# Copy the entire solution directory
COPY src/ .
WORKDIR /app/Dw.Rental.Consumer
RUN dotnet build -c Release -o /app/build

# Build the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dw.Rental.Consumer.dll"]