# Create new IdentityServer project
	dotnet new -i Duende.IdentityServer.Templates
	dotnet new isempty -n IdentityServer
	dotnet new isui

# Run docker
	docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
	docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build