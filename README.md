# BASTA-tfwa

Web application to help tenants keep track of documents related to their rental unit

### What problem are you trying to solve?

Tenants, especially low-income tenants, and advocates need to be save and organize records related to units and complexes.
Records include photos, receipts, the lease terms, notes from the landlord, fees/fines, and incident reports. We're looking for an accessible platform designed for those that BASTA works with -- easy to text or upload in, making metadata clear, and with flexible security settings.

We have managed to get to a point where tenants can send a photo to a phone number and it ends up on Google Drive. We are designing around tenants sending photos of apartment conditions, since the stakes for security are lower.

Next, we're working on implementing a Twilio conversation. Tenants can send in a form, documentation, and add their contact information. 

After that, we're building out the retrieval platform. Rather than Google Drive, we're building a custom web platform where organizers and tenants can retrieve the photos they have sent.

For later versions:
* Eventually, we'd also like to be able to auto-generate forms and complaints for repair requests and code complaints.
* Include nudges/to-do lists to let tenants and others know when to submit records or take other actions.
* Incorporate Appraisal District and Census datasets.
* Display data publicly on a map / something visually appealing.

Document types:
* Leases
* Notices
* **Photos of conditions**
* Apartment Newsletters
* Bills (Utility, Rent, etc)
* Lease Violations
* Court summons
* Petitions
* Letters/notes to MGMT
* Receipts
* Checks

### Who will benefit (directly and indirectly) from your project?

Tenant organizers and tenants facing gaslighting, neglect, or abuse by landlords.

### What other resources/tools are currently serving the same need? How does your project set itself apart?

Justfix.nyc creates tech to help tenants, but only for those residing in New York City. PDF checklist for Texans is [here](http://www.txtha.org/wp-content/uploads/2016/01/MIMOIForm1.pdf). Other [big PDF handbooks](https://www.texasbar.com/AM/Template.cfm?ContentID=25969&Section=Free_Legal_Information2&Template=/CM/ContentDisplay.cfm) also exist.

We recently came across [this application](https://www.uwazi.io/) designed to help human rights organizations organize documents.

### Where can we find any research/data available/articles?

[Audrey McGlinchy](https://www.kut.org/people/audrey-mcglinchy-kut) writes about housing, zoning, and eviction in Austin. [This](https://www.texasobserver.org/rent-by-another-name/) recent article discusses ways in which tenants are taken advantage of with mandatory fees.

Here's the link to our [Google Drive folder](https://drive.google.com/open?id=1ThK_ThKCyllMOzyot5wIKgrcTXWpzoan).

### What help do you need now?

Front-end design

User testing 

Project management

Developers familiar with our tech stack:

Frontend Client:
  * React.JS
  * Redux
  * GraphQL
  * Apollo Client - state management with GraphQL
  * Typescript
  * Axios
  * Firebase (authentication)
  * Formik
  * FontAwesome
  * Styled Components

Backend:
  * API - C#/ASP.NET core
  * Database - Postgres hosted on Google Cloud Platform
  * Authentication - Firebase

### What are the next steps (validation, research, coding, design)?

TBD.

## Required and Recommended local tools

Most team members use the following tools. Required tools are noted.

- dotnet: Required to compile and run the API. Download [here](https://dotnet.microsoft.com/download)
- nvm: A highly recommended version manager for node. Download [here](https://github.com/nvm-sh/nvm)
  - If you don't use nvm, then acquiring node directly is required. Download [here](https://nodejs.org/en/download/)

Once `nvm` is installed, you can `cd` into `tenant-file/portal-app` and run `nvm use`. This will install the appropriate version of node (if not already installed), and set your local environment to the specified version. Currently we're using Node v14 LTS.

Development on the backend requires:
- Docker: Used for having a more easily setup postgres development environment. Install from [here](https://docs.docker.com/get-docker/)
- gcloud: A recommended tool to interact with the google cloud platform. Instruction to install are [here](https://cloud.google.com/sdk/docs#install_the_latest_cloud_tools_version_cloudsdk_current_version)

## Running the Server Locally

You will need a database running locally for the server to connect to. You can run the local server by executing the `./startup.sh` script in the `local-development` folder.

## Google Cloud Account

You will need a Google Cloud account to interact with several services that we use (primarily the database). 

Once you have a Google Cloud account, follow the instructions [here](https://cloud.google.com/docs/authentication/getting-started) to set up local application credentials. This is currently necessary to run the API.

## Database Migrations

https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

dotnet ef migrations add \<MigrationName\>
dotnet ef database update

`npx apollo schema:download --endpoint=http://localhost:8080 graphql-schema.json`
`npx apollo client:codegen --localSchemaFile=graphql-schema.json --target=typescript --includes="src/**/*.ts*" --tagName=gql`
