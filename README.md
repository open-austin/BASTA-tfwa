# BASTA-tfwa

Web application to help tenants keep track of documents related to their rental unit

### What problem are you trying to solve?

Tenants, especially low-income tenants, and advocates need to be save and organize records related to units and complexes.
Records include photos, receipts, the lease terms, notes from the landlord, fees/fines, and incident reports. We're looking for an accessible platform designed for those that BASTA works with -- easy to text or upload in, making metadata clear, and with flexible security settings.

That would be version 1.

For later versions:
Eventually, we'd also like to be able to auto-generate forms and complaints for repair requests and code complaints.
Include nudges/to-do lists to let tenants and others know when to submit records or take other actions.
Incorporate Appraisal District and Census datasets.
Display data publicly on a map / something visually appealing.

Document types:
Leases
Notices
Photos of conditions
Apartment Newsletters
Bills (Utility, Rent, etc)
Lease Violations
Court summons
Petitions
Letters/notes to MGMT
Receipts
Checks

### Who will benefit (directly and indirectly) from your project?

Tenant organizers and tenants facing gaslighting, neglect, or abuse by landlords.

### What other resources/tools are currently serving the same need? How does your project set itself apart?

Justfix.nyc creates tech to help tenants, but only for those residing in New York City. PDF checklist for Texans is [here](http://www.txtha.org/wp-content/uploads/2016/01/MIMOIForm1.pdf). Other [big PDF handbooks](https://www.texasbar.com/AM/Template.cfm?ContentID=25969&Section=Free_Legal_Information2&Template=/CM/ContentDisplay.cfm) also exist.

We recently came across [this application](https://www.uwazi.io/) designed to help human rights organizations organize documents.

### Where can we find any research/data available/articles?

[Audrey McGlinchy](https://www.kut.org/people/audrey-mcglinchy-kut) writes about housing, zoning, and eviction in Austin. [This](https://www.texasobserver.org/rent-by-another-name/) recent article discusses ways in which tenants are taken advantage of with mandatory fees.

Here's the link to our [Google Drive folder](https://drive.google.com/open?id=1ThK_ThKCyllMOzyot5wIKgrcTXWpzoan).

### What help do you need now?

We're relatively new to legit web development, so we need help with...everything! We want to think through the most reasonable way to get this set up in stages -- first being very simple, then building out from there.

### What are the next steps (validation, research, coding, design)?

### How can we contact you outside of Github(list social media or places you're present)?

@e_shackney on twitter

## Connecting to the Google Cloud Dev Postgres instance

1. Install the proxy according to your OS: https://cloud.google.com/sql/docs/postgres/connect-external-app#install
1. Make sure you have properly set up your `GOOGLE_APPLICATION_CREDENTIALS` environment variable
1. Start the proxy with `./cloud_sql_proxy -instances=tenant-file-fc6de:us-central1:tx-tenant-dev=tcp:5432`
1. You should be able to access the database at 12.0.0.1:5432 now

## Database Migrations

https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

`npx apollo schema:download --endpoint=http://localhost:8080 graphql-schema.json`
`npx apollo client:codegen --localSchemaFile=graphql-schema.json --target=typescript --includes="src/**/*.ts*" --tagName=gql`
