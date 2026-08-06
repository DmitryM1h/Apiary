FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ./ApiaryServer ./ApiaryServer
COPY ./Apiary ./Apiary
RUN dotnet restore "./ApiaryServer/ApiaryServer.csproj"


WORKDIR /src/ApiaryServer/

RUN dotnet publish -c Release -o /publish


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS final
WORKDIR /src
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "ApiaryServer.dll"]