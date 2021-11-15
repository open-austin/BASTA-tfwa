import React from "react";
import PhoneTable from "./phone-table";
import NameSearch from "./name-search";

const Dashboard: React.FC = () => (
  <div className="container-fluid">
    <div className="row  justify-content-center">
      <div className="mt-5 col-10">
        <div className="card shadow py-2 px-4">
          <NameSearch />
          <PhoneTable />
        </div>
      </div>
    </div>
  </div>

);

export default Dashboard;
