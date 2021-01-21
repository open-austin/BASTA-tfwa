import React, { useState } from 'react';
import { TwitterPicker, ColorResult } from 'react-color';
import Tag from './tag';
import StyledEditTagRow from './styles/TagEditRow';
import { Tags } from '../types/tag';

type Props = {
  setData: React.Dispatch<React.SetStateAction<Tags>>;
  toggleAddTag: () => void;
};

const AddTagRow = ({ setData, toggleAddTag }: Props) => {
  const initialState = {
    name: 'New Tag',
    description: '',
    photoCount: 0,
    id: Math.floor(Math.random() * 10000),
    color: '#F78DA7',
  };
  const [editTagFields, setEditTagFields] = useState(initialState);
  const [showColorPicker, setShowColorPicker] = useState(false);

  function handleChange(e: React.ChangeEvent) {
    const target = e.currentTarget as HTMLInputElement;
    setEditTagFields((prevState) => {
      return {
        ...prevState,
        [target.id]: target.value,
      };
    });
  }

  function handleSubmit(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
    // Perform tag updating magic here
    e.preventDefault();
    setData((prevState) => {
      return [editTagFields, ...prevState];
    });
    toggleAddTag();
  }

  function toggleColorPicker() {
    setShowColorPicker(!showColorPicker);
  }

  function hadleColorChangeComplete(color: ColorResult) {
    setEditTagFields((prevState) => {
      return { ...prevState, color: color.hex };
    });
  }

  function cancelAddTag() {
    setEditTagFields(initialState);
    toggleAddTag();
  }

  return (
    <>
      <div className="flex-row" key="add">
        <div className="label">
          <Tag tag={editTagFields} />
        </div>

        <div className="description"></div>
        <div className="photoCount"></div>

        <div className="buttons"></div>
      </div>

      <StyledEditTagRow
        color={editTagFields.color}
        className="flex-row"
        onSubmit={() => console.log('ok!')}
      >
        <div>
          <input
            type="text"
            name="name"
            id="name"
            value={editTagFields.name}
            onChange={handleChange}
            placeholder="Name"
          ></input>
        </div>

        <div>
          <input
            type="text"
            name="description"
            id="description"
            value={editTagFields.description}
            onChange={handleChange}
            placeholder="Description"
          />
        </div>
        <div onClick={toggleColorPicker} className="color_section">
          <div className="color"></div>
          {showColorPicker && (
            <div className="color-picker">
              <TwitterPicker
                onChangeComplete={hadleColorChangeComplete}
                color={editTagFields.color}
              />
            </div>
          )}
        </div>
        <div className="buttons">
          <button onClick={handleSubmit}>Save</button>
          <button onClick={cancelAddTag}>Cancel</button>
        </div>
      </StyledEditTagRow>
    </>
  );
};

export default AddTagRow;
