import React from "react";

import ReactDOM from "react-dom";

import { Formik, Field, Form } from "formik";

import { gql, useMutation } from '@apollo/client';

import styled from "styled-components";

// Credit to the documentation for GraphQL, Apollo, and Formik
// Credit to this list of states + DC: https://gist.github.com/pusherman/3145761


const AddPropertyWrapper = styled.div`
  margin: 2rem;
`

const FormContainer = styled.div`
  display: grid;
  grid-template-columns: auto auto;
  grid-auto-flow: column;
`

const FormColumn = styled.div`
  
`

const ADD_PROPERTY = gql`
    mutation addingAProperty($bldgName: String!, $addrLn1: String!, $addrLn2: String!, $addrLn3: String!, $addrLn4: String!, $city: String!, $state: String!, $zip: String!) {
        createProperty(input: { name: $bldgName, addressInput: { line1: $addrLn1, line2: $addrLn2, line3: $addrLn3, line4: $addrLn4, city: $city, state: $state, postalCode: $zip } })
        {
            payload {
                name
            }
        }
    }
`;

export default () => {

    const [addProperty] = useMutation(ADD_PROPERTY);

    const labelIndentation = {
        textIndent: '50px'
    };

    return <AddPropertyWrapper><Formik
        initialValues={{
            addrLn1: '',
            addrLn2: '',
            addrLn3: '',
            addrLn4: '',
            city: '',
            state: 'TX',
            zip: '',
            bldgName: ''
        }}
        onSubmit={         
            async e => {
                addProperty({
                    variables:
                    {
                        addrLn1: e.addrLn1,
                        addrLn2: e.addrLn2,
                        addrLn3: e.addrLn3,
                        addrLn4: e.addrLn4,
                        city: e.city,
                        state: e.state,
                        zip: e.zip,
                        bldgName: e.bldgName
                    }
                }).catch(x => console.log(x))
                setTimeout(() => { }, 1000);
                console.log("addrLn1 is: " + e.addrLn1)
            }
        }>
        {({ errors, touched, validateForm, validateField }) => (
            <Form>
                <h1 style={labelIndentation}>Add a New Property or Complex to the Tenant File</h1>
                <h6 style={labelIndentation}>Before adding a property, please make sure it hasn't already been added.</h6>
                <h6 style={labelIndentation}>Please do not include a unit/apartment number in the address fields.</h6>
                <h6 style={labelIndentation}>Unit numbers can be entered in the addresses for specific Residences.</h6>

                <FormContainer>
                    <FormColumn>
                        <h3 style={labelIndentation}>Property Info:</h3>
                        <label style={{textIndent: '61px'}} htmlFor="bldgName">*Building Name:</label>
                        <Field id="bldgName" name="bldgName" />
                    </FormColumn>
                    <FormColumn>
                        <h3 style={labelIndentation}>Address Info:</h3>
                        <label style={{textIndent: '50px'}} htmlFor="addrLn1">*Address - Line 1:</label>
                        <Field id="addrLn1" name="addrLn1" />
                        {errors.addrLn1 && touched.addrLn1 && <div>{errors.addrLn1}</div>}
                        <label style={{textIndent: '54px'}} htmlFor="addrLn2">Address - Line 2:</label>
                        <Field id="addrLn2" name="addrLn2" />
                        <label style={{textIndent: '53px'}} htmlFor="addrLn3">Address - Line 3:</label>
                        <Field id="addrLn3" name="addrLn3" />
                        <label style={{textIndent: '53px'}} htmlFor="addrLn4">Address - Line 4:</label>
                        <Field id="addrLn4" name="addrLn4" />
                        <label style={{textIndent: '137px'}} htmlFor="city">*City:</label>
                        <Field id="city" name="city" />
                        <label style={{textIndent: '127px'}} htmlFor="state">*State:</label>
                        <Field as="select" id="state" name="state">
                            <option value="AL">Alabama</option>
                            <option value="AK">Alaska</option>
                            <option value="AZ">Arizona</option>
                            <option value="AR">Arkansas</option>
                            <option value="CA">California</option>
                            <option value="CO">Colorado</option>
                            <option value="CT">Connecticut</option>
                            <option value="DE">Delaware</option>
                            <option value="DC">District Of Columbia</option>
                            <option value="FL">Florida</option>
                            <option value="GA">Georgia</option>
                            <option value="HI">Hawaii</option>
                            <option value="ID">Idaho</option>
                            <option value="IL">Illinois</option>
                            <option value="IN">Indiana</option>
                            <option value="IA">Iowa</option>
                            <option value="KS">Kansas</option>
                            <option value="KY">Kentucky</option>
                            <option value="LA">Louisiana</option>
                            <option value="ME">Maine</option>
                            <option value="MD">Maryland</option>
                            <option value="MA">Massachusetts</option>
                            <option value="MI">Michigan</option>
                            <option value="MN">Minnesota</option>
                            <option value="MS">Mississippi</option>
                            <option value="MO">Missouri</option>
                            <option value="MT">Montana</option>
                            <option value="NE">Nebraska</option>
                            <option value="NV">Nevada</option>
                            <option value="NH">New Hampshire</option>
                            <option value="NJ">New Jersey</option>
                            <option value="NM">New Mexico</option>
                            <option value="NY">New York</option>
                            <option value="NC">North Carolina</option>
                            <option value="ND">North Dakota</option>
                            <option value="OH">Ohio</option>
                            <option value="OK">Oklahoma</option>
                            <option value="OR">Oregon</option>
                            <option value="PA">Pennsylvania</option>
                            <option value="RI">Rhode Island</option>
                            <option value="SC">South Carolina</option>
                            <option value="SD">South Dakota</option>
                            <option value="TN">Tennessee</option>
                            <option value="TX">Texas</option>
                            <option value="UT">Utah</option>
                            <option value="VT">Vermont</option>
                            <option value="VA">Virginia</option>
                            <option value="WA">Washington</option>
                            <option value="WV">West Virginia</option>
                            <option value="WI">Wisconsin</option>
                            <option value="WY">Wyoming</option>
                        </Field>
                        <label style={{textIndent: '99px'}} htmlFor="zip">*Zip Code:</label>
                        <Field id="zip" name="zip" />

                        <button>Click Here to Submit</button>
                    </FormColumn>
                </FormContainer>

                
            </Form>
        )}
    </Formik></AddPropertyWrapper>
}
