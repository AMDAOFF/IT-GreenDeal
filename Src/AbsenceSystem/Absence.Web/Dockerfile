#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Absence.Web/Absence.Web.csproj", "Absence.Web/"]
RUN dotnet restore "Absence.Web/Absence.Web.csproj"
COPY . .
WORKDIR "/src/Absence.Web"
RUN dotnet build "Absence.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Absence.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Absence.Web.dll"]