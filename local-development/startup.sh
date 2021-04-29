#!/bin/sh

docker run -p 5432:5432 -e POSTGRES_PASSWORD=example -v $(pwd)/postgres-data:/var/lib/postgresql/dat postgres

# Connect to local
# psql -h 127.0.0.1 -p 5432 -U postgres
