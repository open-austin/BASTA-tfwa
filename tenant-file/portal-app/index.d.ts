declare module "react-google-picker";
declare module "@google.picker";

// interface batchImageData {
//   imageUrl: string;
//   tenantName: string;
//   phoneNumber: string;
//   labels: string[];
// }
export interface BatchImageData {
  imageUrl: string | null;
  tenantName: string | null;
  phoneNumber: string | null;
  labels: GetImagesForPhone_phone_images_labels[] | null;
}