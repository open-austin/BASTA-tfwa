<<<<<<< HEAD
import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import { NavLink } from 'react-router-dom';
import { ThemeProvider } from 'styled-components';
import { RootState } from '../store/store';
import { SignedInStatus } from '../store/auth';
import Navigation from './nav';
import SideBar from './sidebar';
import ExportToolbar from './export-toolbar';
import theme from './styles/themes';

const Layout: React.FC = (props) => {
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);
  const [isExportToolsOpen, setIsExportToolsOpen] = useState(false);

  const signedInStatus = useSelector(
    (state: RootState) => state.auth.signedInStatus
  );

  const userEmail = useSelector((state: RootState) => state.auth.user.email);

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
        {isExportToolsOpen && (
          <ExportToolbar isExportToolsOpen={isExportToolsOpen} />
        )}
      </header>
      <main>{props.children}</main>
      <footer>
        Footer
        <p>
          {signedInStatus === SignedInStatus.LoggedIn
            ? `You are signed in as: ${userEmail}`
            : 'You need to sign in'}
        </p>
      </footer>
=======
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
>>>>>>> dc58887ad9606939ba2046bf7d77dd274992a39f
    </ThemeProvider>
  );
};

export default Layout;
