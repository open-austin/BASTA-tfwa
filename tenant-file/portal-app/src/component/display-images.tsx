import React, { useEffect, useState } from "react";
import { NavbarBrand, Navbar, Container, Row, Col } from "reactstrap";
import axios from "axios";
import { getToken } from "./firebase";

const DisplayImages = () => {
  let [images, setImages] = useState([]);

  useEffect(() => {
    const func = async () => {
      const token = await getToken();
      const baseUrl = "https://tenant-file-api-zmzadnnc3q-uc.a.run.app";
      const imageResponse = await axios
        .get(`http://localhost:8080/api/images?token=${token}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((x) => x.data);
      console.log("RESPONSE", imageResponse);
      const images = imageResponse.images;
      setImages(images);
    };
    func();
  }, []);
  console.log(images);

  return (
    <Container>
      <Row>
        {images.map((i) => (
          <Col key={i} lg="3">
            <img
              className="img-fluid"
              src={`https://tenant-file-api-zmzadnnc3q-uc.a.run.app/api/image?name=${i}`}
            />
          </Col>
        ))}
      </Row>
    </Container>
  );
};

export default DisplayImages;
