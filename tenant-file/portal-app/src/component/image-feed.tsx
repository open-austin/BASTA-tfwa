import React, { useEffect, useState } from 'react';
import styled from 'styled-components';

const StyledImageFeed = styled.div`
  width: 90%;
  max-width: 1600px;
  margin: 0 auto;

  .filters label {
    margin-right: 1rem;
  }

  .change_order {
    position: relative;
    width: 2rem;
    align-self: stretch;
    font-size: 1.1rem;
    background-color: rgba(0, 0, 0, 0);
    border: none;
    & i:first-child {
      position: absolute;
      top: 0;
      color: ${(props: Props) =>
        props.sortOrder === -1 ? 'rgba(0,0,0,1)' : 'rgba(0,0,0,0.3)'};
    }
    & i:last-child {
      position: relative;
      transform: translateY(0.3rem);
      color: ${(props: Props) =>
        props.sortOrder === 1 ? 'rgba(0,0,0,1)' : 'rgba(0,0,0,0.3)'};
    }
  }

  .grid {
    display: grid;
    gap: 0.5rem;
    grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
    justify-content: center;
    margin: 0 auto;
    width: 100%;
  }

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

// Type for sample data from Lorem Picsum, can be scrapped later
interface Image {
  id: string;
  author: string;
  width: number;
  height: number;
  url: string;
  download_url: string;
}

interface Props {
  sortOrder: number;
}

const ImageFeed = () => {
  const [images, setImages] = useState<Image[]>([]);
  const [sortField, setSortField] = useState<string>('id');
  const [sortOrder, setSortOrder] = useState(1);
  useEffect(() => {
    async function getImages() {
      const blob = await fetch('https://picsum.photos/v2/list');
      const result = await blob.json();
      console.log(result);
      setImages(result);
    }
    getImages();
  }, []);

  function sortImages() {
    // Correct types here when incorperating images from back end
    const sortedImages = [...images].sort((a: Image, b: Image) => {
      if (a[sortField as keyof Image] > b[sortField as keyof Image]) {
        return 1 * sortOrder;
      } else if (a[sortField as keyof Image] < b[sortField as keyof Image]) {
        return -1 * sortOrder;
      } else {
        return 0;
      }
    });
    return sortedImages;
  }

  function handleFieldChange(event: React.ChangeEvent<HTMLSelectElement>) {
    setSortField(event.currentTarget.value);
  }

  return (
    <StyledImageFeed sortOrder={sortOrder}>
      <div className="filters">
        <label htmlFor="sort_field">Sort by:</label>
        <select
          name="sort_field"
          onChange={handleFieldChange}
          value={sortField}
        >
          <option value={'id'}>Id</option>
          <option value={'author'}>Author</option>
        </select>
        <button
          className="change_order"
          onClick={() => {
            setSortOrder(sortOrder * -1);
          }}
        >
          <i className="las la-angle-up"></i>
          <i className="las la-angle-down"></i>
        </button>
      </div>
      <div className="grid">
        {sortImages().map((image) => {
          return (
            <div className="image_container">
              <img src={image.download_url} alt="" style={{ width: '100%' }} />
            </div>
          );
        })}
      </div>
    </StyledImageFeed>
  );
};

export default ImageFeed;
