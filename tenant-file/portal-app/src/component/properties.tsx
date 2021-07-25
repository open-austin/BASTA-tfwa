import React from "react";
import PropertyForm from "./property-form";
import { Container, Row, Col } from "reactstrap";
import { RouteComponentProps } from "react-router-dom";
import { gql, useQuery } from "@apollo/client";

type Props = {
  bldgSelectHandler: Function;
};
const Properties = ({ bldgSelectHandler }: Props) => {
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
      <select className="form-control" id="bldgId" name="bldgId">
        <div className="spinner-border">loading...</div>
      </select>
    );
  return (
    <select
      onSelect={(e) => {
        console.log(`e.currentTarget.value: ${e.currentTarget.value}`);
        bldgSelectHandler(e.currentTarget.value);
      }}
      className="form-control"
      id="bldgId"
      name="bldgId"
    >
      {data.properties.nodes.map((bldgNode: any) => {
        //bldgSelectHandler(bldgNode);

        return (
          <option key={bldgNode.id} value={bldgNode.name}>
            {bldgNode.name}
          </option>
        );
      })}
    </select>
  );
};

export default Properties;
