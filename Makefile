add-migration:
	dotnet ef migrations add $(name) -s ./Decenea.WebAPI/Decenea.WebAPI.csproj -p ./Decenea.Infrastructure/Decenea.Infrastructure.csproj
	
database-update:
	dotnet ef database update -s ./Decenea.WebAPI/Decenea.WebAPI.csproj -p ./Decenea.Infrastructure/Decenea.Infrastructure.csproj
