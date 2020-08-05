/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: TenantListQuery
// ====================================================

export interface TenantListQuery_tenants_nodes_images {
  __typename: "Image";
  thumbnailName: string;
}

export interface TenantListQuery_tenants_nodes_tenantPhones_phone {
  __typename: "Phone";
  phoneNumber: string;
}

export interface TenantListQuery_tenants_nodes_tenantPhones {
  __typename: "TenantPhone";
  phone: TenantListQuery_tenants_nodes_tenantPhones_phone;
}

export interface TenantListQuery_tenants_nodes {
  __typename: "Tenant";
  name: string;
  images: TenantListQuery_tenants_nodes_images[];
  tenantPhones: TenantListQuery_tenants_nodes_tenantPhones[];
}

export interface TenantListQuery_tenants {
  __typename: "TenantConnection";
  /**
   * A flattened list of the nodes.
   */
  nodes: (TenantListQuery_tenants_nodes | null)[] | null;
}

export interface TenantListQuery {
  tenants: TenantListQuery_tenants | null;
}
