import React, { useEffect, useState } from "react";
import firebase from "firebase";
import "@firebase/storage";
// import { Tooltip } from "reactstrap";
// import { BrowserRouter } from "react-router-dom";
import { getToken } from "./firebase";
// import axios from "axios";
// import ContextMenu from "./ContextMenu";
import { imageCartVar } from "../cache";
// import styles from "./tenant-details.module.css";

interface ImageProps {
  imageName: string;
  id: string;
  tenantName: string;
  phoneNumber: string;
  // storage: firebase.storage.Storage;
  labels: string[];
}
const Image: React.FC<ImageProps> = ({
  imageName,
  id,
  tenantName,
  phoneNumber,
  labels,
}) => {
  const [url, setUrl] = useState("");
  // const [tooltipOpen, setTooltipOpen] = useState(false);
  //const [tooltipText, settooltipText] = useState("");

  // const [show, setShow] = useState(false);
  const [token, setToken] = useState("");
  const myHeaders = new Headers();
  myHeaders.append("Authorization", `Bearer ${token}`);
  myHeaders.append("Content-Type", "application/octet-stream");
  myHeaders.append(
    "Content-Disposition",
    `attachment;filename="${imageName}"; filename*="${imageName}"`
  );
  myHeaders.append("Access-Control-Allow-Origin", "http://localhost:3000");
  // const func = async () => {
  //   const token = await getToken().then((result) => result);
  //   const baseUrl =
  //     process.env.NODE_ENV === "production"
  //       ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
  //       : "http://localhost:8080";

  //   // const imageResponse = await axios.get(
  //   //   `${baseUrl}/api/image?name=${imageName}`,
  //   //   {
  //   //     headers: {
  //   //       Authorization: `Bearer ${token}`,
  //   //       "Content-Type": "application/octet-stream",
  //   //       "Content-Disposition": "attachment",
  //   //     },
  //   //   }
  //   // );
  // };
  const baseUrl =
    process.env.NODE_ENV === "production"
      ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
      : "http://localhost:8080";

  // const toggle = () => setTooltipOpen(!tooltipOpen);
  const storage = firebase.app().storage();
  useEffect(() => {
    const onFileChange = async () => {
      if (imageName !== undefined) {
        const storageRef = storage.ref();
        const promise = storageRef.child(imageName);
        setUrl(await promise.getDownloadURL());
        setToken((await getToken()) ?? "");
        // if (labels !== undefined) {

        //   settooltipText(
        //     labels.reduce((text, label) => text + "\n" + label, "")
        //   );
        // }
      }
    };
    onFileChange();
  }, [imageName, storage, labels]);

  return (
    <span className="clickDiv">
      {url ? (
        <>
          <div>
          <img className="downloadableImage" id={id} src={url} alt="" />

          <button
            className="btn btn-sm btn-link"
            onClick={() => {
              let anchor = document.createElement("a");
              anchor.style.display = "none";
              anchor.href = `${baseUrl}/api/image?name=${
                "images/" + imageName?.split("/")[1]
              } `;
              
              anchor.download = imageName;
              anchor.click();
            }}
            >
            download
          </button>
          <button
            className="btn btn-sm btn-link"
            onClick={() => {
              imageCartVar().map((i) =>
              console.log(`imageCartVar ${i.phoneNumber} ${i.tenantName}`)
              );

              imageCartVar([
                ...imageCartVar(),
                {
                  imageUrl: imageName,
                  phoneNumber: phoneNumber,
                  tenantName: tenantName,
                  labels: labels,
                },
              ]);
            }}
            >
            add image
          </button>
            </div>
          {/* <Tooltip
          placement="bottom"
        isOpen={tooltipOpen}
        target={id}
        toggle={toggle}
        >
        {tooltipText}
      </Tooltip> */}

          {/* <ContextMenu
            commands={[
              {
                displayText: "add image",
                commandFunc: () => {
                  imageCartVar().map((i) =>
                    console.log(`imageCartVar ${i.phoneNumber} ${i.tenantName}`)
                  );

                  imageCartVar([
                    ...imageCartVar(),
                    {
                      imageUrl: imageName,
                      phoneNumber: phoneNumber,
                      tenantName: tenantName,
                      labels: labels,
                    },
                  ]);
                },
              },
            ]}
          ></ContextMenu> */}
        </>
      ) : (
        <></>
      )}
    </span>
  );
};

export default Image;
