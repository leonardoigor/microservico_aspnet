# FROM mcr.microsoft.com/dotnet/core/sdk
FROM mcr.microsoft.com/dotnet/sdk:6.0
# FROM microsoft/dotnet:2

RUN apt-get update && apt-get install -y curl


COPY ./app /www/app/
WORKDIR /www/app

RUN dotnet tool install -g dotnet-ef


RUN  cp -r  /usr/share/dotnet /usr/local/share/dotnet
ENV ASPNETCORE_URLS=http://+:5000
