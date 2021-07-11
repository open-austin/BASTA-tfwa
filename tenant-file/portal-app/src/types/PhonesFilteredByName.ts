/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: PhonesFilteredByName
// ====================================================

export interface PhonesFilteredByName_phones_edges_node_images_labels {
  __typename: "ImageLabel";
  label: string;
  confidence: number | null;
  source: string;
}

export interface PhonesFilteredByName_phones_edges_node_images {
  __typename: "Image";
  id: string;
  name: string;
  thumbnailName: string;
  labels: PhonesFilteredByName_phones_edges_node_images_labels[] | null;
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
  images: (PhonesFilteredByName_phones_edges_node_images | null)[] | null;
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
   * A list of edges.
   */
  edges: PhonesFilteredByName_phones_edges[] | null;
}

export interface PhonesFilteredByName {
  phones: PhonesFilteredByName_phones | null;
}

export interface PhonesFilteredByNameVariables {
  name?: string | null;
}
