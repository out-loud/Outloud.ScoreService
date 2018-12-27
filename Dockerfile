# build
FROM microsoft/dotnet:2.2-sdk-alpine AS build
LABEL stage intermediate
WORKDIR /
COPY Outloud.ScoreService/src src
COPY Outloud.ScoreService/tests tests
WORKDIR /src
RUN dotnet build Outloud.ScoreService.csproj -c Release -o /app --source "https://api.nuget.org/v3/index.json" --source "https://www.myget.org/F/outloud/api/v3/index.json"
WORKDIR /tests
RUN dotnet build Outloud.ScoreService.Tests.csproj -c Release -o /tests --source "https://api.nuget.org/v3/index.json" --source "https://www.myget.org/F/outloud/api/v3/index.json"
# publish to /app dir
FROM build AS publish
WORKDIR /src
RUN dotnet publish Outloud.ScoreService.csproj -c Release -o /app --source "https://api.nuget.org/v3/index.json" --source "https://www.myget.org/F/outloud/api/v3/index.json"
# copy tests
FROM build AS test
WORKDIR /tests
ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.3.0/wait /wait
RUN chmod +x /wait
ENTRYPOINT /wait && dotnet test -c Release --no-restore --logger:trx
# publish
FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app
COPY --from=publish /app .
ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.3.0/wait /wait
RUN chmod +x /wait
ENTRYPOINT /wait && dotnet Outloud.ScoreService.dll
EXPOSE 5000