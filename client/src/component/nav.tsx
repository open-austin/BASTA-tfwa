import React from 'react';
import StyledNav from './styles/NavStyles';
import AppLogoAndTitle from './app-logo-and-title';
import { ReactComponent as BarsIcon } from '../images/bars.svg';
import { NavLink } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { RootState } from '../store/store';
import { useFirebase } from 'react-redux-firebase';

type Props = {
  setIsSidebarOpen: (active: boolean) => void;
};

const Nav: React.FC<Props> = ({ setIsSidebarOpen, children }) => {
  // Links to be displayed in main nav or mobile sidebar based on screen size

  const profile = useSelector((state: RootState) => state.firebase.profile);
  const firebase = useFirebase();

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
        <ul className="links">{children}</ul>
      </div>
      <div className="user">
        {profile.isLoaded && !profile.isEmpty ? (
          <NavLink
            onClick={() => firebase.logout()}
            exact
            to="/"
            activeClassName="active"
          >
            Logout
          </NavLink>
        ) : (
          <NavLink exact to="/login" activeClassName="active">
            Login
          </NavLink>
        )}
      </div>
    </StyledNav>
  );
};

export default Nav;
