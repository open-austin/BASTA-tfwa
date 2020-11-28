import React from "react";

import ReactDOM from "react-dom";

import { Formik, Field, Form, ErrorMessage } from "formik";

import { gql, useMutation } from '@apollo/client'
import { Input } from "reactstrap";
import { fieldNameFromStoreName } from "@apollo/client/cache/inmemory/helpers";

//const AddTenant: React.FC = () => (<div >I AM A DIV</div>);

// Credit to the Apollo GraphQL documentation:
const ADD_TENANT = gql`
    mutation AddTenant($type: String!) {
        addTenant(type: $type) {
            firstName
            lastName
            addressLn1
            addressLn2
            city
            state
            zip
            bldgName
            phoneNumber
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

    //let input: { value: string; };
    const [addTenant, { data }] = useMutation(ADD_TENANT);

    return <Formik
        initialValues={{
            firstName: '',
            lastName: '',
            addressLn1: '',
            addressLn2: '',
            city: '',
            state: '',
            zip: '',
            bldgName: '',
            phoneNumber: ''
        }}
        onSubmit={
            // addTM({ variables: { type: String } });
         
            async e => {
                //await addTenant({ variables:  });
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
                <label htmlFor="addressLn1">*Address Line 1:</label>
                <Field id="addressLn1" name="addressLn1" />
                <br></br>
                <label htmlFor="addressLn2">Address Line 2:</label>
                <Field id="addressLn2" name="addressLn2" />
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
                <label htmlFor="bldgName">Building Name:</label>
                <Field id="bldgName" name="bldgName" />
                <br></br>
                <label htmlFor="phoneNumber">*Cell Phone Number:</label>
                <Field id="phoneNumber" name="phoneNumber" validate={ValidatePhoneNumber} />
                {errors.phoneNumber && touched.phoneNumber && <div>{errors.phoneNumber}</div>}
                <br></br>
                <button>Click Here to Submit</button>
            </Form>
        )}
    </Formik>
}
ReactDOM.render(<form />, document.getElementById('root'));
//export default ErrorPrintingFunction;
// Credit to Formik documentation - this error rendering logic is copied largely from it as well: