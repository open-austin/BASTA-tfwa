import styled from 'styled-components';

const StyledExportToolbar = styled.div`
  &.hide {
    transform: translateY(-100%);
  }
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0.5rem 1rem;
  background-color: ${(props) => props.theme.backdrop};
  color: ${(props) => props.theme.primary};
  box-shadow: 0 1px 10px rgba(0, 0, 0, 0.3);
  z-index: 2;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 1.2rem;
  padding: 0.5rem 1rem;
  font-weight: 700;
  transition: transform 0.2s ease;
  transform: translateY(0);

  span {
    flex: 1;
    text-align: center;
    transition: color 0.4s ease;

    &:hover {
      color: ${({ theme }) => theme.accent};
      cursor: pointer;
    }
  }
`;

export default StyledExportToolbar;
