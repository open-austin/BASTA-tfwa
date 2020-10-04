import React from 'react';
import PropertyForm from './property-form';
import { Container, Row, Col } from 'reactstrap';

// TODO: Mutation to be implemented
// const CREATE_PROPERTY = gql``;

const Properties = () => {
  // Below are dummy values that will later come from useMutation once Apollo is linked to the backend
  let addProperty = (values: Object) => {
    console.log('Creating new property', values);
  };
  let loading = false;
  const error = null;

  const isError = () => {
    if (error) return true;
    return false;
  };

  return (
    <Container>
      <Row>
        <Col>
          <h1>New Property</h1>
          <PropertyForm
            onSubmit={addProperty}
            loading={loading}
            error={isError()}
          />
        </Col>
      </Row>
    </Container>
  );
};

export default Properties;
