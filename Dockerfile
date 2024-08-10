FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Decenea.WebAPI/Decenea.WebAPI.csproj", "Decenea.WebAPI/"]
RUN dotnet restore "Decenea.WebAPI/Decenea.WebAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Decenea.WebAPI/Decenea.WebAPI.csproj" -c Release -o /app/build
# RUN dotnet test "Decenea.WebAPI.Tests/Decenea.WebAPI.Tests.csproj"
RUN dotnet publish "Decenea.WebAPI/Decenea.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 5080
WORKDIR /app
COPY --from=build /app/publish .

# Change user to non-root (gecos means don't interactively prompt for various info about the user)
RUN adduser --disabled-password --gecos '' appuser
USER appuser

ENTRYPOINT ["dotnet", "Decenea.WebAPI.dll"]
