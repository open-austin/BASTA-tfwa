import React, { useEffect, useState } from 'react';

const ImageFeed = () => {
  const [images, setImages] = useState<any[]>([]);

  useEffect(() => {
    async function getImages() {
      const blob = await fetch('https://picsum.photos/v2/list');
      const result = await blob.json();
      console.log(result);
      setImages(result);
    }
    getImages();
  }, []);

  return <div>{images.map((image) => image?.url)}</div>;
};

export default ImageFeed;
