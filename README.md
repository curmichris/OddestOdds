Project Title
Oddest Odds

GiG Developer Test Background
ODDESTODDS.com needs a website that displays odds in real time to their clients.  They have hired you to provide a proof of concept (POC).  This should not be a full product, but rather a demonstration that you can fulfill their eventual requirements.  
The overall requirements for this POC are two deliverables:  a screen displaying 1X2 odds and a backoffice screen to edit the odds.  


## Project Explanation and Guidelines
Project Consist of 2 sections which will be explained below:
* Punter Area
	The punter area is a screen where a punter is able to view 1 X 2 odds in real-time. 
	Punter Area can be accessed by following the link (for localhost): https://localhost:44350/odds/index
	By Default this is the first screen to be visible when the project is run.
	
* Admin Area
	The Admin area handles all CRUD operations for odds and a 'Populate Fields' button to trigger a SignalR event and update all odds visible in all Punter area screens.
	The Admin area can be accessed by following the link (for localhost): https://localhost:44350/admin/index
	On navigation of the previous link, the admin is presented with a list of all available odds which the user can view. 
	*	The admin can add new odds by clicking the 'Create New' button located at the top of the Body area. A new screen will be visible to add the odds required.
	*	The admin can edit an existing Odd by clicking the 'Edit' button visible per Odd created. A new screen will pop up to edit the current odd selected.
	*	The admin can delete an existing odd by clicking the 'Delete' button visible per Odd Created.
	*	The admin can update all visible odds created or updated in real-time by clicking the 'Populate Fields' button located at the bottom of the Admin Area.
			
## Prerequirements

* Visual Studio 2017
* .NET Core SDK
* SQL Server

## How To Build and Install Packages

* Open solution in Visual Studio 2017
* Build Project in order to restore all Nuget packages which are relevant to the project.


ASP.NET Core SignalR is a new library for ASP.NET Core developers that makes it incredibly simple to add real-time web functionality to your applications. 
Install SignalR Client from npm and run following Command

```bash
npm install @aspnet/signalr
```

The `@aspnet/signalr` package (and it's dependencies) require NPM 5.6.0 or higher.



## How To Run

* Open solution in Visual Studio 2017
* Set OddestOdds.Web project as Startup Project and build the project.
* Run the application.

_Upon running the project, a default SQL 'CreateDb' migration will run which will create the database with two tables which will handle the odds._
_Subsequently a seeding method is being called in order to populate 3 odds in the database_


## Tests
OddestOdds project contains a set of Unit Tests and Integration Tests
Integration Tests located at folder 'Integration' -> 'Repository Tests'

There is no need to manually configure the tests as the initial setup of the tests handles the creation of a mocked context used to tests all CRUD operations in the repository.

## Running the Tests
* Open solution in Visual Studio 2017
* Select 'Tests' Project (Located as last project of solution)


Built With

* ASP.NET CORE 2.0 - Framework
* Microsoft Dependency Injection - Dependency Handler
* Entity Framework Core - Database Managment
* Microsoft Signal R - Real time updates
* ASP.NET Razor Pages - Views / Front End
* NUnit, NSubstitue, xUnit, AutoFixture - Testing


Author
Christian Curmi
