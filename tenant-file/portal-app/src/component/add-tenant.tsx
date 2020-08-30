import React from "react";

import ReactDOM from "react-dom"

import { Formik, Field, Form } from "formik";

//const AddTenant: React.FC = () => (<div >I AM A DIV</div>);

const AddTenant = () => (
    <Formik
        initialValues={{
            firstName: '',
            lastName: '',
            addressLn1: '',
            addressLn2: '',
            city: '',
            state: '',
            zip: '',
            bldgName: ''
        }}
        onSubmit={async values => {
            await new Promise(r => setTimeout(r, 500));
            alert(JSON.stringify(values, null, 2));
        }}>
        <Form>
            <label htmlFor="firstName">First Name:</label>
            <Field id="firstName" name="firstName" />

            <label htmlFor="lastName">Last Name:</label>
            <Field id="lastName" name="lastName" />

            <label htmlFor="addressLn1">Address Line 1:</label>
            <Field id="addressLn1" name="addressLn1" />

            <label htmlFor="addressLn2">Address Line 2:</label>
            <Field id="addressLn2" name="addressLn2" />

            <label htmlFor="city">City:</label>
            <Field id="city" name="city" />

            <label htmlFor="state">State:</label>
            <Field id="state" name="state" />

            <label htmlFor="zip">Zip Code:</label>
            <Field id="zip" name="zip" />

            <label htmlFor="bldgName">Building Name:</label>
            <Field id="bldgName" name="bldgName" />
        </Form>
    </Formik>
);

ReactDOM.render(<AddTenant />, document.getElementById('root'));

export default AddTenant;