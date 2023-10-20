#!/bin/bash

if [ -z "$1" ]; then
    echo "Please provide an environment name. Usage: ./run_projects.sh <EnvironmentName>"
    exit 1
fi

ENVIRONMENT=$1

declare -a projects=(
    "Decenea.WebAPI/Decenea.WebAPI.csproj"
    "Decenea.WebApp/Decenea.WebApp.csproj"
    "Decenea.WebAppAdmin/Decenea.WebAppAdmin.csproj"
)

checkWebAPI() {
    if [ ! -f "Decenea.WebAPI/appsettings.$ENVIRONMENT.json" ]; then
        echo "No configuration file found for the provided environment in folder Decenea.WebAPI"
        echo "Terminating script."
        exit 1 
    fi
}

checkWebApp() {
    if [ ! -f "Decenea.WebApp/wwwroot/appsettings.$ENVIRONMENT.json" ]; then
        echo "No configuration file found for the provided environment in folder Decenea.WebApp/wwwroot"
        echo "Terminating script."
        exit 1 
    fi
}

checkWebAppAdmin() {
    if [ ! -f "Decenea.WebAppAdmin/wwwroot/appsettings.$ENVIRONMENT.json" ]; then
        echo "No configuration file found for the provided environment in folder Decenea.WebAppAdmin/wwwroot"
        echo "Terminating script."
        exit 1 
    fi
}

# Function to run a project in a new terminal
run_project() {
    local project_path="$1"
    local project_name=$(basename $(dirname "$project_path"))        
    gnome-terminal --title="$project_name" -- bash -c "ASPNETCORE_ENVIRONMENT=$ENVIRONMENT dotnet run --project $project_path; exec bash"
}

checkWebAPI
checkWebApp
checkWebAppAdmin

# Loop through the projects and start them
for project in "${projects[@]}"; do
    # Check if the project directory exists
    if [ ! -f "$project" ]; then
        echo "Project file not found: $project"
        continue
    fi
    run_project "$project"
done

