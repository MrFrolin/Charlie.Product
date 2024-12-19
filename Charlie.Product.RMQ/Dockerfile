# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

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