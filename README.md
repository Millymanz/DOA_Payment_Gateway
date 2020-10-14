# Overview
This is a light example project of a Payment Gateway API. 
An API based application that allows a merchant to offer a way for their shoppers to pay for their product.
This project is implemented in ASP.NET Core project with .NET Core 3.1

The solution is named DOA Payment Gateway API (based on my initials).
It contains the following projects :
* PaymentGateway :- ASP.NET Core code, Controllers, Startup and more.
* Payments :- All things relating to payment processing and management.
* Payments.DTO :- Classes for predominately transferring data and holding data, models.
* UnitTests :- This covers tests for the payment classes and the utility clasess.


## Use 

Be sure to disable certification when making the API calls.

# Assumptions

An assumption made is that this application and project is a demonstration piece and not built for production straight out of the box. Another assumption made is that the consuming client application or ecommerce application will be performing the majority of the sanity checks on the card details. In the event that this is not done, a soft form of card check or validation has been implemented.


# Features

## API specification

I added Swagger for easy API documentation and specification, this can be accessed through /Swagger url.
Alternatively these is a link in the startup home page that loads as greeting page.


## Postman 
You can use postman to test the API calls and to configure the headers to match the valid MerchantValidationID and APIKey.
Please see the Postman folder.

## Authentication

A simple form of authentication has been implemented which requires a MerchantValidationID and APIKey to be specified in the header.

**Improvements** :- 
In the future this form of authentication should be replaced with well established authentication frameworks, the kind that uses bearer tokens and has limited sessions.


## Validation checks
Soft form of validation check has been implemented to demonstrate the intent to limit and catch out any extreme or unnecessary calls to the gateway which is then forwarded to the banking service.
Extreme entries or request objects with values that clearly do not meet banking card standards should be caught out before sending to the banking service.

Although the banking service will have its own hard validation checks, this extra layer of checks on the gateway side improves overall platform performance and help to prevent malicious and fraudalent use of the gateway and banking service. 
Soft checks include acceptable currency types, appropiate card number and card date values.
Measures have also been taken to prevent valid merchants from accessing data belonging to another merchant.


## Data storage 

The application uses the Entity Framework Core ORM as a means of persisting all data related items onto disk via SQL Server database.
It has been implemented using the repository pattern, and features data relating the currency codes, merchant data and payment logs.

**Improvements** :- 
For future improvements, this section of the code can be replaced with SharpRepositry 
https://github.com/SharpRepository/SharpRepository

This library is generic repository that lets you easily implement a wide myriad of persistance solutions, giving the user the options of storing to databases such as MongoDB, RavenDB, Postgres, MySQL and more.

The dummy seed data includes 5 merchants and 1 payment example, as well as 5 acceptable currency codes.


## Application logging and Application metrics

Metrics and logs are automatically captured and recorded because of an integration with Application Insights.

Improvements
The logging can be replaced with log4net which is popular logging library, which gives easy options for where to persist and display logs, such as databases, consoles and file logs.
https://logging.apache.org/log4net/

## Unit tests
The unit testing is implemented using xUnit and Fluent Assertions.
Fluent assertions is a user friendly assertions library, it makes assertions look beautiful, natural and, most importantly, extremely readable.

https://xunit.net/
https://fluentassertions.com/



# Improvement ideas

## Queries
Extending the api to include calls that will allow for a much broader set of past payments, even filtering by date periods.

## Refunds and Cancellations 
Extending api to include refund requests and cancellations of payment orders.

## Blacklisting
Measures can be taken to blacklist fraudalent actors, which can include the merchant as well as the shopper with the credit card.




