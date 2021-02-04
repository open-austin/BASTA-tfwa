import React from 'react'
import {RouteComponentProps} from 'react-router-dom';

// Page needs name, phone #(s), Property, Unit #, address, caht log, image filters (date range, image type?), image gallery

type TParams = {
  id: string;
}

const TenantDetails: React.FC<RouteComponentProps<TParams>> = ({match}) => {
  return (
    <div>
      <h4>Tenant Details - Tenant ID: {match.params.id} </h4>
    </div>
  )
}

export default TenantDetails;