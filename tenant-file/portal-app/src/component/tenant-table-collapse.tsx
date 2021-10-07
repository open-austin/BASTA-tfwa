import React, { useState } from "react";
import { Collapse } from "reactstrap";
import { Cell, Row } from "react-table";
import { useHistory } from "react-router-dom";
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

const storage = firebase.app().storage();

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

              {/* {cell.column.Header === "Images" ? (
                <Image
                  imageName={cell.value[0]}

                  id={cell.value[0]}
                  labels={["", ""]}
                />
              ) : (
                cell.render("Cell")
              )} */}
            </td>
          );
        })}
        <td>
          <button
            className="btn btn-secondary"
            onClick={() => onViewClick(row.original.id)}
          >
            View
          </button>
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
                    {/* <Image id={row.cells[0].value} imageName={i} labels={["", ""]} /> */}
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
