import React from "react";
import TenantList from "./tenant-list";
import { Container } from "reactstrap";

const Dashboard: React.FC = () => (
  <Container>
    <TenantList />
  </Container>
);

export default Dashboard;
