/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: TenantListQuery
// ====================================================

export interface TenantListQuery_tenants_nodes_phones_nodes_images_edges_node_labels {
  __typename: "ImageLabel";
  label: string;
  confidence: number | null;
  source: string;
}

export interface TenantListQuery_tenants_nodes_phones_nodes_images_edges_node {
  __typename: "Image";
  thumbnailName: string;
  name: string;
  labels: TenantListQuery_tenants_nodes_phones_nodes_images_edges_node_labels[] | null;
}

export interface TenantListQuery_tenants_nodes_phones_nodes_images_edges {
  __typename: "ImageEdge";
  /**
   * The item at the end of the edge.
   */
  node: TenantListQuery_tenants_nodes_phones_nodes_images_edges_node;
}

export interface TenantListQuery_tenants_nodes_phones_nodes_images {
  __typename: "ImageConnection";
  /**
   * A list of edges.
   */
  edges: TenantListQuery_tenants_nodes_phones_nodes_images_edges[] | null;
}

export interface TenantListQuery_tenants_nodes_phones_nodes {
  __typename: "Phone";
  phoneNumber: string;
  images: TenantListQuery_tenants_nodes_phones_nodes_images | null;
}

export interface TenantListQuery_tenants_nodes_phones {
  __typename: "PhoneConnection";
  /**
   * A flattened list of the nodes.
   */
  nodes: TenantListQuery_tenants_nodes_phones_nodes[] | null;
}

export interface TenantListQuery_tenants_nodes {
  __typename: "Tenant";
  name: string;
  id: string;
  phones: TenantListQuery_tenants_nodes_phones | null;
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
