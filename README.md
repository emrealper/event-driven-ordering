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
You can run the bellow command from the **/source/Producer/Order.Api/** directory to build docker images for  `Producer/Order API` 
```powershell
docker build -f "Dockerfile" -t orderproducer_image ..
```

and the below command from the **/source/Consumer/EmailAndNotificationService/**  for `Consumer/Email and Notification Service` 

```powershell
docker build -f "Dockerfile" -t orderconsumer_image ..
```

after building docker images of two application you can run the below command from the **/setup/** directory to run both 2 services and service bus enviroment. (Kafka and Zookeeper)

```powershell
docker-compose up
```
## Load testing and performance monitoring

To test the application i use `bombardier` which is written in Go programming language to simulate many HTTP(S) request concurently sent from different clients.

You can run the below command from the **/diagnostics/bombardier/** directory to build docker images for  `bombardier` 

```powershell
docker build -t alpine/bombardier .
```

To monitor our applications memory consumption and cpu usage i use `dot-net-counters` which was introduced with [.NET CORE 3.0](https://devblogs.microsoft.com/dotnet/introducing-diagnostics-improvements-in-net-core-3-0/)

You can run the below commands using command prompt to monitor diagnostics of `Producer/Order API` and `Consumer/Email and Notification Service` 
>**PLEASE** change your command prompt font-size to 12 for better readibilty.

```cpp
docker exec -it orderproducer dotnet counters monitor -p 1 System.Runtime Microsoft.AspNetCore.Hosting
```

```cpp
docker exec -it orderconsumer dotnet counters monitor -p 1 System.Runtime
```
In the image below, you can see the CPU utilization and memory consuptions of our applications on waiting mode. (No request and load)

![initial-diagnostics](https://github.com/emrealper/event-driven-sushi/blob/main/media/Diagnostics-1.png)


## Let's load testing using `bombardier` 
![machine-gun](https://i.imgur.com/2u6JJnh.gif)

You can run the below command using powershell or command prompt to make concurent http call to  `Producer/Order API`. It simulates 50 http call per second from 50 different client during 100 seconds.

Please see the content of HTTP `POST` request.

``` JSON
{
	"customerId":322332, 
	"customerName": "Emre",
	"customerLastName":"Alper",
	"customerEmail":"emrealper@gmail.com",
	"deliveryAddress":"Oosterdoksstraat 80, 1011 DK Amsterdam, Netherlands",
	"restaurantId":66789, 
	"restaurantName": "Quick China",
	"orderNote": "please don't ring the doorbell baby is sleeping",
	"paymentMethodType":2,
	"orderProducts":[

		{
			"productId":784567,
			"productName":"Philadelphia Roll Menu (16 Pieces)",
			"quantity":1,
			"unitCost":17.5,
			"currencyType":1
		},
		{
			"productId":784589,
			"productName":"California Roll Half Menu (8 Pieces)",
			"quantity":1,
			"unitCost":8.25,
			"currencyType":1
		}
	]
}
```

### Running and result

```powershell
docker run -ti --rm alpine/bombardier -c 50 -d 100s --rate 50 -m POST "http://host.docker.internal:5000/api/Order" -H "Content-Type: application/json" -f "orderEventData.json"
Bombarding http://host.docker.internal:5000/api/Order for 1m40S using 50 connection(s)
[=======================================================================================================================================================================================================================================] 1m40sDone!
Done!
Statistics        Avg      Stdev        Max
  Reqs/sec       48.32     453.54     26595.33
  Latency      1.13s     147.09ms      1.81s
  Latency Distribution
     50%   613.75ms
     75%   639.65ms
     90%   707.76ms
     99%      1.20s
  HTTP codes:
    1xx - 0, 2xx - 3993, 3xx - 0, 4xx - 0, 5xx - 0
    others - 0
  Throughput:   36.01KB/s
```

In the image below, you can monitor real-time diagnostics to detect `memory leakage` or `cpu usage` of the `producer` and `consumer` applications. 


![real-time-diagnostics](https://github.com/emrealper/event-driven-sushi/blob/main/media/Diagnostics-Real-time.png)

## Read further and references

- [Using Akka between rest and Kafka](https://deezer.io/akka-as-a-bridge-between-rest-and-kafka-acfe6194c202)
- [Performance Testing Techniques](https://www.youtube.com/watch?v=jn54CjePzs0&list=PLbs2hWUWXfExaGUHCyQoOUXZVjds1zHWR&index=13&t=618s)

