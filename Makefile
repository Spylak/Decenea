add-migration:
	dotnet ef migrations add $(name) -s ./Decenea.WebAPI/Decenea.WebAPI.csproj -p ./Decenea.Infrastructure/Decenea.Infrastructure.csproj