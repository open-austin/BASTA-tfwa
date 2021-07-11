import React from "react";
import { useLocation } from "react-router-dom";
import { useQuery, gql } from "@apollo/client";
import { PhonesFilteredByName } from "../types/PhonesFilteredByName";
import { Table } from "reactstrap";
import { useTable, Column } from "react-table";
import PhoneTableCollapse from "./phone-table-collapse";

//TODO: THIS IS NOT FETCHING PHONES W/O TENANTS. Consider filtering on client
const PHONES_BY_NAME_FILTER = gql`
  query PhonesFilteredByName($name: String = "") {
    phones(
      where: {
        or: [
          { tenants: { any: false } }
          { tenants: { some: { name: { contains: $name } } } }
        ]
      }
      order: { phoneNumber: ASC }
    ) {
      edges {
        node {
          phoneNumber
          id
          images {
            id
            name
            thumbnailName
            labels {
              label
              confidence
              source
            }
          }
          tenants {
            id
            name
            residence {
              property {
                name
              }
            }
          }
        }
      }
    }
  }
`;

const columns: Column<PhoneRow>[] = [
  {
    Header: "Name",
    accessor: "name",
  },
  {
    Header: "Property Name",
    accessor: "property",
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

type PhoneRow = {
  name: string;
  tenantId: string;
  phone: string;
  images: [string, string][];
  property: string;
  labels: [string, string[]][];
};

const PhoneTable: React.FC = () => {
  const paramsString = useLocation().search;
  const searchParams = new URLSearchParams(paramsString);
  const nameQuery = searchParams.get("q") || "";
  const queryVariables = {
    name: nameQuery,
  };

  const { loading, error, data } = useQuery<PhonesFilteredByName>(
    PHONES_BY_NAME_FILTER,
    {
      variables: queryVariables,
    }
  );

  const rowData =
    data?.phones?.edges?.reduce((acc, edge) => {
      if (edge?.node.phoneNumber) {
        acc.push({
          name: edge.node.tenants?.[0]?.name
            ? edge.node.tenants?.[0]?.name
            : "",
          tenantId: edge.node.tenants?.[0]?.id
            ? edge.node.tenants?.[0]?.id
            : "",
          property: edge.node.tenants?.[0]?.residence?.property?.name
            ? edge.node.tenants?.[0]?.residence?.property?.name
            : "",
          phone: edge.node.phoneNumber,
          images: edge.node.images?.map(
            (x) => [x?.id, x?.thumbnailName] ?? ["", ""]
          ) as [string, string][],
          //labels indexed for every image by image id
          labels: edge?.node.images?.map((i) => [
            i?.id as string,
            i?.labels
              ?.filter((l) => !l.source.includes("SafeSearch"))
              .map(
                (l) => `${l.label} - ${Math.round((l.confidence ?? 0) * 100)}%`
              ) as string[],
          ]) as [string, string[]][],
        });
      }
      return acc;
    }, [] as PhoneRow[]) || [];

  const tableInstance = useTable<PhoneRow>({
    columns,
    data: rowData,
  });

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error :(</p>;

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;

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
          return <PhoneTableCollapse row={row} key={row.original.tenantId} />;
        })}
      </tbody>
    </Table>
  );
};

export default PhoneTable;
