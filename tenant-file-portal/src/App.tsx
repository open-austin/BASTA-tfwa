import React, { useEffect, useState } from "react";
import logo from "./logo.svg";
import { Col, Container, Navbar, Row, NavbarBrand } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";

import { Server } from "miragejs";

new Server({
  routes() {
    this.namespace = "/api";

    this.get("/images", () => {
      return {
        images: ["test", "test2"]
      };
    });
  }
});

function App() {
  let [images, setImages] = useState([]);

  useEffect(() => {
    const func = async () => {
      const imageResponse = await fetch("/api/images").then(res => res.json());
      console.log("RESPONSE", imageResponse);
      const images = imageResponse.images;
      setImages(images);
    };
    func();
  }, []);
  console.log(images);

  return (
    <div className="App">
      <header className="App-header">
        <Navbar color="light" light expand="md">
          <NavbarBrand>TFWA</NavbarBrand>
        </Navbar>
      </header>
      <main>
        <Container>
          <Row>
            <Col>
              <h2>List of Images</h2>
              <ul>
                {images.map(i => (
                  <li>{i}</li>
                ))}
              </ul>
            </Col>
          </Row>
        </Container>
      </main>
    </div>
  );
}

export default App;
