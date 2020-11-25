import React from 'react';
import TenantList from './tenant-list';
import TenantDetails from './tenant-details';
import { Container } from 'reactstrap';

const Dashboard: React.FC = () => (
  <Container>
    <TenantList />
    {/* <TenantDetails /> */}
  </Container>
);

export default Dashboard;
