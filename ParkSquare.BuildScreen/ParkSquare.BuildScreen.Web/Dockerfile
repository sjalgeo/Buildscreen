FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.3 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY ParkSquare.BuildScreen/. /app
ENTRYPOINT ["dotnet", "ParkSquare.BuildScreen.dll"]
