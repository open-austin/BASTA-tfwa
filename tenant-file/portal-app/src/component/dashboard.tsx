import React from "react";
import PhoneTable from "./phone-table";
import { Container } from "reactstrap";
import NameSearch from "./name-search";

const Dashboard: React.FC = () => (
  <div className="container-fluid">
    <div className="row  justify-content-center">
      <div className=" col-9">
        <div className="card shadow py-2 px-4">
          <NameSearch />
          <PhoneTable />
        </div>
      </div>
    </div>
  </div>
);

export default Dashboard;
