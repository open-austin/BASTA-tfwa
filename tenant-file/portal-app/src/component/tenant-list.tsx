import React, { useEffect, useState } from "react";
import { useQuery, gql } from "@apollo/client";
import { TenantListQuery } from "./__generated__/TenantListQuery";
import { Table } from "reactstrap";
import { useTable, Column } from "react-table";
import {useHistory} from 'react-router-dom';
import axios from "axios";
import { getToken } from "./firebase";
import Image from "./image";

const TENANT_INFO = gql`
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
  let history = useHistory();
  console.log(process.env.REACT_APP_API_URL);
  const { loading, error, data } = useQuery<TenantListQuery>(TENANT_INFO);

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

  // (RG) will be replaced by actual userId as dataset in each row
  const mockUserIdNumber = '1234';
  const onHandleRowClick = (event: React.MouseEvent) => {

    if (event.target instanceof Element) {
      console.log('clicked', event.target.parentElement?.dataset.userId);
      const userId = event.target.parentElement?.dataset.userId;
      history.push(`/dashboard/tenant/${userId}`);
    }
  }

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
          return (
            <>
              <tr {...row.getRowProps()} onClick={onHandleRowClick} data-user-id={mockUserIdNumber}>
                {row.cells.map((cell) => {
                  return (
                    <td {...cell.getCellProps()}>
                      {console.log(cell)}
                      {cell.column.Header === "Images"
                        ? cell.value.map((i: string) => <Image name={i} />)
                        : cell.render("Cell")}
                    </td>
                  );
                })}
              </tr>

              {/* <tr>
                  <td colSpan={2} className="text-center">
                    <Collapse>I'm Spanning Yo</Collapse>
                  </td>
                </tr> */}
            </>
          );
        })}
      </tbody>
    </Table>
  );
};

export default TenantList;
