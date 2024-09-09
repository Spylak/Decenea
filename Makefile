add-migration:
	dotnet ef migrations add $(name) -s ./Decenea.WebAPI/Decenea.WebAPI.csproj -p ./Decenea.Infrastructure/Decenea.Infrastructure.csproj
	
database-update:
	dotnet ef database update -s ./Decenea.WebAPI/Decenea.WebAPI.csproj -p ./Decenea.Infrastructure/Decenea.Infrastructure.csproj

remove-migration:
	 dotnet ef migrations remove -s ./Decenea.WebAPI/Decenea.WebAPI.csproj -p ./Decenea.Infrastructure/Decenea.Infrastructure.csproj 

docker-azurite:
	docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 --name healthtech-azurite -d mcr.microsoft.com/azure-storage/azurite

docker-aspire:
	docker run --rm -it -p 18888:18888 -p 4317:18889 -d --name aspire-dashboard -e DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS='true' mcr.microsoft.com/dotnet/aspire-dashboard:8.0.0
