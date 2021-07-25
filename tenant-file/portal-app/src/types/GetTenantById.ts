/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

import { PreferredLanguage } from "./globalTypes";

// ====================================================
// GraphQL query operation: GetTenantById
// ====================================================

export interface GetTenantById_tenant_residence_property {
  __typename: "Property";
  id: string;
  name: string | null;
}

export interface GetTenantById_tenant_residence_address {
  __typename: "Address";
  line1: string;
  line2: string | null;
  city: string;
  state: string;
  postalCode: string;
}

export interface GetTenantById_tenant_residence {
  __typename: "Residence";
  property: GetTenantById_tenant_residence_property | null;
  address: GetTenantById_tenant_residence_address | null;
}

export interface GetTenantById_tenant_phones_nodes_images_labels {
  __typename: "ImageLabel";
  label: string;
  confidence: number | null;
  source: string;
}

export interface GetTenantById_tenant_phones_nodes_images {
  __typename: "Image";
  thumbnailName: string;
  name: string;
  labels: GetTenantById_tenant_phones_nodes_images_labels[] | null;
}

export interface GetTenantById_tenant_phones_nodes {
  __typename: "Phone";
  phoneNumber: string;
  preferredLanguage: PreferredLanguage | null;
  images: (GetTenantById_tenant_phones_nodes_images | null)[] | null;
}

export interface GetTenantById_tenant_phones {
  __typename: "PhoneConnection";
  /**
   * A flattened list of the nodes.
   */
  nodes: GetTenantById_tenant_phones_nodes[] | null;
}

export interface GetTenantById_tenant {
  __typename: "Tenant";
  name: string;
  id: string;
  residenceId: string | null;
  residence: GetTenantById_tenant_residence | null;
  phones: GetTenantById_tenant_phones | null;
}

export interface GetTenantById {
  tenant: GetTenantById_tenant;
}

export interface GetTenantByIdVariables {
  tenantId: string;
}
