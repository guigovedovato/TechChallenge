FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy everything from the current directory to the /src directory in the container
COPY ./ ./
# restore the dependencies
RUN dotnet restore "FCG.sln"
# build the solution
RUN dotnet build "FCG.sln" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /api
# copy the build output from the previous stage
COPY --from=build /src/out .
# expose the port the api runs on
EXPOSE 8080
# set the entry point for the container
ENTRYPOINT ["dotnet", "FCG.API.dll"]