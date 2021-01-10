import React, { useState } from 'react';
import EditTagRow from '../component/edit-tag-row';
import AddTagRow from '../component/add-tag-row';
import TagRow from '../component/tag-row';
import StyledTagsDisplay from '../component/styles/TagsPage';
import { sampleData } from '../utility';

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
              <TagRow
                tag={tag}
                setEditingRow={setEditingRow}
                handleDelete={handleDelete}
              />
            );
          })}
        </div>
      </div>
    </StyledTagsDisplay>
  );
};

export default Tags;
