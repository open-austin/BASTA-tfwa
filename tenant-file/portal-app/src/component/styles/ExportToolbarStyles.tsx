import styled from 'styled-components';

const StyledExportToolbar = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0.5rem 1rem;
  background-color: ${(props) => props.theme.accent};
  color: #fefad4;
  box-shadow: 0 1px 10px rgba(0, 0, 0, 0.3);
  z-index: 2;
`;

export default StyledExportToolbar;
