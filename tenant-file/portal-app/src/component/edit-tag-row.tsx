import React, { useState } from 'react';
import { TwitterPicker, ColorResult } from 'react-color';
import styled from 'styled-components';
import Tag from './tag';

const StyledEditTagRow = styled.form`
  position: relative;

  .color_section {
    position: relative;
  }
  .color {
    width: 30px;
    height: 30px;
    cursor: pointer;
    border-radius: 4px;
    background-color: ${(props) => props.color};
    position: relative;
  }

  .color-picker {
    position: absolute;
    transform: translate(-5px, 35px);
  }
`;

type Tag = {
  name: string;
  description: string;
  photoCount: number;
  id: number;
  color: string;
};

type Tags = {
  name: string;
  description: string;
  photoCount: number;
  id: number;
  color: string;
}[];

type Props = {
  tag: Tag;
  setEditingRow: React.Dispatch<React.SetStateAction<number>>;
  setData: React.Dispatch<React.SetStateAction<Tags>>;
};

const EditTagRow = ({ tag, setEditingRow, setData }: Props) => {
  const [editTagFields, setEditTagFields] = useState(tag);
  const [showColorPicker, setShowColorPicker] = useState(false);

  function handleChange(e: React.ChangeEvent) {
    const target = e.currentTarget as HTMLInputElement;
    setEditTagFields((prevState) => ({
      ...prevState,
      [target.id]: target.value,
    }));
  }

  function handleSubmit() {
    // Perform tag updating magic here
    setData((prevState) => {
      const index = prevState.findIndex((item) => item.id === tag.id);
      prevState[index] = { ...prevState[index], ...editTagFields };
      return prevState;
    });
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
          <Tag tag={tag} />
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
        <div onClick={toggleColorPicker} className="color_section">
          <div className="color"></div>
          {showColorPicker && (
            <div
              className="color-picker"
              style={{
                position: 'absolute',
                top: '50%',
                left: 0,
              }}
            >
              <TwitterPicker
                onChangeComplete={hadleColorChangeComplete}
                color={editTagFields.color}
              />
            </div>
          )}
        </div>
        <div className="buttons">
          <button onClick={handleSubmit}>Save</button>
          {/* Closes edit row */}
          <button onClick={() => setEditingRow(-1)}>Cancel</button>
        </div>
      </StyledEditTagRow>
    </>
  );
};

export default EditTagRow;
