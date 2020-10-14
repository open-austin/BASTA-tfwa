import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import styled, { ThemeProvider } from "styled-components";
import Footer from "./footer";
import Navigation from "./nav";
import SideBar from "./sidebar";
import theme from "./styles/themes";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";

const PageLayout = styled.div`
  min-height: 100%;
  display: grid;
  grid-template-rows: auto 1fr auto;
`;

const Links: React.FC = () => {
  const isLoaded = useSelector(
    (state: RootState) => state.firebase.profile.isLoaded
  );
  const claims = useSelector(
    (state: RootState) => state.firebase.profile?.token?.claims
  );

  return (
    <>
      <li>
        {isLoaded && (claims?.organizer || claims?.admin) && (
          <NavLink exact to="/dashboard" activeClassName="active">
            Dashboard
          </NavLink>
        )}
      </li>
      <li>
        {isLoaded && claims?.admin && (
          <NavLink exact to="/admin" activeClassName="active">
            Admin
          </NavLink>
        )}
      </li>
    </>
  );
};

const Layout: React.FC = (props) => {
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

  // For dual display in sidebar and main nav

  return (
    <ThemeProvider theme={theme}>
      <PageLayout>
        <header className="App-header">
          <SideBar
            isSidebarOpen={isSidebarOpen}
            setIsSidebarOpen={setIsSidebarOpen}
          >
            <Links />
          </SideBar>
          <Navigation setIsSidebarOpen={setIsSidebarOpen}>
            <Links />
          </Navigation>
        </header>
        <main>{props.children}</main>
        <Footer />
      </PageLayout>
    </ThemeProvider>
  );
};

export default Layout;
