import React from 'react';
import PhoneTable from './phone-table';
import { Container } from 'reactstrap';
import NameSearch from './name-search';

const Dashboard: React.FC = () => (
  <Container>
    <NameSearch />
    <PhoneTable />
  </Container>
);

export default Dashboard;
