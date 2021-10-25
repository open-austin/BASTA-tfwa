/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetImagesForPhone
// ====================================================

export interface GetImagesForPhone_phone_images_pageInfo {
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

export interface GetImagesForPhone_phone_images_edges_node_labels {
  __typename: "ImageLabel";
  label: string;
  source: string;
  confidence: number | null;
}

export interface GetImagesForPhone_phone_images_edges_node {
  __typename: "Image";
  id: string;
  name: string;
  thumbnailName: string;
  labels: GetImagesForPhone_phone_images_edges_node_labels[] | null;
}

export interface GetImagesForPhone_phone_images_edges {
  __typename: "ImageEdge";
  /**
   * The item at the end of the edge.
   */
  node: GetImagesForPhone_phone_images_edges_node;
}

export interface GetImagesForPhone_phone_images {
  __typename: "ImageConnection";
  /**
   * Information to aid in pagination.
   */
  pageInfo: GetImagesForPhone_phone_images_pageInfo;
  /**
   * A list of edges.
   */
  edges: GetImagesForPhone_phone_images_edges[] | null;
}

export interface GetImagesForPhone_phone {
  __typename: "Phone";
  images: GetImagesForPhone_phone_images | null;
}

export interface GetImagesForPhone {
  phone: GetImagesForPhone_phone;
}

export interface GetImagesForPhoneVariables {
  id: string;
  limit?: number | null;
  cursor?: string | null;
}
