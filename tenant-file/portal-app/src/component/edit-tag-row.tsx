import React, { useState } from 'react';
import { TwitterPicker } from 'react-color';
import { isDark } from '../utility';

type Tag = {
  name: string;
  description: string;
  photoCount: number;
  id: number;
  color: string;
};

type Props = {
  tag: Tag;
  setEditingRow: React.Dispatch<React.SetStateAction<number>>;
};

const EditTagRow = ({ tag, setEditingRow }: Props) => {
  const [editTagFields, setEditTagFields] = useState(tag);

  function handleChange() {}
  return (
    <>
      <div className="flex-row" key={tag.id}>
        <div className="label">
          <input
            type="text"
            name="name"
            id="name"
            value={editTagFields.name}
            onChange={handleChange}
          ></input>
        </div>

        <div className="description">
          <input type="text" name="description" id="description" />
        </div>
        <div className="photoCount">Color</div>

        <div className="buttons">
          <button onClick={() => setEditingRow(-1)}>Save</button>
          <button onClick={() => setEditingRow(-1)}>Discard</button>
        </div>
      </div>

      <form className="flex-row" onSubmit={() => console.log('ok!')}>
        <div className="label">
          <button>{tag.name}</button>
        </div>

        <div className="description">{tag.description}</div>
        <div className="photoCount">{tag.photoCount}</div>

        <div className="buttons">
          <TwitterPicker />
        </div>
      </form>
    </>
  );
};

export default EditTagRow;
