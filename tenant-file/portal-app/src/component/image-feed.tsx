import React, { useEffect, useState } from 'react';
import styled from 'styled-components';

const StyledImageFeed = styled.div`
  display: grid;
  gap: 0.5rem;
  width: 90%;
  max-width: 1600px;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  justify-content: center;
  margin: 0 auto;

  .image_container {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    overflow: hidden;
    max-height: 140px;
  }
`;

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

  return (
    <StyledImageFeed>
      {images.map((image) => {
        return (
          <div className="image_container">
            <img src={image.download_url} alt="" style={{ width: '100%' }} />
          </div>
        );
      })}
    </StyledImageFeed>
  );
};

export default ImageFeed;
