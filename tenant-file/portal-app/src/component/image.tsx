import React, { useEffect, useState } from "react";
import firebase from "firebase";
import "@firebase/storage";
import { Tooltip } from "reactstrap";
import { BrowserRouter } from "react-router-dom";
import { getToken } from "./firebase";
import axios from "axios";

interface ImageProps {
  name: string;
  id: string;
  // storage: firebase.storage.Storage;
  labels: string[];
}
const Image: React.FC<ImageProps> = ({ name, id, labels }) => {
  const [url, setUrl] = useState("");
  const [tooltipOpen, setTooltipOpen] = useState(false);
  //const [tooltipText, settooltipText] = useState("");
  
  const [token, setToken] = useState("");
  const myHeaders = new Headers();
  myHeaders.append("Authorization", `Bearer ${token}`);
  myHeaders.append("Content-Type", "application/octet-stream");
  myHeaders.append(
    "Content-Disposition",
    `attachment;filename="${name}"; filename*="${name}"`
  );
  myHeaders.append("Access-Control-Allow-Origin", "http://localhost:3000");
  const func = async () => {
    const token = await getToken().then((result) => result);
    const baseUrl =
      process.env.NODE_ENV === "production"
        ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
        : "http://localhost:8080";

    const imageResponse = await axios.get(`${baseUrl}/api/image?name=${name}`, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/octet-stream",
        "Content-Disposition": "attachment",
      },
    });
  };
  const baseUrl =
    process.env.NODE_ENV === "production"
      ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
      : "http://localhost:8080";

  const toggle = () => setTooltipOpen(!tooltipOpen);
  const storage = firebase.app().storage();
  useEffect(() => {
    const onFileChange = async () => {
      if (name !== undefined) {
        const storageRef = storage.ref();
        const promise = storageRef.child(name);
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
  }, [name, storage, labels]);

  return (
    <>
      {/* <form method="get" action={url}>
        
      <button
        type="submit"
        >
        donwload
      </button>
        </form> */}
      {/* <button onClick={() => func()}>download</button> */}
      {/* <a href={`${baseUrl}/api/image?name=${"images/" + name?.split("/")[1]} `}>
        download link
      </a> */}
      <img id={id} src={url} alt="" />
      <button className="btn btn-primary ml-1"
        onClick={() => {
          let anchor = document.createElement("a");
          anchor.style.display = "none";
          anchor.href = `${baseUrl}/api/image?name=${
            "images/" + name?.split("/")[1]
          } `;

          anchor.download = name;
          anchor.click();
        }}
      >
        download
      </button>
      {/* <Tooltip
        placement="bottom"
        isOpen={tooltipOpen}
        target={id}
        toggle={toggle}
        >
        {tooltipText}
      </Tooltip> */}
    </>
  );
};

export default Image;
