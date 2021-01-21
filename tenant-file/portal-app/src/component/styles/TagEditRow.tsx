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
    transform: translate(-5px, 80px);
    z-index: 4;
  }
`;

export default StyledEditTagRow;
