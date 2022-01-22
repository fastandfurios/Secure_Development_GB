#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Debit_Cards_Project/Debit_Cards_Project.csproj", "Debit_Cards_Project/"]
RUN dotnet restore "Debit_Cards_Project/Debit_Cards_Project.csproj"
COPY . .
WORKDIR "/src/Debit_Cards_Project"
RUN dotnet build "Debit_Cards_Project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Debit_Cards_Project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Debit_Cards_Project.dll"]