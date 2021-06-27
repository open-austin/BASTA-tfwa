# Tenant File Backend

## Before you can run the server, you will need to do the following

- You will need a Google Cloud account to interact with several services that we use (primarily the database).
  - Once you have a Google Cloud account, follow the instructions [here](https://cloud.google.com/docs/authentication/getting-started) to set up local application credentials. This is currently necessary to run the API. Follow the steps up until the section titled ***Verifying authentication***
  - After setting any environment variable, as you are doing in the guide above, you will likely need to close all instances of your code editor for the updates to be propagated
- Install the latest version of [dotnet](https://dotnet.microsoft.com/download) (.NET 5.0)
- You will need a database running locally for the server to connect to. You can run the local server by executing the ./startup.sh script in the local-development folder.
  - Docker: Used for having a more easily setup postgres development environment. Install [here](https://docs.docker.com/get-docker/)
  - If you already have postgres installed, set the `connectionString` in `Startup.cs`
    - It will follow this convention:

  ```C#
  "Server=127.0.0.1; Port=5432; Database=basta; User Id=<postgres>; Password=<password>"
  ```

## Running the server

- Navigate to `/TenantFile/Api`
- Run the command `dotnet run` in your terminal
- Server will be listening on <https://localhost:8080>
