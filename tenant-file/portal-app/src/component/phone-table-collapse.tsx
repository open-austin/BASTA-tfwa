import React, { useState } from "react";
import { Collapse } from "reactstrap";
import { Cell, Row } from "react-table";
import { useHistory } from "react-router-dom";
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

type PhoneRow = {
  name: string;
  tenantId: string;
  phone: string;
  images: [string, string][];
  property: string;
  labels: [string, string[]][];
};

type Props = {
  row: Row<PhoneRow>;
};
const storage = firebase.app().storage();
const PhoneTableCollapse = ({ row }: Props) => {
  let history = useHistory();
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);

  const onViewClick = (userId: string) => {
    history.push(`/dashboard/tenant/${userId}`);
  };

  return (
    <>
      <tr {...row.getRowProps()} onClick={toggle}>
        {row.cells.map((cell: Cell<PhoneRow, any>, index: number) => {
          return (
            <>
              <td {...cell.getCellProps()}>
                {cell.column.Header === "Name" &&
                row.original.tenantId === "" ? (
                  <button className="btn btn-info">Register Tenant</button>
                ) : (
                  <></>
                )}
                {cell.column.Header === "Images" ? (
                  <Image
                    storage={storage}
                    name={cell.value?.[1]?.[1]}
                    id={
                      "image" +
                      (
                        cell.value?.[1]?.[0].replace("=", "") +
                        row.original.tenantId
                      ).replace("=", "")
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

        <td>
          <button
            className="btn btn-secondary"
            onClick={() => onViewClick(row.original.tenantId)}
          >
            View
          </button>
        </td>
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
                          id={`image${
                            i[0].replace("=", "") +
                            row.original.tenantId.replace("=", "")
                          }`}
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
