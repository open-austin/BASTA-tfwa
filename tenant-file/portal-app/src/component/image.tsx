import React, { useEffect, useState } from "react";
import firebase from "firebase";
import "@firebase/storage";
import { Tooltip } from "reactstrap";

interface ImageProps {
  name: string;
  id: string;
  storage: firebase.storage.Storage;
  labels: string[];
}
const Image: React.FC<ImageProps> = ({ name, id, storage, labels }) => {
  console.log(`name ${name}`);
  const [url, setUrl] = useState("");
  const [tooltipOpen, setTooltipOpen] = useState(false);
  const [tooltipText, settooltipText] = useState("");
  const toggle = () => setTooltipOpen(!tooltipOpen);

  useEffect(() => {
    const onFileChange = async () => {
      if (name !== undefined) {
        const storageRef = storage.ref();
        const promise = storageRef.child(name);
        setUrl(await promise.getDownloadURL());
        if (labels !== undefined) {
          console.log(`name ${name} labels ${labels}`);
          settooltipText(
            labels.reduce((text, label) => text + "\n" + label, "")
          );
        }
      }
    };
    onFileChange();
  }, [name, storage, labels]);

  return (
    <>
      <img id={id} src={url} alt="" />
      <Tooltip
        placement="bottom"
        isOpen={tooltipOpen}
        target={id}
        toggle={toggle}
      >
        {tooltipText}
      </Tooltip>
    </>
  );
};

export default Image;
