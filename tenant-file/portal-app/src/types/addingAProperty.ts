/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL mutation operation: addingAProperty
// ====================================================

export interface addingAProperty_createProperty_payload {
  __typename: "Property";
  name: string | null;
}

export interface addingAProperty_createProperty {
  __typename: "CreatePropertyPayload";
  payload: addingAProperty_createProperty_payload | null;
}

export interface addingAProperty {
  createProperty: addingAProperty_createProperty;
}

export interface addingAPropertyVariables {
  bldgName: string;
  addrLn1: string;
  addrLn2: string;
  addrLn3: string;
  addrLn4: string;
  city: string;
  state: string;
  zip: string;
}
