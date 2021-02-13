import React from "react";

import ReactDOM from "react-dom";

import { Formik, Field, Form, ErrorMessage } from "formik";

import { gql, useMutation } from '@apollo/client'
import { Input } from "reactstrap";
import { fieldNameFromStoreName } from "@apollo/client/cache/inmemory/helpers";
import { stringify } from "querystring";
import { Console } from "console";

// Credit to the documentation for GraphQL, Apollo, and Formik

const ADD_PROPERTY = gql`
    mutation addingAProperty($fullName: String!, $phoneNumber: String!, $houseNumber: Int!, $street: String!, $city: String!, $zip: Int!, $bldgName: String!) {
        createProperty(inputTenant: { name: $fullName, phoneNumber: $phoneNumber, houseNumber: $houseNumber, street: $street, city: $city, zipCode: $zip, propertyName: $bldgName })
        {
                name
        }
    }
`;

export default () => {

    const [addProperty] = useMutation(ADD_PROPERTY);

    return <Formik
        initialValues={{
            addrLn1: '',
            addrLn2: '',
            addrLn3: '',
            addrLn4: '',
            city: '',
            state: '',
            zip: '',
            bldgName: ''
        }}
        onSubmit={         
            async e => {
                addProperty({
                    variables:
                    {
                        /* fullName: e.firstName + " " + e.lastName,
                        houseNumber: parseInt(e.houseNumber),
                        street: e.street,
                        city: e.city,
                        state: e.state,
                        zip: parseInt(e.zip),
                        bldgName: e.bldgName,
                        phoneNumber: e.phoneNumber */
                    }
                })
                setTimeout(() => { }, 1000);
            }
        }>
        {({ errors, touched, validateForm, validateField }) => (
            <Form>
                <h1>Address Info:</h1>
                <label htmlFor="addrLn1">*Address - Line 1:</label>
                <Field id="addrLn1" name="addrLn1" />
                <br></br>
                <label htmlFor="addrLn2">Address - Line 2:</label>
                <Field id="addrLn2" name="addrLn2" />
                <br></br>
                <label htmlFor="addrLn3">Address - Line 3:</label>
                <Field id="addrLn3" name="addrLn3" />
                <br></br>
                <label htmlFor="addrLn4">Address - Line 4:</label>
                <Field id="addrLn4" name="addrLn4" />
                <br></br>
                <label htmlFor="city">*City:</label>
                <Field id="city" name="city" />
                <br></br>
                <label htmlFor="state">*State:</label>
                <Field id="state" name="state" />
                <br></br>
                <label htmlFor="zip">*Zip Code:</label>
                <Field id="zip" name="zip" />
                <br></br>

                <h1>Residence/Property Info:</h1>
                <label htmlFor="bldgName">Building Name:</label>
                <Field id="bldgName" name="bldgName" />
                <br></br>

                <button>Click Here to Submit</button>
            </Form>
        )}
    </Formik>
}
ReactDOM.render(<form />, document.getElementById('root'));