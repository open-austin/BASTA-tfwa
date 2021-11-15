import React, { useState } from "react";
import { useLocation } from "react-router-dom";
import { useQuery, gql } from "@apollo/client";
import {
  PhonesFilteredByName,
  PhonesFilteredByName_phones_edges_node_images,
  PhonesFilteredByName_phones_edges_node_images_edges_node_labels,
} from "../types/PhonesFilteredByName";
import Image from "./image";
import { Table } from "reactstrap";
import { useTable, Column } from "react-table";
import PhoneTableCollapse from "./phone-table-collapse";

import { useHistory } from "react-router-dom";
import styles from "./phone-table.module.css";
const PHONES_BY_NAME_FILTER = gql`
  query PhonesFilteredByName(
    $name: String = ""
    $limit: Int = 10
    $cursor: String = null
  ) {
    phones(
      first: $limit
      after: $cursor
      where: {
        or: [
          { tenants: { any: false } }
          { tenants: { some: { name: { contains: $name } } } }
        ]
      }

      order: { phoneNumber: ASC }
    ) {
      pageInfo {
        endCursor
        hasNextPage
        hasPreviousPage
        startCursor
      }
      edges {
        node {
          phoneNumber
          id
          images {
            edges {
              node {
                id
                name
                thumbnailName
                labels {
                  label
                  confidence
                  source
                }
              }
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
  images: PhonesFilteredByName_phones_edges_node_images | null;
  property: string;
  // labels: (PhonesFilteredByName_phones_edges_node_images_edges_node_labels[] | undefined)[] | undefined;

  actionFunc: JSX.Element;
};

const PhoneTable: React.FC = () => {
  const history = useHistory();
  const onViewClick = (tenantId: string) => {
    console.log(`tenantID from path in phone-list ${tenantId}`);
    history.push({
      pathname: `/dashboard/tenant/${tenantId}`,
      state: { [tenantId]: tenantId },
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
    limit: 10,
    cursor: null,
  };
  // const { loading, error, data, refetch } = useQuery<PhonesFilteredByName>(

  // const { loading, error, fetchMore, data } =
  const { loading, error, data, fetchMore } = useQuery<PhonesFilteredByName>(
    PHONES_BY_NAME_FILTER,
    {
      notifyOnNetworkStatusChange: true,
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
          images: edge?.node?.images,
          //labels indexed for every image by image id
          // labels: edge?.node?.images?.edges?.map((i) =>
          //   i?.node?.labels?.filter((l) => !l?.source.includes("SafeSearch"))),

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

  // const tableInstance = useTable<PhoneRow>({
  //   columns,
  //   data: rowData,
  // });
  const handleScroll = (e: any) => {
    console.log(`HandleScroll`);
    //this needs to "add on" to the table and now rerender it. An independent component that addes new rows?
    const bottom =
      e.target.scrollHeight - e.target.scrollTop === e.target.clientHeight;
    const lastItem = data?.phones?.pageInfo?.endCursor;
    const hasMoreItems = data?.phones?.pageInfo?.hasNextPage;
    if (bottom && hasMoreItems) {
      console.log(`bottom hit`);
      fetchMore({
        variables: {
          name: nameQuery,
          limit: 5,
          cursor: lastItem,
        },
        updateQuery: (previous, { fetchMoreResult }) => {
          if (fetchMoreResult!.phones!.edges!.length > 0) {
            fetchMoreResult!.phones!.edges = [
              ...(previous.phones?.edges ?? []),
              ...(fetchMoreResult?.phones?.edges ?? []),
            ];
            return fetchMoreResult!;
          } else {
            return previous!;
          }
        },
      });
    }
  };
  // const [searchTerm, setSearchTerm] = useState("");

  const renderRows = () => {
    return rowData.map((phoneRow) => (
      <>
        <tr>
          <td>{phoneRow.name}</td>
          <td>{phoneRow.phone}</td>
          <td>{phoneRow.property}</td>
          <td>
            {phoneRow?.images?.edges?.[0] === undefined ? (
              <></>
            ) : (
              <Image
                imageName={
                  phoneRow?.images?.edges?.[0].node?.thumbnailName ?? ""
                }
                id={
                  phoneRow?.images?.edges?.[0].node?.id === undefined
                    ? "image"
                    : atob(phoneRow!.images!.edges?.[0].node!.id).replace(
                        "\n",
                        ""
                      )
                }
                tenantName={phoneRow.name}
                phoneNumber={phoneRow.phone}
                labels={
                  phoneRow?.images?.edges?.[0].node?.labels
                    ?.filter((l) => !l?.source.includes("SafeSearch"))
                    .map((l) => l.label) ?? [""]
                }
              />
            )}
          </td>
          <td>{phoneRow.actionFunc}</td>
        </tr>
      </>
    ));
  };
  // const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
  //   tableInstance;
  // if (loading) return <p>Loading...</p>;
  if (error) return <p>Error :(</p>;
  return (
    <div className="container-fluid">
      <div className="card shadow">
        <div className={styles.cardHeader}>
          <div className="d-flex">
            <h4 className="m-0 font-weight-bold mr-3">Images</h4>
          </div>
        </div>
        <div className="card-body">
          <div className="row">
            <div className="col-md-6 text-nowrap">
              <div
                id="dataTable_length"
                className="dataTables_length"
                aria-controls="dataTable"
              >
                {/* <label>
                    Show&nbsp;
                    <select className="form-control form-control-sm custom-select custom-select-sm">
                      <option value={10} selected>
                        10
                      </option>
                      <option value={25}>25</option>
                      <option value={50}>50</option>
                      <option value={100}>100</option>
                    </select>
                  </label> */}
              </div>
            </div>
            <div className="col-md-6"></div>
          </div>
          <div
            onScroll={handleScroll}
            className={styles.scrollFeed}
            id="dataTable"
            role="grid"
            aria-describedby="dataTable_info"
          >
            <table className="table my-0" id="dataTable">
              <thead>
                <tr>
                  <th>Tenant Name</th>
                  <th>Number</th>
                  <th>Property</th>
                  <th>Image</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>{renderRows()}</tbody>
              <tfoot>
                <tr>
                  <td>
                    <strong>Tenant Name</strong>
                  </td>
                  <td>
                    <strong>Number</strong>
                  </td>
                  <td>
                    <strong>Property</strong>
                  </td>
                  <td>
                    <strong>Image</strong>
                  </td>
                  <td>
                    <strong>Action</strong>
                  </td>
                </tr>
              </tfoot>
            </table>
          </div>
          {/* <div className="row">
              <div className="col-md-6 align-self-center">
                <p
                  id="dataTable_info"
                  className="dataTables_info"
                  role="status"
                  aria-live="polite"
                ></p>
              </div>
              <div className="col-md-6">
                <nav className="d-lg-flex justify-content-lg-end dataTables_paginate paging_simple_numbers">
                  <ul className="pagination">
                    <li className="page-item disabled">
                      <a className="page-link" href="#" aria-label="Previous">
                        <span aria-hidden="true">«</span>
                      </a>
                    </li>
                    <li className="page-item active">
                      <a className="page-link" href="#">
                        1
                      </a>
                    </li>
                    <li className="page-item">
                      <a className="page-link" href="#">
                        2
                      </a>
                    </li>
                    <li className="page-item">
                      <a className="page-link" href="#">
                        3
                      </a>
                    </li>
                    <li className="page-item">
                      <a className="page-link" href="#" aria-label="Next">
                        <span aria-hidden="true">»</span>
                      </a>
                    </li>
                  </ul>
                </nav>
              </div>
            </div> */}
        </div>
      </div>
    </div>
  );
  // return (
  //   <div className={styles.scrollFeed} onScroll={handleScroll}>
  //     <button
  //       onClick={() => {
  //         const lastItem = data?.phones?.pageInfo?.endCursor;
  //         fetchMore({
  //           variables: { cursor: lastItem },
  //           updateQuery: (previous, { fetchMoreResult }) => {
  //             fetchMoreResult!.phones!.edges = [
  //               ...(previous.phones?.edges ?? []),
  //               ...(fetchMoreResult?.phones?.edges ?? []),
  //             ];
  //             return fetchMoreResult!;
  //           },
  //         });
  //       }}
  //     >
  //       Next
  //     </button>
  //     <Table hover {...getTableProps()}>
  //       <thead>
  //         {headerGroups.map((headerGroup) => (
  //           <tr {...headerGroup.getHeaderGroupProps()}>
  //             {headerGroup.headers.map((column) => (
  //               <th {...column.getHeaderProps()}>{column.render("Header")}</th>
  //             ))}
  //           </tr>
  //         ))}
  //       </thead>
  //       <tbody {...getTableBodyProps()}>
  //         {rows.map((row) => {
  //           prepareRow(row);
  //           return <PhoneTableCollapse row={row} key={row.original.tenantId} />;
  //         })}
  //       </tbody>
  //     </Table>
  //   </div>
  // );
};

export default PhoneTable;
