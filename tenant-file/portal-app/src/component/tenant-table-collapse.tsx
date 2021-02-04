import React, { useState } from "react";
import { Collapse } from "reactstrap";
import { Cell, Row } from "react-table";
import { useHistory } from "react-router-dom";
import Image from "./image";
import styled from "styled-components";

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

type TenantRow = {
  name: string;
  id: string;
  phone: string;
  images: string[];
};

type Props = {
  row: Row<TenantRow>;
};

const TenantTableCollapse = ({ row }: Props) => {
  let history = useHistory();
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);
  
  const onViewClick = (userId: string) => {
    history.push(`/dashboard/tenant/${userId}`);
  };

  return (
    <>
      <tr {...row.getRowProps()} onClick={toggle}>
        {row.cells.map((cell: Cell<TenantRow, any>, index: number) => {
          return (
            <td {...cell.getCellProps()}>
              {console.log(row, cell)}
              {cell.column.Header === "Images" ? (
                <Image name={cell.value[0]} />
              ) : (
                cell.render("Cell")
              )}
            </td>
          );
        })}
        <td></td>
        <td>
          <button onClick={() => onViewClick(row.original.id)}>View</button>
        </td>
      </tr>

      <tr>
        <td
          colSpan={3}
          className="text-center"
          style={isOpen ? {} : { padding: 0 }}
        >
          <Collapse isOpen={isOpen}>
            {row.cells[2].value.length ? (
              <ImageGridStyles>
                {row.cells[2].value.map((i: string) => (
                  <>
                    <Image name={i} />
                  </>
                ))}
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

export default TenantTableCollapse;
