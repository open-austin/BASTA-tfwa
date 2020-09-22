import styled from 'styled-components';

const StyledFooter = styled.footer`
  background-color: ${(props) => props.theme.backdrop};
  color: #fefad4;
  width: 100vw;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 1.5rem;
  padding: 0.5rem 1rem;

  a {
    flex: 1;
    text-align: center;
    text-decoration: none;
    color: ${(props) => props.theme.primary};
    display: flex;
    flex-direction: column;

    span {
      font-size: 0.5em;
    }

    &:hover {
      color: ${(props) => props.theme.accent};
    }
  }
`;

export default StyledFooter;
