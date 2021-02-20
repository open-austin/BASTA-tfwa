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

    const fieldIndentation = {
        textIndent: '3em'
    };

    const labelIndentation = {
        textIndent: '50px'
    };

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
                <h1 style={labelIndentation}>Add a New Property or Complex to the Tenant File</h1>
                <h4 style={labelIndentation}>Before adding a property, please make sure it hasn't already been added.</h4>
                <br></br>

                <h3 style={labelIndentation}>Address Info:</h3>
                <label style={fieldIndentation} htmlFor="addrLn1">*Address - Line 1:</label>
                <Field style={fieldIndentation} id="addrLn1" name="addrLn1" />
                <br></br>
                <label style={fieldIndentation} htmlFor="addrLn2">Address - Line 2:</label>
                <Field style={fieldIndentation} id="addrLn2" name="addrLn2" />
                <br></br>
                <label style={fieldIndentation} htmlFor="addrLn3">Address - Line 3:</label>
                <Field style={fieldIndentation} id="addrLn3" name="addrLn3" />
                <br></br>
                <label style={fieldIndentation} htmlFor="addrLn4">Address - Line 4:</label>
                <Field style={fieldIndentation} id="addrLn4" name="addrLn4" />
                <br></br>
                <label style={fieldIndentation} htmlFor="city">*City:</label>
                <Field style={fieldIndentation} id="city" name="city" />
                <br></br>
                <label style={fieldIndentation} htmlFor="state">*State:</label>
                <Field style={fieldIndentation} id="state" name="state" />
                <br></br>
                <label style={fieldIndentation} htmlFor="zip">*Zip Code:</label>
                <Field style={fieldIndentation} id="zip" name="zip" />
                <br></br>
                <br></br>

                <h3 style={labelIndentation}>Residence/Property Info:</h3>
                <label style={fieldIndentation} htmlFor="bldgName">*Building Name:</label>
                <Field style={fieldIndentation} id="bldgName" name="bldgName" />
                <br></br>

                <button>Click Here to Submit</button>
            </Form>
        )}
    </Formik>
}
ReactDOM.render(<form />, document.getElementById('root'));