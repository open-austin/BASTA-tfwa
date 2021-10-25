/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: PhonesFilteredByName
// ====================================================

export interface PhonesFilteredByName_phones_pageInfo {
  __typename: "PageInfo";
  /**
   * When paginating forwards, the cursor to continue.
   */
  endCursor: string | null;
  /**
   * Indicates whether more edges exist following the set defined by the clients arguments.
   */
  hasNextPage: boolean;
  /**
   * Indicates whether more edges exist prior the set defined by the clients arguments.
   */
  hasPreviousPage: boolean;
  /**
   * When paginating backwards, the cursor to continue.
   */
  startCursor: string | null;
}

export interface PhonesFilteredByName_phones_edges_node_images_edges_node_labels {
  __typename: "ImageLabel";
  label: string;
  confidence: number | null;
  source: string;
}

export interface PhonesFilteredByName_phones_edges_node_images_edges_node {
  __typename: "Image";
  id: string;
  name: string;
  thumbnailName: string;
  labels: PhonesFilteredByName_phones_edges_node_images_edges_node_labels[] | null;
}

export interface PhonesFilteredByName_phones_edges_node_images_edges {
  __typename: "ImageEdge";
  /**
   * The item at the end of the edge.
   */
  node: PhonesFilteredByName_phones_edges_node_images_edges_node;
}

export interface PhonesFilteredByName_phones_edges_node_images {
  __typename: "ImageConnection";
  /**
   * A list of edges.
   */
  edges: PhonesFilteredByName_phones_edges_node_images_edges[] | null;
}

export interface PhonesFilteredByName_phones_edges_node_tenants_residence_property {
  __typename: "Property";
  name: string | null;
}

export interface PhonesFilteredByName_phones_edges_node_tenants_residence {
  __typename: "Residence";
  property: PhonesFilteredByName_phones_edges_node_tenants_residence_property | null;
}

export interface PhonesFilteredByName_phones_edges_node_tenants {
  __typename: "Tenant";
  id: string;
  name: string;
  residence: PhonesFilteredByName_phones_edges_node_tenants_residence | null;
}

export interface PhonesFilteredByName_phones_edges_node {
  __typename: "Phone";
  phoneNumber: string;
  id: string;
  images: PhonesFilteredByName_phones_edges_node_images | null;
  tenants: (PhonesFilteredByName_phones_edges_node_tenants | null)[] | null;
}

export interface PhonesFilteredByName_phones_edges {
  __typename: "PhoneEdge";
  /**
   * The item at the end of the edge.
   */
  node: PhonesFilteredByName_phones_edges_node;
}

export interface PhonesFilteredByName_phones {
  __typename: "PhoneConnection";
  /**
   * Information to aid in pagination.
   */
  pageInfo: PhonesFilteredByName_phones_pageInfo;
  /**
   * A list of edges.
   */
  edges: PhonesFilteredByName_phones_edges[] | null;
}

export interface PhonesFilteredByName {
  phones: PhonesFilteredByName_phones | null;
}

export interface PhonesFilteredByNameVariables {
  name?: string | null;
  limit?: number | null;
  cursor?: string | null;
}
