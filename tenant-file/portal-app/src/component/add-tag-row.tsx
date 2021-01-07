import React, { useState } from 'react';
import { TwitterPicker, ColorResult } from 'react-color';
import { isDark } from '../utility';
import styled from 'styled-components';

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
    transform: translate(-5px, 30px);
  }
`;

type Tags = {
  name: string;
  description: string;
  photoCount: number;
  id: number;
  color: string;
}[];

type Props = {
  setData: React.Dispatch<React.SetStateAction<Tags>>;
};

const AddTagRow = ({ setData }: Props) => {
  const initialState = {
    name: '',
    description: '',
    photoCount: 0,
    id: Math.floor(Math.random() * 10000),
    color: 'red',
  };
  const [editTagFields, setEditTagFields] = useState(initialState);
  const [showColorPicker, setShowColorPicker] = useState(false);

  function handleChange(e: React.ChangeEvent) {
    //   const target = e.currentTarget as HTMLInputElement;
    //   console.log(target.id);
    //   setEditTagFields((prevState) => ({
    //     ...prevState,
    //     [target.id]: target.value,
    //   }));
    //   console.log(editTagFields);
  }

  function handleSubmit() {
    // Perform tag updating magic
    setData((prevState) => {
      return [editTagFields, ...prevState];
    });
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
      <div className="flex-row" key="add">
        <div className="label">
          <button
            style={{
              backgroundColor: editTagFields.color,
              color: isDark(editTagFields.color) ? 'white' : 'black',
            }}
          >
            "New Tag"
          </button>
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
              {/* TODO: Handle color picker */}
              <TwitterPicker
                onChangeComplete={hadleColorChangeComplete}
                color={editTagFields.color}
              />
            </div>
          )}
        </div>
        <div className="buttons">
          <button onClick={handleSubmit}>Save</button>
          <button onClick={() => console.log('closing')}>Cancel</button>
        </div>
      </StyledEditTagRow>
    </>
  );
};

export default AddTagRow;
