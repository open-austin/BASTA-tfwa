import React from "react";
import { Navbar, NavbarBrand } from "reactstrap";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { SignedInStatus } from "../store/auth";

const Layout: React.FC = (props) => {
  const signedInStatus = useSelector(
    (state: RootState) => state.auth.signedInStatus
  );
  const userEmail = useSelector((state: RootState) => state.auth.user.email);

  return (
    <>
      <header className="App-header">
        <Navbar color="light" light expand="md">
          <NavbarBrand>TFWA</NavbarBrand>
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
