import React from "react";
import { useQuery, gql } from "@apollo/client";
import { TenantListQuery } from "./__generated__/TenantListQuery";
import { Row, Col, Table, Collapse } from "reactstrap";
import { useTable, Column } from "react-table";

const EXCHANGE_RATES = gql`
  query TenantListQuery {
    tenants(order_by: { name: ASC }) {
      nodes {
        name
        images {
          thumbnailName
        }
        tenantPhones {
          phone {
            phoneNumber
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
  console.log(process.env.REACT_APP_API_URL);
  const { loading, error, data } = useQuery<TenantListQuery>(EXCHANGE_RATES);

  const rowData =
    data?.tenants?.nodes?.reduce((acc, curr) => {
      if (curr?.name && curr?.tenantPhones[0].phone.phoneNumber) {
        acc.push({
          name: curr.name,
          phone: curr.tenantPhones[0].phone.phoneNumber,
          images: curr.images.map((x) => x.thumbnailName),
        });
      }
      return acc;
    }, [] as TenantRow[]) || [];

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
    <Table {...getTableProps()}>
      <thead>
        {headerGroups.map((headerGroup) => (
          <tr {...headerGroup.getHeaderGroupProps()}>
            {
              // Loop over the headers in each row
              headerGroup.headers.map((column) => (
                // Apply the header cell props
                <th {...column.getHeaderProps()}>
                  {
                    // Render the header
                    column.render("Header")
                  }
                </th>
              ))
            }
          </tr>
        ))}
      </thead>
      <tbody {...getTableBodyProps()}>
        {
          // Loop over the table rows
          rows.map((row) => {
            // Prepare the row for display
            prepareRow(row);
            return (
              // Apply the row props
              <>
                <tr {...row.getRowProps()}>
                  {
                    // Loop over the rows cells
                    row.cells.map((cell) => {
                      // Apply the cell props
                      return (
                        <td {...cell.getCellProps()}>
                          {
                            // Render the cell contents
                            cell.render("Cell")
                          }
                        </td>
                      );
                    })
                  }
                </tr>

                {/* <tr>
                  <td colSpan={2} className="text-center">
                    <Collapse>I'm Spanning Yo</Collapse>
                  </td>
                </tr> */}
              </>
            );
          })
        }
      </tbody>
    </Table>
  );
};

export default TenantList;
