/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: getProps
// ====================================================

export interface getProps_properties_nodes {
  __typename: "Property";
  id: string;
  name: string | null;
}

export interface getProps_properties {
  __typename: "PropertyConnection";
  /**
   * A flattened list of the nodes.
   */
  nodes: getProps_properties_nodes[] | null;
}

export interface getProps {
  properties: getProps_properties | null;
}
