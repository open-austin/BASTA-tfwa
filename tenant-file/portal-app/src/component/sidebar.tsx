import React from 'react';
import StyledSideBar from './styles/SidebarStyles';
import AppLogoAndTitle from './app-logo-and-title';

type Props = {
  isSidebarOpen: boolean;
  setIsSidebarOpen: (active: boolean) => void;
};

const SideBar: React.FC<Props> = ({
  isSidebarOpen,
  setIsSidebarOpen,
  children,
}) => {
  const closeSideBar = () => {
    setIsSidebarOpen(false);
  };

  const handleOuterSidebarClick = (e: React.MouseEvent) => {
    if (e.target === e.currentTarget) {
      closeSideBar();
    }
  };

  return (
    <>
      <StyledSideBar>
        {/* Dark overlay added to entire page */}
        <div
          className={`sidebar_outer ${isSidebarOpen ? '' : 'hide'}`}
          onClick={handleOuterSidebarClick}
        >
          <section className={`sidebar ${isSidebarOpen ? '' : 'hide'}`}>
            <div className="heading">
              <span className="close_button" onClick={closeSideBar}>
                &times;
              </span>
              <AppLogoAndTitle />
            </div>
            <ul className="links">{children}</ul>
          </section>
        </div>
      </StyledSideBar>
    </>
  );
};

export default SideBar;
