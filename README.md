

# Basic Income Application

This application basicly works with EFCore , Oauth2 and infrastructure which I build my own.If It doesn't suit you or your style , please feel free to open an issue :+1: There is no logical logic on application :stuck_out_tongue:

## Usage and Run

First you should run migrations with PostgreSQL (if you want to change DB just install any DB you want. Postgre is a open-source , free and dockerizable which I ran it on docker.) There are three web applications ;

 - **IdentityServer** ; Manage user's authorization and authentication.
 - **API** ; Only db access in here and there are several domain services right now. Domain services communicate database with repository. There is no direct communication with database.
 - **WebApp** ; All clients see this application and web pages. There is no db connection on it and there is no businesses on it. It'll always communicate with **API** only **API**. :confused:
 ## Dummy Photos 
 **Swagger**
 
![Swagger](https://user-images.githubusercontent.com/20838613/99772286-89be6600-2b1b-11eb-828a-608a352e7d76.png)
**Customer API**

![Customer API](https://user-images.githubusercontent.com/20838613/99772351-a5c20780-2b1b-11eb-977a-0e2a3318837b.png)
**Expense API**

![Expense API](https://user-images.githubusercontent.com/20838613/99772411-c7bb8a00-2b1b-11eb-8fc9-941b74928205.png)
**Institution API**

![Institution API](https://user-images.githubusercontent.com/20838613/99772449-dbff8700-2b1b-11eb-92ce-0cfe0a389c7c.png)
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate. *I've to focus on writing more repository tests and domain services tests.*   :heavy_check_mark:

## Future
  - I believe I should build mobile application for it. First of all , it is a dummy application which there is no make sense or real life businesses :grin: Decided to build flutter mobile application.   :confused:
  - Run all application in Docker and all application must be Dockerize.  :soon:
  - Despite of Creating MVC application inside of VS , I decided to create an Angular application for all users(include admin). :soon:
  - Logging with SeriLog and Show these log with Dockerize SEQ.   :soon:
  - Creating a Gateway to rule all API with one way.   :soon:
  - Write dockerfile and docker-compose to RULE EVERYTHING.   :beer:
