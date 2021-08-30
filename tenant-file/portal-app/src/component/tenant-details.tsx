import React from "react";
import { RouteComponentProps, useLocation } from "react-router-dom";
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

//TODO: If there is no Tenant, this will break. Make this Phone based as well
const ADD_TENANT = gql`
  mutation addingATenant(
    $fullName: String!
    $phoneNumber: String!
    $street: String!
    $unitNumber: String!
    $city: String!
    $zip: String!
    $bldgId: ID
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
          propertyId: $bldgId
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
  `;
  const [isInEditMode, setEditMode] = useState(false);
  const inputClass = isInEditMode
    ? styles.formControlSm
    : styles.readOnlyInputSm;
  const [tenantData] = useMutation(ADD_TENANT);
  const location = useLocation();
  const tenantId = props?.tenantId;

  const { loading, data } = useQuery(GET_TENANT_DATA, {
    variables: { tenantId: location.pathname.split("/dashboard/tenant/")[1] },
  });
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
                    unitNumber: data?.address?.line2 ?? "",
                    city: data?.tenant?.residence?.address?.city ?? "",
                    state: data?.tenant?.residence?.address?.state ?? "",
                    zip: data?.tenant?.residence?.address?.postalCode ?? "",
                    bldgId: data?.tenant?.residence?.property?.id ?? "",
                    phoneNumber: props.phone
                      ? props.phone.split("+1")[1] ?? ""
                      : data?.tenant?.phones?.nodes[0]?.phoneNumber.split(
                          "+1"
                        )[1] ?? "",
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
                        bldgId: e.bldgId,
                        //Submitting the phoneNumber alone is fine because the CreateTenant Mutation checks if the phone is registered already
                        phoneNumber: "+1" + e.phoneNumber,
                      },
                    });
                    setTimeout(() => {}, 1000);
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
                                <Properties
                                  bldgSelectHandler={(id: string) =>
                                    (values.bldgId = id)
                                  }
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
