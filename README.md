# ejmossmann-prosigliere-coding-challenge

### This repository is dedicated to the Prosigliere Backend coding challenge.


## Architecture Structure

The coding challenge was developed in .NET Core 8 and include follows principles of the Domain Driven Design (DDD) architecture.

The solution include the following projects:
* #### BloggingPlatform.Api
    * The executable project of the solution. Defines configuration, authentication, Swagger documentation and the dependency injection of other services.
* #### BloggingPlatform.Application
    * The project that contains the application services and the business rules defined by each use case. Acts as a middle point between the API and the database data.
    * Also defines AutoMapper mapping profiles, validators and other middlewares related with the solution
* #### BloggingPlatform.Domain
    * Defines the domain, the core entities and interfaces used along side the entire solution. All other projects are driven by what the domain defines.
* #### BloggingPlatform.Infrastructure.Data
    * Defines the DbContext mappings and for EF Core, along side with the repositories code to access database information.
* #### BloggingPlatform.Tests
    * Defines tests (in this case, only unit tests) for the core pieces and services of the application

The main was to develop a well defined and generic infrastructure, making the project ready for feature additions and maintenance. The project counts with a lot of polymorphism usage and storage of audit information (Created, updated and deleted information) for future usage.

The application uses Serilog, that could in advance support a more robust logging service for any external logging application.

# How to run it
Inside "./src" folder there is a file named "run.bat" that contains the step by step guide on how to run the application.

The application have the following dependencies:
* .NET Core version 8 or superior
* .NET Entity Framework Core version 8 
    * Can be installed with: dotnet tool install --global dotnet-ef --version 8.0.0

This will setup a local MSSQL database with the corresponding schema for the application.

NOTE: Ideally Docker would be used to setup the project in different machines without any issues.

### Usability

A simple authentication/authorization was set in place. No Identity databases and/or services were developed during the process, only the API authorization as a POC.
The only working credentials to be used are described in the "appsettings.json" file. The endpoint **POST api/login** should generate a JWT Bearer Token.
Here is the expected body payload for token generation
```json
{
  "email": "user@user.com",
  "password": "pass"
}
```

The generated token should be used as a Bearer token in the Authorization header or, if using Swagger, can be added in the top-right Authorize button configuration.

## CI/CD processes
Two different YAML files were added under .github/workflows: **build-validation.yml** and **deploy.yml**. 

Those are placeholder workflow files to be used during CI/CD processes. More information and comments were added under each of the workflow files.

# Next Steps
* Add more functional resources to the application
    * Add support to reactions to BlogPosts and Comments. Every BlogPost and Comments would have a list of reactions related with it, with the reaction type being: Like, Loved, Funny and others.
    * Add full usability flow for the Blogging Platform, adding editing and remove functionalities. Every BlogPost and Comments could have been edited or deleted by it's owner.
This gives room to improve the authentication/authorization flow, adding support to other roles, as Admin, which would have permissions to manage any BlogPost/Comment.

* Add more testability and observability
    * Add test coverage to the application. Currently only a few unit tests were set in place. A good next step would be to add more unit tests for other pieces of the code: Login Flow, Middlewares, DbContext and other EF Cores pieces. Integration tests, with minimal mocked pieces, would be helpful to ensure the functionalities of the API.
    * Add monitoring through a third party application that would expose logs and help to configure monitors for unexpected behaviors and/or service downtimes.

* Add a proper robust documentation. The project counts with Swagger documentation, containing a guideline to execute corresponding APIs. A robust documentation would help to understand the whole architecture and business rules related with the application.
    * A proper documentation, including architecture drawings, would serve as guideline for the application future features to be added

* Add a Docker setup for the solution to be agnostic from machines while developing and deploying the application