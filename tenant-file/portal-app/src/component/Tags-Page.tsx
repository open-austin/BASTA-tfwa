import React, { useState } from 'react';
import styled from 'styled-components';
import EditTagRow from './edit-tag-row';
import AddTagRow from './add-tag-row';
import Tag from './tag';

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
    display: flex;
    background-color: rgba(0, 0, 0, 0.08);
    border-radius: 8px;

    & > * {
      flex: 1;
    }

    .title {
      font-weight: 700;
    }

    .add {
      text-align: right;

      button {
        width: 30px;
        height: 30px;
        border: none;
        border-radius: 50%;
        background-color: rgba(0, 0, 0, 0.04);
        text-align: center;
        position: relative;
        transition: background-color ease 0.4s;

        &:hover {
          background-color: rgba(0, 0, 0, 0.4);
        }

        span {
          display: block;
          position: absolute;
          top: 2px;
          left: 10px;
        }
      }
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
  // Sample data stored here. Can be swapped with redux or other method later.
  const [data, setData] = useState(sampleData);
  // Editing row shows on whichever tag id is stored in this state. Setting to -1 shows no editing row.
  const [editingRow, setEditingRow] = useState(-1);
  const [isAddTagShowing, setIsAddTagShowing] = useState(false);

  function toggleAddTag() {
    setIsAddTagShowing(!isAddTagShowing);
  }

  function handleDelete(tagId: number) {
    if (window.confirm('Do you really want to delete this tag?')) {
      // Clever and clean solution for deleting tag here
      setData((prevState) => {
        return prevState.filter((item) => item.id !== tagId);
      });
      window.alert(`deleted tag with id of ${tagId}`);
    }
  }

  function renderTagRow(tag: Tag) {
    return (
      <div className="flex-row" key={tag.id}>
        <div className="label">
          <Tag tag={tag} />
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
          <div className="title">{data.length} Tags</div>
          <div className="add">
            <button onClick={toggleAddTag}>
              <span>+</span>
            </button>
          </div>
        </div>
        <div className="body">
          {isAddTagShowing && (
            <AddTagRow setData={setData} toggleAddTag={toggleAddTag} />
          )}
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
