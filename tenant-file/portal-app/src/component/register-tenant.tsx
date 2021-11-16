import React from "react";
import { useHistory, useLocation } from "react-router-dom";
import { gql, useMutation } from "@apollo/client";
import { Formik, Field, Form } from "formik";
import Properties from "./properties";
import styles from "./tenant-details.module.css";

const RegisterTenant = () => {
  const history = useHistory();
  const ADD_TENANT = gql`
    mutation createTenant(
      $fullName: String!
      $phoneNumber: String!
      $street: String!
      $unitNumber: String!
      $city: String!
      $zip: String!
      $propertyId: ID
    ) {
      createTenant(
        inputTenant: {
          name: $fullName
          phoneNumber: $phoneNumber
          currentResidence: {
            addressInput: {
              line1: $street
              line2: $unitNumber
              city: $city
              state: "TX"
              postalCode: $zip
            }
            propertyId: $propertyId
          }
        }
      ) {
        payload {
          id
          name
        }
      }
    }
  `;
  const [tenantData, { data }] = useMutation(ADD_TENANT);
  const onViewClick = () => {
    history.push({
      pathname: data?.createTenant.payload.id
        ? `/dashboard/tenant/${data?.createTenant.payload.id}`
        : "/dashboard",
      //   state: { [tenantId]: data.createTenant.payload.id },
    });
  };
  const location = useLocation();
  const phonePath = location.pathname.split("/register-tenant/")[1];
  return (
    <div className="container-fluid col-lg-11">
      <div className="card shadow">
        <div className="mt-2 py-5 px-2">
          <div className="row">
            <div className="container-fluid col-7">
              <Formik
                initialValues={{
                  firstName: "",
                  lastName: "",
                  street: "",
                  unitNumber: "",
                  city: "",
                  state: "",
                  zip: "",
                  propertyId: "000",
                  propertyName: "Select a Property",
                  phoneNumber: phonePath,
                }}
                onSubmit={async (e) => {
                  tenantData({
                    variables: {
                      fullName: e.firstName + " " + e.lastName,
                      street: e.street,
                      unitNumber: e.unitNumber,
                      city: e.city,
                      state: e.state,
                      zip: e.zip,
                      propertyId: e.propertyId,
                      //Submitting the phoneNumber alone is fine because the CreateTenant Mutation checks if the phone is registered already
                      phoneNumber: "+1" + e.phoneNumber,
                    },
                  }).then(() => {
                    // console.log(`data ${JSON.stringify(data)}`);
                    // console.log(
                    //   `data.payload.id ${data.createTenant.payload.id}`
                    // );
                    onViewClick();
                  });
                  setTimeout(() => {}, 10000);
                }}
              >
                {({ values }) => (
                  <Form>
                    <div className="card shadow mb-3">
                      <div className={styles.cardHeader}>
                        <div className="d-flex align-items-top ">
                          <h4 className="pr-3 font-weight-bold">
                            Register Tenant
                          </h4>
                        </div>
                      </div>
                      <div className="card-body pt-1 pb-3">
                        <div className="col">
                          <div className="form-group m-1">
                            <label
                              className={styles.formLabel}
                              htmlFor="firstName"
                            >
                              <strong>*First Name:</strong>
                            </label>
                            <Field
                              className={styles.formControl}
                              name="firstName"
                            />
                          </div>
                        </div>
                        <div className="col">
                          <div className="form-group m-1">
                            <label
                              className={styles.formLabel}
                              htmlFor="lastName"
                            >
                              <strong>*Last Name:</strong>
                            </label>
                            <Field
                              className={styles.formControl}
                              name="lastName"
                            />
                          </div>
                        </div>
                        <div className="col">
                          <div className="form-group m-1">
                            <label
                              className={styles.formLabel}
                              htmlFor="phoneNumber"
                            >
                              <strong>*Cell Phone Number:</strong>
                            </label>
                            <Field
                              className={styles.formControl}
                              name="phoneNumber"
                              disabled={true}
                            />
                          </div>
                        </div>

                        <div className="col">
                          <div className="form-group m-1">
                            <label className={styles.formLabel} htmlFor="bldg">
                              <strong>*Property:</strong>
                            </label>
                            <Properties
                              defaultProperty={values.propertyId ?? ""}
                              bldgSelectHandler={(keyValueArray: string) => {
                                let arr = keyValueArray.split(",");
                                // console.log(
                                //   `values.propertyId ${arr[0]}`
                                // );
                                // console.log(
                                //   `values.propertyName ${arr[1]}`
                                // );
                                values.propertyId = arr[0];
                                values.propertyName = arr[1];
                              }}
                            ></Properties>
                          </div>
                        </div>
                        <div className="col">
                          <div className="form-group m-1">
                            <label
                              className={styles.formLabel}
                              htmlFor="street"
                            >
                              <strong>*Street:</strong>
                            </label>
                            <Field
                              className={styles.formControl}
                              name="street"
                            />
                          </div>
                        </div>
                        <div className="col">
                          <div className="form-group m-1">
                            <label
                              className={styles.formLabel}
                              htmlFor="unitNumber"
                            >
                              <strong>Unit Number:</strong>
                            </label>
                            <Field
                              className={styles.formControl}
                              name="unitNumber"
                            />
                          </div>
                        </div>

                        <div className="col">
                          <div className="form-group m-1">
                            <label className={styles.formLabel} htmlFor="city">
                              <strong>*City:</strong>
                            </label>
                            <Field className={styles.formControl} name="city" />
                          </div>
                        </div>
                        <div className="col">
                          <div className="form-group m-1">
                            <label className={styles.formLabel} htmlFor="state">
                              <strong>*State:</strong>
                            </label>
                            <Field
                              className={styles.formControl}
                              name="state"
                            />
                          </div>
                        </div>
                        <div className="col">
                          <div className="form-group m-1">
                            <label className={styles.formLabel} htmlFor="zip">
                              <strong>*Zip Code:</strong>
                            </label>
                            <Field className={styles.formControl} name="zip" />
                          </div>
                        </div>
                        <div className="col d-flex justify-content-center align-items-end ">
                          <div className="form-group  m-1">
                            <button className={styles.bastaBtn} type="submit">
                              <i className="las la-check-square"> </i> Submit
                            </button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </Form>
                )}
              </Formik>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default RegisterTenant;
