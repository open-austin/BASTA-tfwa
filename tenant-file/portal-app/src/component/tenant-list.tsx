import React, { useEffect, useState } from "react";
import { useQuery, gql } from "@apollo/client";
import { TenantListQuery } from "./__generated__/TenantListQuery";
import { Table } from "reactstrap";
import { useTable, Column } from "react-table";
import axios from "axios";
import { getToken } from "./firebase";
import Image from "./image";

const EXCHANGE_RATES = gql`
  query TenantListQuery {
    tenants(order_by: { name: ASC }) {
      nodes {
        name
        tenantPhones {
          phone {
            phoneNumber
            images {
              thumbnailName
              name
            }
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

  const [, setUserToken] = useState("");

  useEffect(() => {
    const func = async () => {
      const token = await getToken();
      if (token) {
        setUserToken(token);
      }
    };
    func();
  }, []);

  const rowData =
    data?.tenants?.nodes?.reduce((acc, curr) => {
      if (curr?.name && curr?.tenantPhones[0].phone.phoneNumber) {
        acc.push({
          name: curr.name,
          phone: curr.tenantPhones[0].phone.phoneNumber,
          images: curr.tenantPhones[0].phone.images.map((x) => x.thumbnailName),
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
                          {console.log(cell)}
                          {
                            // Render the cell contents
                            cell.column.Header === "Images"
                              ? cell.value.map((i: string) => (
                                  <Image name={i} />
                                ))
                              : cell.render("Cell")
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
