import React from 'react';
import { NavLink } from 'react-router-dom';
import StyledFooter from './styles/FooterStyles';
const Footer: React.FC = () => {
  // Links to be displayed in main nav or mobile sidebar based on screen size

  return (
    <StyledFooter>
      <NavLink to="#" tabIndex={0}>
        <i className="las la-file-alt"></i>
        <span>Create Account</span>
      </NavLink>
      <NavLink to="#" tabIndex={0}>
        <i className="las la-home"></i>
        <span>Home</span>
      </NavLink>
      <NavLink to="#" tabIndex={0}>
        <i className="las la-cog"></i>
        <span>Settings</span>
      </NavLink>
    </StyledFooter>
  );
};

export default Footer;
