# <img src="https://icons-for-free.com/iconfiles/png/512/sushi-1320568027512378083.png" width="30" height="30"> event-driven-sushi<img src="https://cdn0.iconfinder.com/data/icons/linkedin-ui-colored/48/JD-13-512.png" height="30" width="30"><img src="https://icon-library.com/images/delivery-icon-png/delivery-icon-png-29.jpg" width="30"> - Food Delivery Microservices Application
 The primary goal of this sample is to explain following software-architecture concepts and container-technologies like:  
* Microservices  
* CQRS  
* Event Sourcing (Using Kafka)
* Domain Driven Design (DDD)  
* Eventual Consistency  
* Docker
* Docker-Compose

and methods and tools to make load testing and monitoring memory and cpu consumption like  :
* Bombardier
* dotnet-counters

## Architecture:

![architecture](https://github.com/emrealper/event-driven-sushi/blob/main/media/Architecture.png)

## Description:
This repo contains a sample application simulates a food delivery journey between ordering and notification operations after successfull transaction. The system consists of the following parts.

* **Producer/Order API** - An API which accepts post request to make transactional operation of an order (Assuming a new order and/or payment operation has been successfully executed) Then it sends an OrderPaid event message to the event bus. 
* **Consumer/Email and Notification Service** - An Hosted Service (.Net Core Worker Service) which subscribes the Kafka Topic (orderPaid) and sends notification/email about the operation.

## How to set up and run the project
You can run the bellow command from the **/source/Producer/** directory to build docker images for  `Producer/Order API` 
```powershell
docker build -f "Dockerfile" -t orderproducer_image ..
```

and the below command from the **/source/Consumer/**  for `Consumer/Email and Notification Service` 

```powershell
docker build -f "Dockerfile" -t orderconsumer_image ..
```

after building docker images of two application you can run the below command from the **/setup/** directory to run both 2 services and service bus enviroment. (Kafka and Zookeeper)

```powershell
docker-compose up
```
## Load testing and performance monitoring


