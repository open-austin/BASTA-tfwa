import React, { useState, useEffect } from "react";
import axios from "axios";
import { getToken } from "./firebase";
import firebase from "firebase";
import {
  Row,
  Container,
  Col,
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Label,
  Input,
} from "reactstrap";
import { Formik, Form, Field } from "formik";

interface UserRecord {
  uid: string;
  email: string;
  claims: Record<string, boolean>;
  displayName: string;
}

const baseUrl =
  process.env.NODE_ENV === "production"
    ? "https://tenant-file-api-zmzadnnc3q-uc.a.run.app"
    : "http://localhost:8080";

const Admin: React.FC = () => {
  let [users, setUsers] = useState<UserRecord[]>([]);

  const [modal, setModal] = useState(false);
  const [deleteModalOpen, setDeleteModalOpen] = useState(false);

  const [userBeingModified, setUserBeingModified] = useState<UserRecord | null>(
    null
  );
  const [editMode, setEditMode] = useState<"edit" | "create">("create");

  const toggleEditModal = () => setModal(!modal);
  const toggleDeleteModal = () => {
    if (deleteModalOpen) setUserBeingModified(null);
    setDeleteModalOpen(!deleteModalOpen);
  };

  useEffect(() => {
    const func = async () => {
      const token = await getToken();
      const usersResponse = await axios
        .get(`${baseUrl}/users/list`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((x) => x.data);
      // console.log("RESPONSE", usersResponse);
      setUsers(usersResponse.users);
    };
    func();
  }, []);

  return (
    <>
      <Modal isOpen={modal} toggle={toggleEditModal}>
        <ModalHeader toggle={toggleEditModal}>Modal title</ModalHeader>

        <Formik
          initialValues={{
            emailField: userBeingModified?.email || "",
            password: "",
            displayName: userBeingModified?.displayName,
            claims:
              (userBeingModified &&
                Object.keys(userBeingModified.claims).filter(
                  (c) => userBeingModified.claims[c]
                )) ||
              [],
          }}
          onSubmit={async (values, { setSubmitting }) => {
            // console.log(values);
            if (editMode === "create") {
              await firebase
                .auth()
                .createUserWithEmailAndPassword(
                  values.emailField,
                  values.password
                );
            } else if (editMode === "edit") {
              const token = await getToken();
              await axios.put(
                `${baseUrl}/user/update`,
                {
                  uid: userBeingModified?.uid,
                  email: values.emailField,
                  displayName: values.displayName,
                  claims: values.claims.reduce((map, obj) => {
                    map[obj] = true;
                    return map;
                  }, {} as Record<string, boolean>),
                },
                {
                  headers: {
                    Authorization: `Bearer ${token}`,
                  },
                }
              );
            }
            setSubmitting(false);
            setUserBeingModified(null);
            toggleEditModal();
          }}
        >
          {({ isSubmitting }) => (
            <Form>
              <ModalBody>
                <FormGroup>
                  {editMode === "edit" && <p>User: {userBeingModified?.uid}</p>}
                  <Label for="emailField">Email</Label>
                  <Input type="text" name="emailField" tag={Field} />
                  {editMode === "create" && (
                    <>
                      <Label for="password">Password</Label>
                      <Input type="password" name="password" tag={Field} />
                    </>
                  )}
                  <Label for="displayName">Display Name</Label>
                  <Input
                    type="text"
                    name="displayName"
                    defaultValue={userBeingModified?.displayName}
                    tag={Field}
                  />
                  <Label for="claims">Display Name</Label>
                  <Input
                    type="select"
                    as="select"
                    name="claims"
                    tag={Field}
                    multiple
                  >
                    <option value="admin">Admin</option>
                    <option value="organizer">Organizer</option>
                  </Input>
                </FormGroup>
              </ModalBody>
              <ModalFooter>
                <Button color="secondary" onClick={toggleEditModal}>
                  Cancel
                </Button>
                <Button color="primary" type="submit" disabled={isSubmitting}>
                  Submit
                </Button>
              </ModalFooter>
            </Form>
          )}
        </Formik>
      </Modal>
      <Modal isOpen={deleteModalOpen} toggle={toggleDeleteModal}>
        <ModalHeader toggle={toggleDeleteModal}>
          Delete {userBeingModified?.displayName || userBeingModified?.email}?
        </ModalHeader>
        <ModalBody>
          Deleting this user will remove their account and any associated data
          permanently
        </ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={toggleEditModal}>
            CANCEL
          </Button>{" "}
          <Button
            color="danger"
            onClick={async () => {
              const token = await getToken();
              await axios.delete(
                `${baseUrl}/user/delete?uid=${userBeingModified?.uid}`,
                {
                  headers: {
                    Authorization: `Bearer ${token}`,
                  },
                }
              );
              setUserBeingModified(null);
              toggleDeleteModal();
            }}
          >
            DELETE
          </Button>
        </ModalFooter>
      </Modal>
      <Container>
        <Row>
          <Col>
            <Button
              color="success"
              onClick={() => {
                setEditMode("create");
                setUserBeingModified(null);
                setModal(true);
              }}
            >
              Create User
            </Button>
            <table className="table">
              <thead>
                <tr>
                  <th scope="col">Display Name</th>
                  <th scope="col">Email</th>
                  <th scope="col">Claims</th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody>
                {users.map((user) => (
                  <tr>
                    <td>{user.displayName}</td>
                    <td>{user.email}</td>
                    <td>
                      {Object.keys(user.claims)
                        .filter((c) => user.claims[c])
                        .join(", ")}
                    </td>
                    <td>
                      <Button
                        onClick={() => {
                          setEditMode("edit");
                          setUserBeingModified(user);
                          setModal(true);
                        }}
                      >
                        Edit
                      </Button>
                      <Button
                        color="danger"
                        className="ml-2"
                        onClick={() => {
                          setUserBeingModified(user);
                          setDeleteModalOpen(true);
                        }}
                      >
                        Delete
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Admin;
