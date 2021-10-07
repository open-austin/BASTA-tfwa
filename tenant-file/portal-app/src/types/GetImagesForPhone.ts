/* tslint:disable */
/* eslint-disable */
// @generated
// This file was automatically generated and should not be edited.

// ====================================================
// GraphQL query operation: GetImagesForPhone
// ====================================================

export interface GetImagesForPhone_phone_images_labels {
  __typename: "ImageLabel";
  label: string;
  source: string;
  confidence: number | null;
}

export interface GetImagesForPhone_phone_images {
  __typename: "Image";
  id: string;
  name: string;
  thumbnailName: string;
  labels: GetImagesForPhone_phone_images_labels[] | null;
}

export interface GetImagesForPhone_phone {
  __typename: "Phone";
  images: (GetImagesForPhone_phone_images | null)[] | null;
}

export interface GetImagesForPhone {
  phone: GetImagesForPhone_phone;
}

export interface GetImagesForPhoneVariables {
  id: string;
}
