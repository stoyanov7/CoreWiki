FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
WORKDIR /src
COPY ["CoreWiki.Web/CoreWiki.Web.csproj", "CoreWiki.Web/"]
COPY ["CoreWiki.Application/CoreWiki.Application.csproj", "CoreWiki.Application/"]
COPY ["CoreWiki.Domain/CoreWiki.Domain.csproj", "CoreWiki.Domain/"]
COPY ["CoreWiki.Models/CoreWiki.Models.csproj", "CoreWiki.Models/"]
COPY ["CoreWiki.Infrastructure/CoreWiki.Infrastructure.csproj", "CoreWiki.Infrastructure/"]
COPY ["CoreWiki.Utilities/CoreWiki.Utilities.csproj", "CoreWiki.Utilities/"]
COPY ["CoreWiki.Data/CoreWiki.Data.csproj", "CoreWiki.Data/"]
RUN dotnet restore "CoreWiki.Web/CoreWiki.Web.csproj"
COPY . .
WORKDIR "/src/CoreWiki.Web"
RUN dotnet build "CoreWiki.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoreWiki.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreWiki.Web.dll"]