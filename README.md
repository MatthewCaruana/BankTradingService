# BankTradingService

## Setup

Kindly clone the application and run the docker.compose file through Visual Studio 2022. 

During the development of this application I had made some assumptions:

- All user creation services are being handled by a different microservice. Therefore, no creation of user service have been introduced, and the users in the database have been fabricated by myself during development. However, checks are still made to ensure that the user actually exists in the database.
- The trading that will be done will be based on a Stock Market or Foreign Exchange market. Therefore, there is an expectation that the user has to open a trade (Buy or Sell) and then subsequently close it.
- Even though Mediatr is set up, I did not follow the CQRS methodology entirely mainly for simplicity reasons. In the ideal case, the reading and writing Database contexts are split to ensure that the reads are not affected by the writes and vice versa.
- The Unit of Work is intended to possess the ability of setting a transaction based environment for when interfacing with the database. In this case, it is not required as the case is very specific.
- A Domain Driven Design was initially planned for this solution, however, given the simplicity of this scenario, I did not fully apply the design approach.