import React, { MouseEvent } from 'react';
import { NavLink } from 'react-router-dom';
import StyledExportToolbar from './styles/ExportToolbarStyles';

// type Props = {
//   setIsSidebarOpen: (active: boolean) => void;
//   renderLinks: () => React.ReactNode;
// };

const ExportToolbar = () => {
  // Links to be displayed in main nav or mobile sidebar based on screen size

  // const openSideBar = (e: React.MouseEvent) => {
  //   setIsSidebarOpen(true);
  // };

  return (
    <StyledExportToolbar>
      Exporting your stuff!
      <span>&#8594; CSV</span>
      <span>&#8594; ZIP</span>
      <span>&#8594; MAP</span>
    </StyledExportToolbar>
  );
};

export default ExportToolbar;
