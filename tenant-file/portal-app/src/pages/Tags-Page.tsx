import React, { useState } from 'react';
import EditTagRow from '../component/edit-tag-row';
import AddTagRow from '../component/add-tag-row';
import Tag from '../component/tag';
import StyledTagsDisplay from '../component/styles/TagsPage';

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
