# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
#USER app
#WORKDIR /app
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["Charlie.Product.RMQ/Charlie.Product.RMQ.csproj", "Charlie.Product.RMQ/"]
#RUN dotnet restore "./Charlie.Product.RMQ/Charlie.Product.RMQ.csproj"
#COPY . .
#WORKDIR "/src/Charlie.Product.RMQ"
#RUN dotnet build "./Charlie.Product.RMQ.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./Charlie.Product.RMQ.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Charlie.Product.RMQ.dll"]

# Use .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything from the Charlie.Order directory
COPY . .

# Print directory contents for debugging
RUN ls -la Charlie.Product.RMQ/
RUN ls -la Charlie.Product.DataAccess/
RUN ls -la Charlie.Product.Shared/

# Restore dependencies
RUN dotnet restore Charlie.Product.RMQ/Charlie.Product.RMQ.csproj

# Build and publish a release
RUN dotnet publish Charlie.Product.RMQ/Charlie.Product.RMQ.csproj -c Release -o out

# Use .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copy the published output from the build stage
COPY --from=build-env /App/out .

# Expose port and set environment variables
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ConnectionStrings__CharlieOrderDb="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"

# Run the application
ENTRYPOINT ["dotnet", "Charlie.Product.RMQ.dll"]