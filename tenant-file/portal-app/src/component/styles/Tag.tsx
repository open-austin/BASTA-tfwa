import styled from 'styled-components';
import { isDark } from '../../utility';

const StyledTag = styled.button`
  display: inline-block;
  padding: 0.1rem 0.5rem;
  background-color: ${(props) => props.color};
  border-radius: 15px;
  font-weight: 700;
  color: ${(props) => {
    return isDark(props.color!) ? 'white' : 'black';
  }};
  box-shadow: none;
  border: none;
`;

export default StyledTag;
