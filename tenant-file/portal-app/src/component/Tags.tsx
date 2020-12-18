import React, { useState } from 'react';
import styled from 'styled-components';

const StyledTagsDisplay = styled.div`
  width: 95%;
  max-width: 1200px;
  margin: 0 auto;
  border: 1px solid rgba(0, 0, 0, 0.3);
  display: flex;
  border-radius: 10px;
  box-shadow: 0 1px 8px 0 rgba(0, 0, 0, 0.1);

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
    border-bottom: 1px solid rgba(0, 0, 0, 0.3);
  }

  .header {
    background-color: rgba(0, 0, 0, 0.08);
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
    font-size: 0.8rem;

    & > * {
      flex: 1;
      display: flex;
      align-items: center;
    }

    .label button {
      /* TODO Fix stretch */
      display: inline-block;
      padding: 0.1rem 0.5rem;
      background-color: green;
      border-radius: 15px;
      font-weight: 700;
      color: #eee;
      box-shadow: none;
      border: none;
    }
  }

  .body .flex-row:last-child {
    border-bottom: none;
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
      padding: 0.3rem 0.8rem;
      font-size: 0.8rem;
      transition: color 0.4s ease;
    }

    button:hover {
      color: ${(props) => props.theme.accent};
    }

    button:first-child {
      border-radius: 8px 0 0 8px;
      border-right: 1px solid white;
      width: 64px;
    }

    button:last-child {
      border-radius: 0 8px 8px 0;
    }
  }
`;

const Tags = () => {
  const data = [
    {
      name: 'Chris',
      description: 'Good guy',
      photoCount: 5,
      id: 0,
    },
    {
      name: 'Jenn',
      description: 'cool gal',
      photoCount: 4,
      id: 1,
    },
    {
      name: 'Elvis',
      description: 'Big guy',
      photoCount: 5,
      id: 2,
    },
  ];

  const [editingRow, setEditingRow] = useState(-1);

  return (
    <StyledTagsDisplay>
      <div className="display">
        <div className="header">
          <div className="title">{data.length} labels</div>
          <div className="sort">{/* <button>sort</button> */}</div>
        </div>
        <div className="body">
          {data.map((tag) => (
            <>
              <div className="flex-row" key={tag.id}>
                <div className="label">
                  <button>{tag.name}</button>
                </div>
                {editingRow !== tag.id && (
                  <>
                    <div className="description">{tag.description}</div>
                    <div className="photoCount">{tag.photoCount}</div>
                  </>
                )}
                <div className="buttons">
                  <button onClick={() => setEditingRow(tag.id)}>Edit</button>
                  <button>Delete</button>
                </div>
              </div>

              {editingRow === tag.id && (
                <form className="flex-row" onSubmit={() => console.log('ok!')}>
                  <div className="label">
                    <button>{tag.name}</button>
                  </div>

                  <div className="description">{tag.description}</div>
                  <div className="photoCount">{tag.photoCount}</div>

                  <div className="buttons">
                    <button onClick={() => setEditingRow(tag.id)}>Edit</button>
                    <button>Delete</button>
                  </div>
                </form>
              )}
            </>
          ))}
        </div>
      </div>
    </StyledTagsDisplay>
  );
};

export default Tags;
