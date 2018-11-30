FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50586

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Exebite.API/Exebite.API.csproj", "Exebite.API/"]
COPY ["Exebite.Business/Exebite.Business.csproj", "Exebite.Business/"]
COPY ["Exebite.Sheets.API/Exebite.Sheets.API.csproj", "Exebite.Sheets.API/"]
COPY ["Exebite.Sheets/Exebite.Sheets.PodLipom/Exebite.Sheets.PodLipom.csproj", "Exebite.Sheets/Exebite.Sheets.PodLipom/"]
COPY ["Exebite.Sheets/Exebite.Sheets.Common/Exebite.Sheets.Common.csproj", "Exebite.Sheets/Exebite.Sheets.Common/"]
COPY ["Exebite.Sheets/Exebite.Sheets.Reader/Exebite.Sheets.Reader.csproj", "Exebite.Sheets/Exebite.Sheets.Reader/"]
COPY ["Exebite.Sheets/Exebite.Sheets.Index/Exebite.Sheets.Index.csproj", "Exebite.Sheets/Exebite.Sheets.Index/"]
COPY ["Exebite.Sheets/Exebite.Sheets.Hedone/Exebite.Sheets.Hedone.csproj", "Exebite.Sheets/Exebite.Sheets.Hedone/"]
COPY ["Exebite.DomainModel/Exebite.DomainModel.csproj", "Exebite.DomainModel/"]
COPY ["Exebite.Sheets/Exebite.Sheets.Teglas/Exebite.Sheets.Teglas.csproj", "Exebite.Sheets/Exebite.Sheets.Teglas/"]
COPY ["Option/Option.csproj", "Option/"]
COPY ["Exebite.GoogleSheetAPI/Exebite.GoogleSheetAPI.csproj", "Exebite.GoogleSheetAPI/"]
COPY ["Exebite.DataAccess/Exebite.DataAccess.csproj", "Exebite.DataAccess/"]
COPY ["Either/Either.csproj", "Either/"]
COPY ["Exebite.Common/Exebite.Common.csproj", "Exebite.Common/"]
COPY ["Exebite.DtoModels/Exebite.DtoModels.csproj", "Exebite.DtoModels/"]
RUN dotnet restore "Exebite.API/Exebite.API.csproj"
COPY . .
WORKDIR "/src/Exebite.API"
RUN dotnet build "Exebite.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Exebite.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Exebite.API.dll"]