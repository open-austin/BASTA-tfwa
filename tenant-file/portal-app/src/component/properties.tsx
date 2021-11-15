import React from "react";

import { gql, useQuery } from "@apollo/client";
import { getProps_properties_nodes } from "../types/getProps";

type Props = {
  defaultProperty: string;
  bldgSelectHandler: Function;
};
const Properties = ({ bldgSelectHandler, defaultProperty }: Props) => {
  const GET_PROPERTIES = gql`
    query getProps {
      properties {
        nodes {
          id
          name
        }
      }
    }
  `;
  const { loading, data } = useQuery(GET_PROPERTIES);
  if (loading)
    return (
      <select
        className="form-control form-control-sm"
        id="bldgId"
        name="bldgId"
      >
        <div className="spinner-border">loading...</div>
      </select>
    );
  return (
    <select
      onChange={(e) => {
        console.log(`e.currentTarget.value: ${e.currentTarget.value}`);
        console.log(
          `e.currentTarget.value: ${JSON.stringify(e.currentTarget.value)}`
        );
        // console.log(`e.currentTarget.text: ${e.currentTarget.innerText}`);
        // console.log(`e.currentTarget.text: ${e.currentTarget.innerHTML}`);
        bldgSelectHandler(e.currentTarget.value);
      }}
      className="form-control form-control-sm"
      id="bldgId"
      name="bldgId"
    >
      {data?.properties?.nodes?.map((bldgNode: getProps_properties_nodes) => {
        //bldgSelectHandler(bldgNode);

        return defaultProperty === bldgNode.id ? (
          <option
            selected
            key={bldgNode.id}
            value={[bldgNode.id, bldgNode?.name ?? ""]}
          >
            {bldgNode.name}
          </option>
        ) : (
          <option key={bldgNode.id} value={[bldgNode.id, bldgNode?.name ?? ""]}>
            {bldgNode.name}
          </option>
        );
      })}
    </select>
  );
};

export default Properties;
