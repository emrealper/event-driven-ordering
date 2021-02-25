FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY ["EmailAndNotificationService/EmailAndNotificationService.csproj", "EmailAndNotificationService/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "EmailAndNotificationService/EmailAndNotificationService.csproj"

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY --from=build-env /app/out .

RUN mkdir /root/.dotnet/tools
ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet tool install -g dotnet-counters

ENTRYPOINT ["dotnet", "EmailAndNotificationService.dll"]





