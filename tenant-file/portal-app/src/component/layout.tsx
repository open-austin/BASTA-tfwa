import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import styled, { ThemeProvider } from "styled-components";
import Footer from "./footer";
import Navigation from "./nav";
import SideBar from "./sidebar";
import theme from "./styles/themes";

const PageLayout = styled.div`
  min-height: 100%;
  display: grid;
  grid-template-rows: auto 1fr auto;
`;

const Layout: React.FC = (props) => {
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

  // For dual display in sidebar and main nav
  const renderLinks = () => {
    return (
      <li>
        <NavLink exact to="/admin" activeClassName="active">
          Admin
        </NavLink>
      </li>
    );
  };

  return (
    <ThemeProvider theme={theme}>
      <PageLayout>
        <header className="App-header">
          <SideBar
            isSidebarOpen={isSidebarOpen}
            setIsSidebarOpen={setIsSidebarOpen}
            renderLinks={renderLinks}
          />
          <Navigation
            setIsSidebarOpen={setIsSidebarOpen}
            renderLinks={renderLinks}
          />
        </header>
        <main>{props.children}</main>
        <Footer />
      </PageLayout>
    </ThemeProvider>
  );
};

export default Layout;
