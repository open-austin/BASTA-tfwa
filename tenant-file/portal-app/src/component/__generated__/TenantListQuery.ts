/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: TenantListQuery
// ====================================================

export interface TenantListQuery_tenants_nodes_phones_images {
  __typename: "Image";
  thumbnailName: string;
  name: string;
}

export interface TenantListQuery_tenants_nodes_phones {
  __typename: "Phone";
  phoneNumber: string;
  id: string;
  images: (TenantListQuery_tenants_nodes_phones_images | null)[] | null;
}

export interface TenantListQuery_tenants_nodes {
  __typename: "Tenant";
  name: string;
  phones: TenantListQuery_tenants_nodes_phones[];
}

export interface TenantListQuery_tenants {
  __typename: "TenantConnection";
  /**
   * A flattened list of the nodes.
   */
  nodes: TenantListQuery_tenants_nodes[] | null;
}

export interface TenantListQuery {
  tenants: TenantListQuery_tenants | null;
}

export interface TenantListQueryVariables {
  name?: string | null;
}
