#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EmailAndNotificationService/EmailAndNotificationService.csproj", "EmailAndNotificationService/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "EmailAndNotificationService/EmailAndNotificationService.csproj"
COPY . .
WORKDIR "/src/EmailAndNotificationService"
RUN dotnet build "EmailAndNotificationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailAndNotificationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir /root/.dotnet/tools
ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet tool install -g dotnet-counters

ENTRYPOINT ["dotnet", "EmailAndNotificationService.dll"]