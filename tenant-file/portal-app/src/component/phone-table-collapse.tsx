import React, { useState } from "react";
import { Collapse } from "reactstrap";
import { Cell, Row } from "react-table";
//import { useHistory } from "react-router-dom";
import Image from "./image";
import styled from "styled-components";
import firebase from "firebase";
import "@firebase/storage";

const ImageGridStyles = styled.div`
  display: grid;
  gap: 0.5rem;
  width: auto;
  grid-template-columns: repeat(auto-fill, minmax(125px, 1fr));
  justify-content: center;

  & > * {
    place-self: center;
  }
`;
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
// function b64_to_utf8(str: string) {
//   return decodeURIComponent(escape(window.atob(str)));
// }
type Props = {
  row: Row<PhoneRow>;
};
const storage = firebase.app().storage();
const PhoneTableCollapse = ({ row }: Props) => {
  //let history = useHistory();
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);

  // const onViewClick = (tenantId: string) => {
  //   history.push(`/dashboard/tenant/${tenantId}`);
  // };

  // const onRegisterTenantClick = (phone: string) => {
  //   history.push(`/add-tenant/?phone=${phone}`);
  // };

  return (
    <>
      <tr {...row.getRowProps()} onClick={toggle}>
        {row.cells.map((cell: Cell<PhoneRow, any>, index: number) => {
          if (
            cell.column.Header === "Images" &&
            cell?.value?.[1]?.[0] !== undefined
          ) {
            console.log(
              ` cell.value?.[1]?.[0] ${atob(cell?.value?.[1]?.[0]).replace(
                "\n",
                ""
              )}`
            );
            console.log(`atob cell.value?.[1]?.[0] ${cell?.value?.[1]?.[0]}`);
          }
          return (
            <>
              <td {...cell.getCellProps()}>
                {cell.column.Header === "Images" ? (
                  <Image
                    storage={storage}
                    name={cell.value?.[1]?.[1]}
                    id={
                      cell.value?.[1]?.[0] == undefined
                        ? "image"
                        : atob(cell?.value?.[1]?.[0]).replace("\n", "") +
                          atob(row?.original?.tenantId).replace("\n", "")
                    }
                    labels={
                      cell.value
                        ? (row.original.labels
                            .filter(
                              (l) => l[0] === (cell.value?.[1]?.[0] as string)
                            )
                            .flatMap((l) => l[1]) as string[])
                        : ["", ""]
                    }
                  />
                ) : (
                  cell.render("Cell")
                )}
              </td>
            </>
          );
        })}
      </tr>
      <tr>
        <td
          colSpan={4}
          className="text-center"
          style={isOpen ? {} : { padding: 0 }}
        >
          <Collapse isOpen={isOpen}>
            {row.cells[3].value.length ? (
              <ImageGridStyles>
                {row.cells[3].value.map(
                  (i: [string, string], index: number) => {
                    return (
                      <>
                        <Image
                          storage={storage}
                          name={i[1]}
                          id={
                            i[1] == undefined
                              ? "image"
                              : `${
                                  atob(i[0]).replace("\n", "") +
                                  atob(row?.original?.tenantId).replace(
                                    "\n",
                                    ""
                                  )
                                }`
                          }
                          labels={
                            row.original.labels !== undefined
                              ? (row.original.labels
                                  .filter((l) => l[0] === (i[0] as string))
                                  .flatMap((l) => l[1]) as string[])
                              : ["", ""]
                          }
                        />
                      </>
                    );
                  }
                )}
              </ImageGridStyles>
            ) : (
              "No images to show."
            )}
          </Collapse>
        </td>
      </tr>
    </>
  );
};

export default PhoneTableCollapse;
