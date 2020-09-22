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
        <ExportToolbar />
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
    </ThemeProvider>
  );
};

export default Layout;
