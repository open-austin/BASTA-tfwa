import React from "react";
// import { getToken } from "./firebase";
import axios from "axios";
// import {
//   GetImagesForPhone,
//   GetImagesForPhone_phone_images,
//   GetImagesForPhone_phone_images_labels,
// } from "../types/GetImagesForPhone";
// import { BatchImageData } from "../..";
// interface BatchImageProps {
//   imageData: batchImageData[];
// }
// interface BatchImageProps {
//   images: string[];
//   baseUrl: string;
//   filename: string;
//   url: string;
// }
const BatchImageSelector: React.FC = () => {
  // const getImages = () => {
  //   let anchor = new HTMLAnchorElement();
  //   anchor.style.display = "none";
  //   anchor.href = url;

  //   anchor.download = filename;
  //   anchor.click();
  // };
  // const [imageCart, setImageCart] = useState<BatchImageData[]>();
  // const enqueImage = (imageData: BatchImageData) => {
  //   setImageCart([imageData, ...imageCart!]);
  // };

  const baseUrl =
    process.env.NODE_ENV === "production"
      ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
      : "http://localhost:8080";

  // const [token, setToken] = useState("");
  // const myHeaders = new Headers();
  // myHeaders.append("Authorization", `Bearer ${token}`);
  // myHeaders.append("Content-Type", "application/octet-stream");
  // myHeaders.append(
  //   "Content-Disposition",
  //   `attachment;filename="${name}"; filename*="${name}"`
  // );
  // myHeaders.append("Access-Control-Allow-Origin", "http://localhost:3000");
  // const func = async () => {
  //   const token = await getToken().then((result) => result);

  //   const imageResponse = await axios.post(
  //     `${baseUrl}/api/image?name=${name}`,
  //     {
  //       headers: {
  //         Authorization: `Bearer ${token}`,
  //         "Content-Type": "application/octet-stream",
  //         "Content-Disposition": "attachment",
  //       },
  //     }
  //   );
  // };
  // const getImages = () => {
  //   let anchor = new HTMLAnchorElement();
  //   anchor.style.display = "none";
  //   anchor.href = `${baseUrl}/api/batchImages?name=${
  //     "images/" + images.join("^")
  //   } `;
  //   anchor.download = filename;
  //   anchor.click();
  // };
  const getImages = () => {
    const func = async () => {
      // const token = await getToken().then((result) => result);

      const imagePackage = await axios
        .post(`${baseUrl}/api/batchImage`, {
          // headers: {
          //   Authorization: `Bearer ${token}`,
          //   "Content-Type": "application/octet-stream",
          //   "Content-Disposition": "attachment",
          // },
          data: {
            imageCart,
          },
        })
        .then((result) => result);
      let anchor = new HTMLAnchorElement();
      anchor.style.display = "none";
      anchor.href = `https://storage.cloud.google.com/tenant-file-fc6de.appspot.com/packages/2aafab94-3ff1-4fcf-80a9-60e87ea8473d.zip`;
      anchor.download = imagePackage.data;
      anchor.click();
    };
    func();
  };

  return (
    <div>
      <button className="btn" onClick={() => getImages()}>
        Get Selected Images
      </button>
    </div>
  );
};
// const BatchImageSelector: React.FC<BatchImageProps> = ({
//   images,
//   baseUrl,
//   filename,
//   url,
// }) => {
//   const getImages = () => {
//     let anchor = new HTMLAnchorElement();
//     anchor.style.display = "none";
//     anchor.href = url;

//     anchor.download = filename;
//     anchor.click();
//   };

//   const [token, setToken] = useState("");

//   const func = async () => {
//     const token = await getToken().then((result) => result);
//     const baseUrl =
//       process.env.NODE_ENV === "production"
//         ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
//         : "http://localhost:8080";

//     const imageResponse = await axios.post(`${baseUrl}/api/image?name=${name}`, {
//       headers: {
//         Authorization: `Bearer ${token}`,
//         "Content-Type": "application/octet-stream",
//         "Content-Disposition": "attachment",
//       },
//     });
//   };
//   //   const getImages = () => {
//   //     let anchor = new HTMLAnchorElement();
//   //     anchor.style.display = "none";
//   //     anchor.href = `${baseUrl}/api/batchImages?name=${
//   //       "images/" + images.join("^")
//   //     } `;
//   //     anchor.download = filename;
//   //     anchor.click();
//   //   };

//   return (
//     <div>
//       <button className="btn" onClick={() => getImages()}>
//         Get Selected Images
//       </button>
//     </div>
//   );
// };
export default BatchImageSelector;
