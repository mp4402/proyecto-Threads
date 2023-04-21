FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
#docker run -it --rm --cpus="1.0" --memory="1g" -v C:/GitHub/proyecto-Threads/App/so_respuesta:/App/so_respuesta proyecto
#docker run -it --rm --cpus="1.0" --memory="1g" -v /Users/carlosalvarado/Desktop/SO/proyecto-threads/App/so_respuesta:/App/so_respuesta proyecto