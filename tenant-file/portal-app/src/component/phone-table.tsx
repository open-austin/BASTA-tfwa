import React from "react";
import { useLocation } from "react-router-dom";
import { useQuery, gql } from "@apollo/client";
import { PhonesFilteredByName } from "../types/PhonesFilteredByName";
import { Table } from "reactstrap";
import { useTable, Column } from "react-table";
import PhoneTableCollapse from "./phone-table-collapse";
import { useHistory } from "react-router-dom";
import styles from "./tenant-details.module.css";

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
  {
    Header: "Action",
    accessor: "actionFunc",
  },
];

type ActionFunc = {
  name: string;
  func: Function;
};
type PhoneRow = {
  name: string;
  tenantId: string;
  phone: string;
  images: [string, string][];
  property: string;
  labels: [string, string[]][];
  actionFunc: JSX.Element;
};

const PhoneTable: React.FC = () => {
  const history = useHistory();
  const onViewClick = (tenantId: string) => {
    console.log(`tenantID from path in phone-list ${tenantId}`);
    history.push({
      pathname: `/dashboard/tenant/${tenantId}`,
      state: { [tenantId] : tenantId },
    });
  };

  const onRegisterTenantClick = (phone: string) => {
    history.push(`/add-tenant/${phone}`);
  };

  const paramsString = useLocation().search;
  const searchParams = new URLSearchParams(paramsString);
  const nameQuery = searchParams.get("q") || "";
  const queryVariables = {
    name: nameQuery,
  };

  // const { loading, error, data, refetch } = useQuery<PhonesFilteredByName>(
  const { loading, error, data } = useQuery<PhonesFilteredByName>(
    PHONES_BY_NAME_FILTER,
    {
      variables: queryVariables,
      fetchPolicy: "cache-and-network",
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
          // actionFunc: edge.node.tenants?.[0]?.id === "" ?
          //   { name: "Register", func: onRegisterTenantClick }
          //   :{name: "View", func: onViewClick}
          actionFunc: !edge.node.tenants?.[0]?.id ? (
            <button
              onClick={() =>
                onRegisterTenantClick(edge.node.phoneNumber.replace("+1", ""))
              }
              className="btn btn-info"
            >
              Register Tenant
            </button>
          ) : (
            <button
              className={styles.bastaBtn}
              onClick={() => onViewClick(edge.node.tenants?.[0]?.id ?? "")}
            >
              View
            </button>
          ),
        });
      }
      return acc;
    }, [] as PhoneRow[]) || [];

  const tableInstance = useTable<PhoneRow>({
    columns,
    data: rowData,
  });

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    tableInstance;
  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error :(</p>;

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
