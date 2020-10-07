import styled from 'styled-components';

const StyledSideBar = styled.div`
  .sidebar_outer {
    background-color: rgba(0, 0, 0, 0.2);
    width: 100vw;
    height: 100vh;
    position: absolute;
    top: 0;
    left: 0;
    z-index: 10;
    transition: background-color 0.2s ease;

    &.hide {
      background-color: rgba(0, 0, 0, 0);
      pointer-events: none;
    }
  }

  section.sidebar {
    position: absolute;
    left: 0;
    top: 0;
    height: 100vh;
    padding: 0.5rem 0;
    width: 16rem;
    z-index: 4;
    background-color: ${({ theme }) => theme.backdrop};
    box-shadow: 1px 0 10px rgba(0, 0, 0, 0.3);
    transform: translateX(0);
    transition: transform 0.2s ease;

    &.hide {
      transform: translateX(-100%);
    }
  }

  .heading {
    display: flex;
    align-items: center;
    border-bottom: 1px solid ${({ theme }) => theme.secondary};
    padding: 1rem 1rem;
    padding-bottom: 1.5rem;
  }

  .close_button {
    display: inline-block;
    font-size: 2rem;
    font-weight: 100;
    margin-right: 0.5rem;
    border-radius: 50%;
    height: 34px;
    width: 34px;
    display: flex;
    justify-content: center;
    align-items: center;
    /* background-color: rgba(130, 216, 216, 0); */
    transition: color 0.2s ease;
    color: ${({ theme }) => theme.primary};
    font-weight: 700;

    &:hover {
      cursor: pointer;
      color: rgb(130, 216, 216);
    }
  }

  .links {
    list-style: none;
    margin: 0;
    padding: 0;
    font-size: 0.8rem;
    font-weight: 700;

    li {
      display: flex;
      align-items: center;
      padding: 1rem 1.5rem;
      border-bottom: 1px solid ${({ theme }) => theme.secondary};
    }
    a {
      text-decoration: none;
      color: ${({ theme }) => theme.secondary};
      cursor: pointer;
    }
  }
`;

export default StyledSideBar;
