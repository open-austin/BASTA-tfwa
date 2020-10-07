import React from 'react';
import StyledExportToolbar from './styles/ExportToolbarStyles';

type Props = {
  isExportToolsOpen: Boolean;
};

const ExportToolbar = ({ isExportToolsOpen }: Props) => {
  return (
    <StyledExportToolbar className={isExportToolsOpen ? '' : 'hide'}>
      <span tabIndex={0}>
        <i className="fas fa-file-csv"></i> &#8594; CSV
      </span>
      <span tabIndex={0}>
        <i className="far fa-file-archive"></i> &#8594; ZIP
      </span>
      <span tabIndex={0}>
        <i className="fas fa-table"></i> &#8594; MAP
      </span>
    </StyledExportToolbar>
  );
};

export default ExportToolbar;
