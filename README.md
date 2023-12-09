# BankTradingService

## Setup

Kindly clone the application and run the docker.compose file through Visual Studio 2022. 

During the development of this application I had made some assumptions:

- All user creation services are being handled by a different microservice. Therefore, no creation of user service have been introduced, and the users in the database have been fabricated by myself during development.
- The trading that will be done will be based on a Stock Market or Foreign Exchange market. Therefore, there is an expectation that the user has to open a trade (Buy or Sell) and then subsequently close it.
