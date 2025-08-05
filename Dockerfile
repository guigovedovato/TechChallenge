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

# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=NRAK-9OO9X582GDGRZ5QPLWOAQMUPYJ7 \
NEW_RELIC_APP_NAME="tech129" \
NEW_RELIC_APPLICATION_LOGGING_ENABLED=true \
NEW_RELIC_APPLICATION_LOGGING_FOWARDING_ENABLED=true \
NEW_RELIC_APPLICATION_LOGGING_FOWARDING_MAX_SAMPLES_STORED=1000 \
NEW_RELIC_APPLICATION_LOGGING_LOCAL_DECORATING_ENABLED=true

# copy the build output from the previous stage
COPY --from=build /src/out .
# expose the port the api runs on
EXPOSE 8080
# set the entry point for the container
ENTRYPOINT ["dotnet", "FCG.API.dll"]