<<<<<<< HEAD
import React, { MouseEvent } from "react";
import StyledNav from "./styles/NavStyles";
import AppLogoAndTitle from "./app-logo-and-title";
import BarsIcon from "./bars-svg";
import { NavLink } from "react-router-dom";
=======
import React, { MouseEvent } from 'react';
import StyledNav from './styles/NavStyles';
import AppLogoAndTitle from './app-logo-and-title';
import { ReactComponent as BarsIcon } from '../images/bars.svg';
import { NavLink } from 'react-router-dom';
>>>>>>> dc58887ad9606939ba2046bf7d77dd274992a39f

type Props = {
  setIsSidebarOpen: (active: boolean) => void;
  renderLinks: () => React.ReactNode;
};

const Nav = ({ setIsSidebarOpen, renderLinks }: Props) => {
  // Links to be displayed in main nav or mobile sidebar based on screen size

  const openSideBar = (e: React.MouseEvent) => {
    setIsSidebarOpen(true);
  };

  return (
    <StyledNav>
      <div className="hamburger">
        <BarsIcon onClick={openSideBar} />
      </div>
      <AppLogoAndTitle />
      <div className="center">
        <ul className="links">{renderLinks()}</ul>
      </div>
      <div className="user">
        <NavLink exact to="/login" activeClassName="active">
          Login
        </NavLink>
      </div>
    </StyledNav>
  );
};

export default Nav;
