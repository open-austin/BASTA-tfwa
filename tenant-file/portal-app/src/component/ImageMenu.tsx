import React from "react";
import { BatchImageData } from "../..";
import { useImageCart } from "../hooks/useImageCart";
import { imageCartVar } from "../cache";

interface ImageMenuProps {
  imageData: BatchImageData;
}
const ImageMenu: React.FC<ImageMenuProps> = ({ imageData }) => {
  const { operations } = useImageCart(imageCartVar);
  return (
    <ul className="menu">
      <li
        onClick={() => {
          console.log(`imageDate = ${imageData.phoneNumber}`);
          console.log(`imageDate = ${imageData.imageUrl}`);
          console.log(`imageDate = ${imageData.tenantName}`);
          operations.addImage(imageData);
        }}
      >
        Add Image to Bundle
      </li>
    </ul>
  );
};
export default ImageMenu;

// menu={() => (
//   <ImageMenu
//     imageData={{
//       imageUrl: i[1],
//       phoneNumber: row.original.phone,
//       tenantName: row.original.name,
//       labels: row.original.labels,
//     }}
//   ></ImageMenu>
