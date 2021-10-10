import { ReactiveVar } from "@apollo/client";
import { BatchImageData } from "../..";

export function useImageCart(imageCartVar: ReactiveVar<BatchImageData[]>) {
  const addImage = (imageData: BatchImageData) => {
    const allImages = imageCartVar();
    imageCartVar([...allImages, imageData]);
  };

  return {
    operations: { addImage },
  };
}
