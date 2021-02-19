import React from 'react';
import StyledExportToolbar from './styles/ExportToolbarStyles';

type Props = {
  isExportToolsOpen: Boolean;
};

const ExportToolbar = ({ isExportToolsOpen }: Props) => {
  const handleClick = (e: React.MouseEvent) => {
    console.log('click');
  };

  return (
    <StyledExportToolbar className={isExportToolsOpen ? '' : 'hide'}>
      <button onClick={handleClick}>
        <i className="fas fa-file-csv"></i> &#8594; CSV
      </button>
      <button onClick={handleClick}>
        <i className="far fa-file-archive"></i> &#8594; ZIP
      </button>
      <button onClick={handleClick}>
        <i className="fas fa-table"></i> &#8594; MAP
      </button>
    </StyledExportToolbar>
  );
};

export default ExportToolbar;
