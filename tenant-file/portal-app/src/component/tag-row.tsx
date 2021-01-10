import React from 'react';
import TagButton from './tag';
import { Tag } from '../types/tag';

type Props = {
  tag: Tag;
  setEditingRow: React.Dispatch<React.SetStateAction<number>>;
  handleDelete: (tagId: number) => void;
};

const TagRow = ({ tag, setEditingRow, handleDelete }: Props) => {
  return (
    <div className="flex-row" key={tag.id}>
      <div className="label">
        <TagButton tag={tag} />
      </div>
      <div className="description">{tag.description}</div>
      <div className="photoCount">{tag.photoCount}</div>
      <div className="buttons">
        <button onClick={() => setEditingRow(tag.id)}>Edit</button>
        <button onClick={() => handleDelete(tag.id)}>Delete</button>
      </div>
    </div>
  );
};

export default TagRow;
