import React from 'react';
import TenantList from './tenant-list';
import { Container } from 'reactstrap';
import NameSearch from './name-search';

const Dashboard: React.FC = () => (
  <Container>
    <NameSearch />
    <TenantList />
  </Container>
);

export default Dashboard;
