FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY ["Order.Api/Order.Api.csproj", "Order.Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Order.Api/Order.Api.csproj"

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY --from=build-env /app/out .

RUN mkdir /root/.dotnet/tools
ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet tool install -g dotnet-counters

ENTRYPOINT ["dotnet", "Order.Api.dll","--urls=http://*80"]