import React from 'react';
import styled from 'styled-components';

const StyledTagsDisplay = styled.div`
  width: 90%;
  margin: 0 auto;
  border: 1px solid grey;
  display: flex;
  border-radius: 10px;

  .display,
  .header,
  .body,
  .flex-row {
    width: 100%;
  }

  .header,
  .flex-row {
    padding: 0.5rem;
  }

  .header {
    background-color: lightgray;
    border-radius: 8px 8px 0 0;
  }

  .header {
    display: flex;

    & > * {
      flex: 1;
    }

    .sort {
      text-align: right;
    }
  }

  .flex-row {
    display: flex;
    border-bottom: 1px solid grey;

    & > * {
      flex: 1;
    }
  }

  .body .flex-row:last-child {
    border-bottom: none;
  }

  .buttons {
    text-align: right;
  }
`;

const Tags = () => {
  const data = [
    {
      name: 'Chris',
      description: 'Good guy',
      photoCount: 5,
    },
    {
      name: 'Jenn',
      description: 'cool gal',
      photoCount: 4,
    },
    {
      name: 'Elvis',
      description: 'Big guy',
      photoCount: 5,
    },
  ];

  return (
    <StyledTagsDisplay>
      <div className="display">
        <div className="header">
          <div className="title">12 labels</div>
          <div className="sort">
            <button>sort</button>
          </div>
        </div>
        <div className="body">
          {data.map((tag) => (
            <div className="flex-row">
              <div className="label">{tag.name}</div>
              <div className="description">{tag.description}</div>
              <div className="photoCount">{tag.photoCount}</div>
              <div className="buttons">
                <button>Edit</button>
                <button>Delete</button>
              </div>
            </div>
          ))}
        </div>
      </div>
    </StyledTagsDisplay>
  );
};

export default Tags;
