import React from 'react';
import StyledExportToolbar from './styles/ExportToolbarStyles';

type Props = {
  isExportToolsOpen: Boolean;
};

const ExportToolbar = ({ isExportToolsOpen }: Props) => {
  return (
    <StyledExportToolbar className={isExportToolsOpen ? '' : 'hide'}>
      <button tabIndex={0}>
        <i className="fas fa-file-csv"></i> &#8594; CSV
      </button>
      <button tabIndex={0}>
        <i className="far fa-file-archive"></i> &#8594; ZIP
      </button>
      <button tabIndex={0}>
        <i className="fas fa-table"></i> &#8594; MAP
      </button>
    </StyledExportToolbar>
  );
};

export default ExportToolbar;
