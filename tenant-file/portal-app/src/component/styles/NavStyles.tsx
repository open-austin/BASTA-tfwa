import styled from "styled-components";

const StyledNav = styled.nav`
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0.5rem 1rem;
  background-color: ${(props) => props.theme.backdrop};
  color: #fefad4;
  box-shadow: 0 1px 10px rgba(0, 0, 0, 0.3);
  position: relative;
  z-index: 3;
  top: 0;
  height: 50px;
  & > * {
    margin: 1rem 0.5rem;
  }

  a,
  button {
    color: ${(props) => props.theme.secondary};
    text-decoration: none;
    transition: color 0.4s ease;
    cursor: pointer;

    &:hover {
      color: ${(props) => props.theme.darkSecondary};
    }
  }

  .center {
    flex: 1;
    margin: 1rem 2rem;
    @media (max-width: 760px) {
      margin: 0;
    }
  }

  .links {
    flex: 1;
    list-style: none;
    font-size: 1.1em;
    margin: 0;
    padding: 0;
    @media (max-width: 760px) {
      display: none;
    }
    display: flex;

    & > li {
      padding-right: 1rem;
    }
  }

  .hamburger {
    display: none;
    color: ${(props) => props.theme.primary};
    font-size: 1.3rem;

    &:hover {
      cursor: pointer;
    }

    @media (max-width: 760px) {
      display: block;
    }
  }

  button {
    border: none;
    background-color: rgba(0, 0, 0, 0);
    font-size: inherit;
    font-weight: inherit;
    padding: 0;
  }
`;

export default StyledNav;
