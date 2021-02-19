import React, { useEffect, useState } from 'react';
import { Container, Row, Col } from 'reactstrap';
import axios from 'axios';
import { getToken } from './firebase';

const DisplayImages = () => {
  let [images, setImages] = useState([]);

  useEffect(() => {
    const func = async () => {
      const token = await getToken();
      const baseUrl =
        process.env.NODE_ENV === 'production'
          ? 'https://tenant-file-api-zmzadnnc3q-uc.a.run.app'
          : 'http://localhost:8080';
      const imageResponse = await axios
        .get(`${baseUrl}/api/images?token=${token}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((x) => x.data);
      console.log('RESPONSE', imageResponse);
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
              alt="placeholderimage"
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
