import React, { useState } from 'react';
import styled from 'styled-components';
import EditTagRow from './edit-tag-row';
import AddTagRow from './add-tag-row';
import { isDark } from '../utility';

const StyledTagsDisplay = styled.div`
  width: 95%;
  max-width: 1200px;
  margin: 0 auto;
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
  }

  .header {
    background-color: rgba(0, 0, 0, 0.08);
    border-radius: 8px;
  }

  .header {
    display: flex;

    & > * {
      flex: 1;
    }

    .title {
      font-weight: 700;
    }

    .add {
      text-align: right;
    }
  }

  .flex-row {
    display: flex;
    justify-content: center;
    font-size: 0.8rem;
    border-bottom: 1px solid rgba(0, 0, 0, 0.3);

    & > * {
      flex: 1;
      display: flex;
      align-items: center;
    }

    .label button {
      display: inline-block;
      padding: 0.1rem 0.5rem;
      background-color: green;
      border-radius: 15px;
      font-weight: 700;
      color: rgba(220, 220, 220, 0.5);
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

const sampleData = [
  {
    name: 'Damages',
    description: 'Windows, walls, appliance',
    photoCount: 5,
    id: 0,
    color: '#00D084',
  },
  {
    name: 'Conversations',
    description: 'Records of texts or phone calls',
    photoCount: 4,
    id: 1,
    color: '#0693E3',
  },
  {
    name: 'Documents',
    description: 'Lease, notices, etc.',
    photoCount: 5,
    id: 2,
    color: '#FF6900',
  },
];

type Tag = {
  name: string;
  description: string;
  photoCount: number;
  id: number;
  color: string;
};

const Tags = () => {
  const [data, setData] = useState(sampleData);
  const [editingRow, setEditingRow] = useState(-1);

  function handleDelete(tagId: number) {
    if (window.confirm('Do you really want to delete this tag?')) {
      // Clever and clean solution for deleting tag here
      window.alert(`deleted tag with id of ${tagId}`);
      setData((prevState) => {
        return prevState.filter((item) => item.id !== tagId);
      });
    }
  }

  function renderTagRow(tag: Tag) {
    return (
      <div className="flex-row" key={tag.id}>
        <div className="label">
          <button
            style={{
              backgroundColor: tag.color,
              color: isDark(tag.color) ? 'white' : 'black',
            }}
          >
            {tag.name}
          </button>
        </div>

        <div className="description">{tag.description}</div>
        <div className="photoCount">{tag.photoCount}</div>

        <div className="buttons">
          <button onClick={() => setEditingRow(tag.id)}>Edit</button>
          <button onClick={() => handleDelete(tag.id)}>Delete</button>
        </div>
      </div>
    );
  }

  return (
    <StyledTagsDisplay>
      <div className="display">
        <div className="header">
          <div className="title">{data.length} labels</div>
          <div className="add">
            <button>+</button>
          </div>
        </div>
        <div className="body">
          <AddTagRow setData={setData} />
          {data.map((tag) => {
            return editingRow === tag.id ? (
              <EditTagRow
                tag={tag}
                setEditingRow={setEditingRow}
                setData={setData}
              />
            ) : (
              renderTagRow(tag)
            );
          })}
        </div>
      </div>
    </StyledTagsDisplay>
  );
};

export default Tags;
