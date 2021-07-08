/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL mutation operation: addingATenant
// ====================================================

export interface addingATenant_createTenant_payload {
  __typename: "Tenant";
  id: string;
  name: string;
}

export interface addingATenant_createTenant {
  __typename: "CreateTenantPayload";
  payload: addingATenant_createTenant_payload | null;
}

export interface addingATenant {
  createTenant: addingATenant_createTenant;
}

export interface addingATenantVariables {
  fullName: string;
  phoneNumber: string;
  street: string;
  unitNumber: string;
  city: string;
  zip: string;
  bldgId?: string | null;
}
