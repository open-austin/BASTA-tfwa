import React, { useEffect } from "react";
import { useLocation } from "react-router-dom";
import { useQuery, gql } from "@apollo/client";
import { TenantListQuery } from "./__generated__/TenantListQuery";
import { Table } from "reactstrap";
import { useTable, Column } from "react-table";
import axios from "axios";
import { getToken } from "./firebase";
import TenantTableCollapse from "./tenant-table-collapse";

const TENANT_QUERY = gql`
  query TenantListQuery($name: String = "") {
    tenants(order: { name: ASC }, where: { name: { contains: $name } }) {
      nodes {
        name
        phones {
          phoneNumber
          images {
            thumbnailName
            name
          }
        }
      }
    }
  }
`;

const columns: Column<TenantRow>[] = [
  {
    Header: "Name",
    accessor: "name", // accessor is the "key" in the data
  },
  {
    Header: "Phone Number",
    accessor: "phone",
  },
  {
    Header: "Images",
    accessor: "images",
  },
];

type TenantRow = {
  name: string;
  phone: string;
  images: string[];
};

const TenantList: React.FC = () => {
  const paramsString = useLocation().search;
  const searchParams = new URLSearchParams(paramsString);
  const nameQuery = searchParams.get("q") || "";
  console.log(searchParams.get("q"), "location");

  const queryVariables = {
    name: nameQuery,
  };

  console.log(process.env.REACT_APP_API_URL);
  const { loading, error, data } = useQuery<TenantListQuery>(TENANT_QUERY, {
    variables: queryVariables,
  });
  console.log("ROWDATA", loading, error, data);
  const rowData =
    data?.tenants?.nodes?.reduce((acc, node) => {
      if (node?.name && node?.phones[0].phoneNumber) {
        acc.push({
          name: node.name,
          phone: node.phones[0].phoneNumber,
          images:
            node.phones[0].images
              ?.filter((x) => x)
              .map((x) => x!.thumbnailName) ?? [],
        });
      }
      return acc;
    }, [] as TenantRow[]) || [];

  useEffect(() => {
    const func = async () => {
      const token = await getToken();
      const baseUrl =
        process.env.NODE_ENV === "production"
          ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
          : "http://localhost:8080";

      rowData.map((x) =>
        x.images.map(async (y) => {
          const imageResponse = await axios
            .get(`${baseUrl}/api/image?name=${y}`, {
              headers: {
                Authorization: `Bearer ${token}`,
              },
            })
            .then((x) => x);
          console.log("RESPONSE", imageResponse);
        })
      );
    };
    func();
  }, [rowData]);

  const tableInstance = useTable<TenantRow>({
    columns,
    data: rowData,
  });

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error :(</p>;

  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    rows,
    prepareRow,
  } = tableInstance;

  return (
    <Table hover {...getTableProps()}>
      <thead>
        {headerGroups.map((headerGroup) => (
          <tr {...headerGroup.getHeaderGroupProps()}>
            {headerGroup.headers.map((column) => (
              <th {...column.getHeaderProps()}>{column.render("Header")}</th>
            ))}
          </tr>
        ))}
      </thead>
      <tbody {...getTableBodyProps()}>
        {rows.map((row) => {
          prepareRow(row);
          return <TenantTableCollapse row={row} />;
        })}
      </tbody>
    </Table>
  );
};

export default TenantList;
