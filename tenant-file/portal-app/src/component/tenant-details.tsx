import React from "react";
import { useLocation } from "react-router-dom";
import { gql, useMutation, useQuery } from "@apollo/client";
import { Formik, Field, Form } from "formik";
import Properties from "./properties";
import styles from "./tenant-details.module.css";
import { useState } from "react";
import ImageTable from "./image-table";
import logo from "../images/tools-logo.svg";

// Page needs name, phone #(s), Property, Unit #, address, caht log, image filters (date range, image type?), image gallery

type TParams = {
  tenantId: string;
  phone: string;
};

function ValidatePhoneNumber(number: any) {
  let error;
  if (!number) {
    error = "Please enter your cell phone number. ";
  }
  if (!/^[2-9][0-9]{9}/.test(number)) {
    error =
      "Please make sure to enter your 10-digit cell phone number, without hyphens or parentheses. ";
  }
  return error;
}
//TODO: PROPS DONT WORK, path hack in place
const TenantDetails: React.FC<TParams> = (props: TParams) => {
  //TODO: If there is no Tenant, this will break. Make this Phone based as well
  const EDIT_TENANT = gql`
    mutation editTenant(
      $fullName: String!
      $phoneNumber: String!
      $street: String!
      $unitNumber: String!
      $city: String!
      $zip: String!
      $propertyId: ID
      $tenantId: ID!
    ) {
      editTenant(
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
          tenantId: $tenantId
        }
      ) {
        payload {
          id
          name
        }
      }
    }
  `;
  const GET_TENANT_DATA = gql`
    query GetTenantById($tenantId: ID!) {
      tenant(id: $tenantId) {
        name
        id
        residenceId

        residence {
          property {
            id
            name
          }
          address {
            line1
            line2
            city
            state
            postalCode
          }
        }
        phones {
          nodes {
            id
            phoneNumber
            preferredLanguage
            images {
              edges {
                node {
                  thumbnailName
                  name
                  labels {
                    label
                    confidence
                    source
                  }
                }
              }
            }
          }
        }
      }
    }
  `;
  const [isInEditMode, setEditMode] = useState(false);
  const inputClass = isInEditMode
    ? styles.formControlSm
    : styles.readOnlyInputSm;
  const location = useLocation();
  const { loading, data } = useQuery(GET_TENANT_DATA, {
    variables: { tenantId: location.pathname.split("/dashboard/tenant/")[1] },
  });
  const [
    tenantData,
    { data: datamutation, loading: loadingMutation, error: errorMutation },
  ] = useMutation(EDIT_TENANT);
  // const tenantId = props?.tenantId;

  const onEditClick = () => {
    setEditMode(!isInEditMode);
  };
  if (loading) return <p>Loading...</p>;
  return (
    <>
      <div className="container-fluid">
        <h3 className="text-dark mb-4">
          {`Tenant Details: ${data?.tenant.name.split(" ")[0]} ${
            data?.tenant.name.split(" ")[1]
          } ` ?? ""}
        </h3>
        <div className="row mb-3">
          <div className="col-lg-4">
            <div className="card mb-3">
              <div className="card-body text-center shadow">
                <img
                  className="rounded-circle mb-3 mt-4"
                  src={logo}
                  alt=""
                  width={125}
                  height={125}
                />
                <div className="mb-3">
                  <button type="button" className={styles.bastaBtn}>
                    <i className="las la-image"></i> Change Photo
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div className="col-lg-8">
            <div className="row">
              <div className="col">
                <Formik
                  initialValues={{
                    firstName: data?.tenant?.name.split(" ")[0] ?? "",
                    lastName: data?.tenant?.name.split(" ")[1] ?? "",
                    street: data?.tenant?.residence?.address?.line1 ?? "",
                    unitNumber: data?.tenant?.residence?.address?.line2 ?? "",
                    city: data?.tenant?.residence?.address?.city ?? "",
                    state: data?.tenant?.residence?.address?.state ?? "",
                    zip: data?.tenant?.residence?.address?.postalCode ?? "",
                    propertyId: data?.tenant?.residence?.property?.id ?? "",
                    propertyName: data?.tenant?.residence?.property?.name ?? "",
                    phoneNumber: props.phone
                      ? props.phone.split("+1")[1] ?? ""
                      : data?.tenant?.phones?.nodes[0]?.phoneNumber.split(
                          "+1"
                        )[1] ?? "",
                  }}
                  onSubmit={(e) => {
                    console.log(`e.params ${JSON.stringify(e)}`);
                    setEditMode(false)
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
                        tenantId:
                          location.pathname.split("/dashboard/tenant/")[1],
                      },
                    });
                    setTimeout(() => {}, 10000);
                  }}
                >
                  {({
                    errors,
                    touched,
                    validateForm,
                    validateField,
                    values,
                  }) => (
                    <Form>
                      <div className="card shadow mb-3">
                        <div className={styles.cardHeader}>
                          <div className="d-flex align-items-top ">
                            <h4 className="pr-3 font-weight-bold">
                              Contact Info
                            </h4>
                            <button
                              type="button"
                              onClick={onEditClick}
                              className={styles.bastaBtnSm}
                            >
                              <i className="las la-edit"></i> Edit
                            </button>
                          </div>
                        </div>
                        <div className="card-body pt-1 pb-3">
                          <div className="form-row">
                            <div className="col">
                              <div className="form-group m-1">
                                <label
                                  className={styles.formLabel}
                                  htmlFor="firstName"
                                >
                                  <strong>*First Name:</strong>
                                </label>
                                <Field
                                  disabled={!isInEditMode}
                                  className={inputClass}
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
                                  disabled={!isInEditMode}
                                  className={inputClass}
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
                                  disabled={!isInEditMode}
                                  className={inputClass}
                                  name="phoneNumber"
                                  validate={ValidatePhoneNumber}
                                />
                                {errors.phoneNumber && touched.phoneNumber && (
                                  <div>{errors.phoneNumber}</div>
                                )}
                              </div>
                            </div>
                          </div>

                          <div className="form-row">
                            <div className="col">
                              <div className="form-group m-1">
                                <label
                                  className={styles.formLabel}
                                  htmlFor="bldg"
                                >
                                  <strong>*Property:</strong>
                                </label>
                                {isInEditMode ? (
                                  <Properties
                                    defaultProperty={values.propertyId}
                                    bldgSelectHandler={(
                                      keyValueArray: string
                                    ) => {
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
                                ) : (
                                  <div className={inputClass}>
                                    {values.propertyName}
                                  </div>
                                )}
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
                                  disabled={!isInEditMode}
                                  className={inputClass}
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
                                  disabled={!isInEditMode}
                                  className={inputClass}
                                  name="unitNumber"
                                />
                              </div>
                            </div>
                          </div>

                          <div className="form-row">
                            <div className="col">
                              <div className="form-group m-1">
                                <label
                                  className={styles.formLabel}
                                  htmlFor="city"
                                >
                                  <strong>*City:</strong>
                                </label>
                                <Field
                                  disabled={!isInEditMode}
                                  className={inputClass}
                                  name="city"
                                />
                              </div>
                            </div>
                            <div className="col-sm-2">
                              <div className="form-group m-1">
                                <label
                                  className={styles.formLabel}
                                  htmlFor="state"
                                >
                                  <strong>*State:</strong>
                                </label>
                                <Field
                                  disabled={!isInEditMode}
                                  className={inputClass}
                                  name="state"
                                />
                              </div>
                            </div>
                            <div className="col-sm-2">
                              <div className="form-group m-1">
                                <label
                                  className={styles.formLabel}
                                  htmlFor="zip"
                                >
                                  <strong>*Zip Code:</strong>
                                </label>
                                <Field
                                  disabled={!isInEditMode}
                                  className={inputClass}
                                  name="zip"
                                />
                              </div>
                            </div>
                            <div className="col d-flex justify-content-center align-items-end ">
                              <div className="form-group  m-1">
                                <button
                                  className={styles.bastaBtn}
                                  type="submit"
                                  disabled={!isInEditMode}
                                >
                                  <i className="las la-check-square"> </i>{" "}
                                  Submit
                                </button>
                              </div>
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
      <div className="row  justify-content-center">
        <div className="col mx-5">
          <ImageTable
            phoneNumber={data?.tenant?.phones?.nodes[0]?.phoneNumber}
            tenantName={data?.tenant.name.split(" ")[0]}
            phoneId={
              data?.tenant?.phones?.nodes[0]?.id === undefined
                ? ""
                : data?.tenant?.phones?.nodes[0]?.id
            }
          ></ImageTable>
        </div>
      </div>
    </>
  );
};

export default TenantDetails;
