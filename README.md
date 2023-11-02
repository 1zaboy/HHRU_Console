# HH.RU Console

This repository contains the source code for a web application that acts as a wrapper for the HH.ru API. The project is built using ASP.NET Web API for the backend, Angular for the frontend, and utilizes Docker Compose for containerization. Logging in the project is implemented using the ELK (Elasticsearch, Logstash, Kibana) stack.

## Functionality
The website provides the following features:

1) Displaying Responses as a Table:\
Users can view responses to job postings in a convenient table format, making it easy to browse and analyze the data.

2) Automatic Resume Boosting:\
The site automatically boosts a user's resume ranking based on specific criteria or user actions, enhancing its visibility to employers.

3) Table of Possible Job Openings and Ability to Respond (planned for future):\
Users will have the ability to see available job openings in a convenient table and directly apply to them through the website.

## Installation and Run with Docker Compose

> [!WARNING]  
> At the moment, docker-compose to start only the server side.

To install and run the project using Docker Compose, follow these steps:

1) Clone the Repository:
    ```sh
    git clone <repository_url>
    ``` 
2) Navigate to the Project Directory:
    ```sh
	cd <directory_name>
    ``` 
3) Create docker network:
    ```sh
	docker network create hhru_console
    ```
4) Setup API keys:
    ```sh
	cd HHRU_Console.Api
    ``` 
    ```sh
    vim appsettings.Development.json
    ``` 
5) Build and Run the Docker Containers:
    ```sh
	docker-compose up --b
    ``` 
6) Access the Application: \
Open a web browser and go to http://localhost:4200 to access the web application.

## Technologies
The project is developed using the following technologies:

1) ASP.NET Web API:\
The backend of the application is developed using ASP.NET Web API, ensuring efficient handling of requests and interaction with the HH.ru API.

2) Angular:\
The user interface is created using Angular, providing a dynamic and responsive user experience. UI components are implemented using the TaigaUI library, enhancing the visual appeal and functionality of the application.

3) AG Grid:\
AG Grid is used to create interactive and feature-rich data tables, enabling users to efficiently manage and view responses to job postings.

4) TaigaUI:\
TaigaUI is utilized for UI components, enhancing the overall user experience with its rich set of interface elements.

5) Docker Compose:\
Docker Compose is employed to containerize the application and its dependencies, allowing for easy deployment and scalability.

6) ELK Stack (Elasticsearch, Logstash, Kibana):\
Logging in the project is implemented using the ELK stack, providing powerful tools for collecting, storing, and visualizing log data.

## Logging with ELK Stack
The ELK stack components (Elasticsearch, Logstash, and Kibana) are configured via Docker Compose to handle logging. Log data generated by the application is collected, processed, stored, and visualized using the ELK stack tools.

## License
This project is licensed under the MIT License - see the LICENSE file for details.
