
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
RUN ls  
RUN pwd

WORKDIR /src


COPY . .


RUN dotnet restore Exebite.API
RUN dotnet build Exebite.API -c Release -o /app

FROM build AS publish
RUN dotnet publish Exebite.API -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Exebite.API.dll"]