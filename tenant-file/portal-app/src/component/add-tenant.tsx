import React from "react";

import ReactDOM from "react-dom";

import { Formik, Field, Form } from "formik";

import { gql, useMutation, useQuery } from '@apollo/client';

const ADD_TENANT = gql`
    mutation addingATenant($fullName: String!, $phoneNumber: String!, $street: String!, $city: String!, $zip: String!, $bldgId: ID) {

        createTenant(inputTenant: { name: $fullName, phoneNumber: $phoneNumber, currentResidence: { addressInput: { line1: $street, city: $city, state: "TX", postalCode: $zip }, propertyId: $bldgId } } )
        {
            payload {
                id
                name
            }
        }
    }
`;

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

// Credit to the Formik documentation - this was largely copied from an example there

function ValidatePhoneNumber(number: any) {
    let error;
    if(!number) {
        error = "Please enter your cell phone number. ";
    }
    if(!/^[2-9][0-9]{9}/.test(number)) {
        error = "Please make sure to enter your 10-digit cell phone number, without hyphens or parentheses. "
    }
    return error;
}

export default () => {

    const [addTenant] = useMutation(ADD_TENANT);

    const { loading, error, data } = useQuery(GET_PROPERTIES);

    if (loading) return null;

    //console.log("The properties are: " + data.properties.nodes[0].name);
    //console.log("The type of the properties attribute is: " + typeof(data.properties));

    return <Formik
        initialValues={{
            firstName: '',
            lastName: '',
            houseNumber: '',
            street: '',
            city: '',
            state: '',
            zip: '',
            bldgId: '',
            phoneNumber: ''
        }}
        onSubmit={         
            async e => {
                console.log("The ID is: " + e.bldgId);
                addTenant({
                    variables:
                    {
                        fullName: e.firstName + " " + e.lastName,
                        houseNumber: parseInt(e.houseNumber),
                        street: e.houseNumber + " " + e.street,
                        city: e.city,
                        state: e.state,
                        zip: e.zip,
                        bldgId: e.bldgId,
                        phoneNumber: e.phoneNumber
                    }
                })
                setTimeout(() => { }, 1000);
            }
        }>
        {({ errors, touched, validateForm, validateField }) => (
            <Form>
                <label htmlFor="firstName">*First Name:</label>
                <Field id="firstName" name="firstName" />
                <br></br>
                <label htmlFor="lastName">*Last Name:</label>
                <Field id="lastName" name="lastName" />
                <br></br>
                <label htmlFor="houseNumber">*House Number:</label>
                <Field id="houseNumber" name="houseNumber" />
                <br></br>
                <label htmlFor="street">*Street:</label>
                <Field id="street" name="street" />
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
                <label htmlFor="bldgId">*Building Name:</label>
                <select id="bldgId" name="bldgId">
                    {data.properties.nodes.map(function (node: any) {
                        return (<option key={node.id} value={node.id}>
                            {node.id}
                        </option>)
                    })}
                    {/* <option key={data.properties.nodes[0].id} value={data.properties.nodes[0].name}>
                            {data.properties.nodes[0].name}
                    </option> */}
                </select>
                <br></br>
                <label htmlFor="phoneNumber">*Cell Phone Number:</label>
                <Field id="phoneNumber" name="phoneNumber" validate={ValidatePhoneNumber} />
                {errors.phoneNumber && touched.phoneNumber && <div>{errors.phoneNumber}</div>}
                <br></br>
                <button type="submit">Click Here to Submit</button>
            </Form>
        )}
    </Formik>
}
ReactDOM.render(<form />, document.getElementById('root'));