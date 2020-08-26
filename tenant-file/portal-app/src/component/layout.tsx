import React, { useState } from "react";
import {
  Navbar,
  NavbarBrand,
  NavItem,
  NavbarToggler,
  Collapse,
  Nav,
} from "reactstrap";
import { useSelector } from "react-redux";
import { NavLink } from "react-router-dom";
import { RootState } from "../store/store";
import { SignedInStatus } from "../store/auth";

const Layout: React.FC = (props) => {
  const [isOpen, setIsOpen] = useState(false);

  const signedInStatus = useSelector(
    (state: RootState) => state.auth.signedInStatus
  );
  const userEmail = useSelector((state: RootState) => state.auth.user.email);
  const toggle = () => setIsOpen(!isOpen);
  return (
    <>
      <header className="App-header">
        <Navbar color="light" light expand="md">
          <NavbarBrand>TFWA</NavbarBrand>
          <NavbarToggler onClick={toggle} />
          <Collapse isOpen={isOpen} navbar>
            <Nav className="mr-auto" navbar>
              {signedInStatus === SignedInStatus.LoggedIn && (
                <NavItem>
                  <NavLink
                    exact
                    to="/admin"
                    className="nav-link"
                    activeClassName="active"
                  >
                    Admin
                  </NavLink>
                </NavItem>
              )}
            </Nav>
          </Collapse>
          <NavItem className="navbar">
            <NavLink
              exact
              to="/login"
              className="nav-link"
              activeClassName="active"
            >
              Login
            </NavLink>
          </NavItem>
        </Navbar>
      </header>
      <main>{props.children}</main>
      <footer>
        Footer
        <p>
          {signedInStatus === SignedInStatus.LoggedIn
            ? `You are signed in as: ${userEmail}`
            : "You need to sign in"}
        </p>
      </footer>
    </>
  );
};

export default Layout;
