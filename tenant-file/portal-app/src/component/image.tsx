import React, { useEffect, useState } from "react";
import axios from "axios";
import { getToken } from "./firebase";

interface ImageProps {
  name: string;
}

const Image: React.FC<ImageProps> = ({ name }) => {
  const [url, setUrl] = useState("");
  useEffect(() => {
    const func = async () => {
      const token = await getToken();
      const imageResponse = await axios
        .get(`${process.env.REACT_APP_API_URL}/api/image?name=${name}`, {
          responseType: "arraybuffer",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((x) => x.data);
      const base64 = Buffer.from(imageResponse, "binary").toString("base64");
      setUrl("data:image/png;base64," + base64);
      //   const blob = new Blob([imageResponse], { type: "image/png" });
      //   console.log("RESPONSE", blob);
      //   setUrl(window.URL.createObjectURL(blob));
    };
    func();
  }, [name]);
  return <img src={url} />;
};

export default Image;
