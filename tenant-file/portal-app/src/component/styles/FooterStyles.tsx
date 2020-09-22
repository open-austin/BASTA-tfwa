import styled from 'styled-components';

const StyledFooter = styled.footer`
  background-color: ${(props) => props.theme.backdrop};
  color: #fefad4;
  position: sticky;
  bottom: 0;
`;

export default StyledFooter;
