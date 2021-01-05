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

  function handleChange(e: React.ChangeEvent) {
    const target = e.currentTarget as HTMLInputElement;
    console.log(target.id);
    setEditTagFields((prevState) => ({
      ...prevState,
      [target.id]: target.value,
    }));
    console.log(editTagFields);
  }

  function handleSubmit() {
    // Perform tag updating magic
    window.alert(JSON.stringify(editTagFields));
    setEditingRow(-1);
  }

  function toggleColorPicker() {}

  return (
    <>
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

        <div className="description"></div>
        <div className="photoCount"></div>

        <div className="buttons"></div>
      </div>

      <form className="flex-row" onSubmit={() => console.log('ok!')}>
        <div>
          <input
            type="text"
            name="name"
            id="name"
            value={editTagFields.name}
            onChange={handleChange}
          ></input>
        </div>

        <div>
          <input
            type="text"
            name="description"
            id="description"
            value={editTagFields.description}
            onChange={handleChange}
          />
        </div>
        <div onClick={toggleColorPicker}>
          <input
            type="text"
            name="color"
            id="color"
            value={editTagFields.color}
            onChange={handleChange}
          />
        </div>
        <div className="buttons">
          <button onClick={handleSubmit}>Save</button>
          <button onClick={() => setEditingRow(-1)}>Cancel</button>
        </div>
      </form>
    </>
  );
};

export default EditTagRow;
