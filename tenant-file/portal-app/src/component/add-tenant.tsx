import React from "react";

import { Formik, Field, Form } from "formik";

import { gql, useMutation, useQuery } from '@apollo/client';

import {RouteComponentProps} from 'react-router-dom';

type TParams = {
    phone: string;
  }
const ADD_TENANT = gql`
    mutation addingATenant($fullName: String!, $phoneNumber: String!, $street: String!, $unitNumber: String!, $city: String!, $zip: String!, $bldgId: ID) {

        createTenant(inputTenant: { name: $fullName, phoneNumber: $phoneNumber, currentResidence: { addressInput: { line1: $street, line2: $unitNumber, city: $city, state: "TX", postalCode: $zip }, propertyId: $bldgId } } )
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

const AddTenant: React.FC<RouteComponentProps<TParams>> = ({match}) => {

    const [tenantData] = useMutation(ADD_TENANT);

    const { loading, data } = useQuery(GET_PROPERTIES);
        
    const labelIndentation = {
        textIndent: '50px'
    };
    
    const bldgSelectHandler = (bldgNode: any) => {
        console.log("The building ID is: " + bldgNode.id);
    };

    if (loading) return <p>Loading...</p>;
    
    return <Formik
    initialValues={{
        firstName: '',
        lastName: '',
        street: '',
        unitNumber: '',
        city: '',
        state: '',
        zip: '',
        bldgId: '',
        phoneNumber: match.params.phone? match.params.phone : ''  }}
        onSubmit={         
            async e => {
                console.log("The ID is: " + e.bldgId);
                tenantData({
                    variables:
                    {
                        fullName: e.firstName + " " + e.lastName,
                        street: e.street,
                        unitNumber: e.unitNumber,
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
        {({ errors, touched, validateForm, validateField, values }) => (
            <Form>
                <h1 style={labelIndentation}>Add a New Tenant to the Tenant File</h1>

                <label htmlFor="firstName">*First Name:</label>
                <Field id="firstName" name="firstName" />
                <br></br>
                <label htmlFor="lastName">*Last Name:</label>
                <Field id="lastName" name="lastName" />
                <br></br>
                <label htmlFor="street">*Street:</label>
                <Field id="street" name="street" />
                <br></br>
                <label htmlFor="unitNumber">Unit Number:</label>
                <Field id="unitNumber" name="unitNumber" />
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
                <label htmlFor="bldg">*Building Name:</label>
                <select id="bldgId" name="bldgId">
                    {data.properties.nodes.map( function (bldgNode: any) {
                            
                            values.bldgId = bldgNode.id;
                            bldgSelectHandler(bldgNode);

                            return (<option key={bldgNode.id} value={bldgNode.name}>
                                {bldgNode.name}
                            </option>);
                    })}

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
export default AddTenant;