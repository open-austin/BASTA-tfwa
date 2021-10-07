import React from "react";
// import { useSelector } from "react-redux";
import { NavLink } from "react-router-dom";
import StyledFooter from "./styles/FooterStyles";
// import { RootState } from "../store/store";

const Footer: React.FC = () => {
  // const profile = useSelector((state: RootState) => state.firebase.profile);

  return (
    <>
      <StyledFooter>
        <NavLink to="#" tabIndex={0}>
          <i className="las la-file-alt"></i>
          <span>Create Account</span>
        </NavLink>
        <NavLink to="/" tabIndex={0}>
          <i className="las la-home"></i>
          <span>Home</span>
        </NavLink>
        <NavLink to="#" tabIndex={0}>
          <i className="las la-cog"></i>
          <span>Settings</span>
        </NavLink>
      </StyledFooter>
    </>
  );
};

export default Footer;
