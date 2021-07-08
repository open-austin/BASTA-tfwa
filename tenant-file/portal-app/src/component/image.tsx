import React, { useEffect, useState } from "react";
import firebase from 'firebase';
import "@firebase/storage";

interface ImageProps {
  name: string;
  storage: firebase.storage.Storage
}
const Image: React.FC<ImageProps> = ({ name, storage }) => {
  console.log(`name ${name}`)
  const [url, setUrl] = useState("");
 
  useEffect(() => {
    const onFileChange = async () => {
      const storageRef = storage.ref()
      const promise = storageRef.child(name)
      setUrl(await promise.getDownloadURL())
    };
    onFileChange();
  }, [name]);

  return <img src={url} />;
};
       
export default Image;
