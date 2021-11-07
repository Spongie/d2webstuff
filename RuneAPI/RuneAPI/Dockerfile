FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RuneAPI/RuneAPI.csproj", "RuneAPI/"]
RUN dotnet restore "RuneAPI/RuneAPI.csproj"
COPY . .
WORKDIR "/src/RuneAPI"
RUN dotnet build "RuneAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RuneAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RuneAPI.dll"]