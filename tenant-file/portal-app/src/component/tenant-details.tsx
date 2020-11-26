import React from 'react'
import {RouteComponentProps} from 'react-router-dom';

type TParams = {
  id: string;
}

const TenantDetails = ({ match }: RouteComponentProps<TParams>) => {
  return (
    <div>
      <h4>Tenant Details - Mock ID from dashboard: {match.params.id} </h4>
    </div>
  )
}

export default TenantDetails;