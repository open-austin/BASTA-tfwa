// Credit to:
// https://stackoverflow.com/questions/55834987/in-typescript-why-is-jsx-intrinsicelements-not-working-as-documentation-describ/55835142
// https://stackoverflow.com/questions/66207765/react-typescript-expects-at-least-3-arguments-but-the-jsx-factory-react-cr

import styles from "./imageModalStyling.module.css";

import React, { useState } from 'react';

import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

import Image from "./image";

import {
    GetImagesForPhone_phone_images,
    GetImagesForPhone_phone_images_labels,
  } from "../types/GetImagesForPhone";

type Props = {
    isOpen_param: boolean
    customHeaderContent: any

    tenantName: any
    phoneNumber: any
    image: any

    customFooterContent: any
}

const ImageModal: React.FC<Props> = ({
    isOpen_param,
    customHeaderContent,

    // Body
    tenantName,
    phoneNumber,
    image,

    customFooterContent
}: Props) => {

    const [showImageModal, setShowImageModal] = useState(isOpen_param);

    function sortLabels(ascending: boolean) {
        return function (
          right: GetImagesForPhone_phone_images_labels,
          left: GetImagesForPhone_phone_images_labels
        ) {
          if ((right?.confidence ?? 0) === (left?.confidence ?? 0)) {
            return 0;
          }
          if (ascending) {
            return (right?.confidence ?? 0) < (left?.confidence ?? 0) ? -1 : 1;
          } else {
            return (right?.confidence ?? 0) < (left?.confidence ?? 0) ? 1 : -1;
          }
        };
      }

    const sortedLabels = [...(image?.labels ?? [])]?.sort(sortLabels(false));

    const imgName = image?.id === undefined ? "" : image?.name;
    
    //const imgLabels = image?.id === undefined ? ["", ""] : ( ["Label: " + sortedLabels?.[0].label.toString() + "\n Confidence: " + sortedLabels?.[0].confidence + "\n Source: " + sortedLabels?.[0].source.toString(), ""] );

    return (

        <Modal isOpen={showImageModal}>

            <ModalHeader>
                <div>Image: {image?.name}</div>

                <i
                    className="fa fa-window-close"
                    onClick={() => {
                        let anchor = document.createElement("a");
                        anchor.style.display = "none";
                        anchor.click();
                        setShowImageModal(!showImageModal);
                    }}
                >
                </i>

            </ModalHeader>

            <ModalBody>
                <Image
                    tenantName={tenantName}
                    phoneNumber={phoneNumber}
                    imageName={imgName}
                    labels={[]}
                    id={
                    image?.id === undefined
                        ? "image"
                        : atob(image?.id).replace("\n", "")
                    }
                >
                </Image>
            </ModalBody>

            <ModalFooter>
                <strong>Labels:</strong> {sortedLabels?.[0].label}
                <br></br>
                <strong>Confidence:</strong> {sortedLabels?.[0].confidence}
                <br></br>
                <strong>Source:</strong> {sortedLabels?.[0].source}
            </ModalFooter>

        </Modal>
    );
};

export default ImageModal;