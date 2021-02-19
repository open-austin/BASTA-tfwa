import React from 'react';
import { Formik } from 'formik';
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  FormFeedback,
} from 'reactstrap';

type FormProps = { onSubmit: Function; loading: Boolean; error: Boolean };

const PropertyForm = ({ onSubmit, error, loading }: FormProps) => {
  return (
    <Formik
      initialValues={{
        street: '',
        city: '',
        state: '',
        postalCode: '',
        residence: '',
        unitIdentifier: '',
      }}
      validate={(values) => {
        // Including interface so TypeScript will allow Formik to dynamically assign properties to error object
        interface LooseObject {
          [key: string]: any;
        }
        const errors: LooseObject = {};
        const isValidZip = /(^\d{5}$)|(^\d{5}-\d{4}$)/.test(values.postalCode);
        if (!values.postalCode) {
          errors.postalCode = 'Required';
        } else if (!isValidZip) {
          errors.postalCode = 'Please enter a valid 5 or 9 digit zip code.';
        }
        return errors;
      }}
      onSubmit={(values, { setSubmitting }) => {
        onSubmit(values);
        setSubmitting(false);
      }}
    >
      {({
        values,
        errors,
        touched,
        handleChange,
        handleBlur,
        handleSubmit,
      }) => (
        <Form onSubmit={handleSubmit}>
          <FormGroup>
            <Label for="unitIdentifier">Unit Identifier</Label>
            <Input
              type="text"
              name="unitIdentifier"
              id="unitIdentifier"
              placeholder=""
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.unitIdentifier}
            />
          </FormGroup>
          <FormGroup>
            <Label for="street">Street</Label>
            <Input
              type="text"
              name="street"
              id="street"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.street}
            />
          </FormGroup>
          <FormGroup>
            <Label for="city">City</Label>
            <Input
              type="text"
              name="city"
              id="city"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.city}
            />
          </FormGroup>
          <FormGroup>
            <Label for="state">State</Label>
            <Input
              type="text"
              name="state"
              id="state"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.state}
            />
          </FormGroup>
          <FormGroup>
            <Label for="postalCode">Postal Code</Label>
            <Input
              type="text"
              name="postalCode"
              id="postalCode"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.postalCode}
              invalid={errors.postalCode && touched.postalCode ? true : false}
            />
            {errors.postalCode && touched.postalCode && (
              <FormFeedback>{errors.postalCode}</FormFeedback>
            )}
          </FormGroup>
          <FormGroup>
            <Label for="residence">Residence</Label>
            <Input
              type="text"
              name="residence"
              id="residence"
              placeholder=""
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.residence}
            />
          </FormGroup>

          <Button
            color="success"
            // checking to see if there are errors before submiting
            disabled={Object.keys(errors).length ? true : false}
            type="submit"
          >
            Submit
          </Button>
        </Form>
      )}
    </Formik>
  );
};

export default PropertyForm;
