import React from 'react';
import styled from 'styled-components';

const StyledTagsDisplay = styled.div`
  width: 95%;
  max-width: 1200px;
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
    padding: 1rem;
    border-bottom: 1px solid grey;
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

    .title {
      font-weight: 700;
    }

    .sort {
      text-align: right;
    }
  }

  .flex-row {
    display: flex;
    justify-content: center;

    & > * {
      flex: 1;
      display: flex;
      align-items: center;
    }

    .label span {
      /* TODO Fix stretch */
      display: inline-block;
      padding: 0.1rem 0.5rem;
      background-color: green;
      border-radius: 15px;
      font-weight: 700;
      color: #eee;
      font-size: 0.8rem;
    }
  }

  .body .flex-row:last-child {
    border-bottom: none;
    background-color: rgba(10, 100, 10, 0.5);
    border-radius: 0 0 8px 8px;
  }

  .buttons {
    justify-content: flex-end;

    button {
      border: none;
      box-shadow: none;
      background-color: ${(props) => props.theme.backdrop};
      color: ${(props) => props.theme.primary};
      border-radius: 8px;
      padding: 0 0.6rem;
    }
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
              <div className="label">
                <span>{tag.name}</span>
              </div>
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
