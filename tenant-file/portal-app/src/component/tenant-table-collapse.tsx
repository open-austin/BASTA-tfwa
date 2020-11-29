import React, { useState } from 'react';
import { Collapse } from 'reactstrap';
import { Cell } from 'react-table';
import Image from './image';

type TenantRow = {
  name: string;
  phone: string;
  images: string[];
};

type Props = {
  row: any;
};

const TenantTableCollapse = ({ row }: Props) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <>
      <tr {...row.getRowProps()} onClick={toggle}>
        {row.cells.map((cell: Cell<TenantRow, any>, index: number) => {
          return (
            <td {...cell.getCellProps()}>
              {console.log(row, cell)}
              {cell.column.Header === 'Images' ? (
                <Image name={cell.value[0]} />
              ) : (
                cell.render('Cell')
              )}
            </td>
          );
        })}
      </tr>

      <tr>
        <td
          colSpan={3}
          className="text-center"
          style={isOpen ? {} : { padding: 0 }}
        >
          <Collapse isOpen={isOpen}>
            {row.cells[2].value.length
              ? row.cells[2].value.map((i: string) => <Image name={i} />)
              : 'No images to show.'}
          </Collapse>
        </td>
      </tr>
    </>
  );
};

export default TenantTableCollapse;
