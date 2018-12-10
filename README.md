# Exebite

## Project goal

Exebite is a research and development project for internal food ordering. Goal of this project is to automate the ordering of food, payment's and reporting for this current process.

Menus/lunches are administers by restaurants delivering food. Restaurants are filling sheets with available orders for each day.

This system/app will load the data from sheets to internal database and make it available via REST point to Client application(s). Client applications will be used as a GUI for food ordering.

## Project setup and starting

### Requirements

- Net Core SDK 2.1.500
- SQL server
- Visual Studio 2017 or some other IDE compatible with .Net Core 2.1 projects

### Setup

1. Clone the application `git clone https://github.com/execom-eu/exebite'
2. Set up DB and update connection string (on first run migrations shoudl be applied)
3. Run `dotnet run --project Exebite.API`
4. Access http://localhost:50586/swagger to see availiable endpoints/generate clinet
5. Happy coding :-)

# Current build status
[![Build status](https://dev.azure.com/exebite/exebite/_apis/build/status/exebite-ASP.NET%20Core-CI)](https://dev.azure.com/exebite/exebite/_build/latest?definitionId=6)
