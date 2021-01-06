import React, { useState } from 'react';
import { TwitterPicker, ColorResult } from 'react-color';
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
  const [showColorPicker, setShowColorPicker] = useState(false);

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

  function toggleColorPicker() {
    setShowColorPicker(!showColorPicker);
  }

  function hadleColorChangeComplete(color: ColorResult) {
    setEditTagFields((prevState) => {
      return { ...prevState, color: color.hex };
    });
  }

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
            onFocus={toggleColorPicker}
          />
          {showColorPicker && (
            <div
              className="color-picker"
              style={{
                position: 'absolute',
                top: '50%',
                left: 0,
              }}
            >
              {/* TODO: Handle color picker */}
              <TwitterPicker
                onChangeComplete={hadleColorChangeComplete}
                color={tag.color}
              />
            </div>
          )}
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
