import React from "react";

import ReactDOM from "react-dom";

import { Formik, Field, Form, ErrorMessage } from "formik";

import { gql, useMutation } from '@apollo/client'
import { Input } from "reactstrap";
import { fieldNameFromStoreName } from "@apollo/client/cache/inmemory/helpers";
import { stringify } from "querystring";
import { Console } from "console";

//const AddTenant: React.FC = () => (<div >I AM A DIV</div>);

// Credit to the Apollo GraphQL documentation:
/* const ADD_TENANT = gql`
    mutation addingATenant {
        createTenant(inputTenant: { name: "Sample 2", phoneNumber: "5121231234", houseNumber: 1333, street: "Shore District Dr", city: "Austin", zipCode: 78741, propertyName: "South Shore District" })
        {
                name
        }
    }
`; */

const ADD_TENANT = gql`
    mutation addingATenant($fullName: String!, $phoneNumber: String!, $houseNumber: Int!, $street: String!, $city: String!, $zip: Int!, $bldgName: String!) {
        createTenant(inputTenant: { name: $fullName, phoneNumber: $phoneNumber, houseNumber: $houseNumber, street: $street, city: $city, zipCode: $zip, propertyName: $bldgName })
        {
                name
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
    const [addTenant] = useMutation(ADD_TENANT);

    return <Formik
        initialValues={{
            firstName: '',
            lastName: '',
            houseNumber: '',
            street: '',
            city: '',
            state: '',
            zip: '',
            bldgName: '',
            phoneNumber: ''
        }}
        onSubmit={         
            async e => {
                console.log("****FIRST NAME IS: " + e.firstName);
                addTenant({
                    variables:
                    {
                        fullName: e.firstName + " " + e.lastName,
                        houseNumber: parseInt(e.houseNumber),
                        street: e.street,
                        city: e.city,
                        state: e.state,
                        zip: parseInt(e.zip),
                        bldgName: e.bldgName,
                        phoneNumber: e.phoneNumber
                    }
                })
                // .then(({ data: variables }) =>
                //     console.log(variables)
                // );
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
                <label htmlFor="bldgName">*Building Name:</label>
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