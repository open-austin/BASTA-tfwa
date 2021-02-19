#!/bin/sh

gcloud builds submit --tag gcr.io/tenant-file-fc6de/tenant-file-api

gcloud run deploy tenant-file-api --image gcr.io/tenant-file-fc6de/tenant-file-api --platform managed --region=us-central1