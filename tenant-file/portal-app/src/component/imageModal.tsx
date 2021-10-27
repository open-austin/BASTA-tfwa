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

    tenantName: string
    phoneNumber: string
    image: GetImagesForPhone_phone_images | null
    labels: string[]

    customFooterContent: any

    func: (open : boolean) => void
}

const ImageModal: React.FC<Props> = ({
    isOpen_param,
    customHeaderContent,

    // Body
    tenantName,
    phoneNumber,
    image,
    labels,

    customFooterContent,

    func
}: Props) => {

    const imgName = image?.id === undefined ? "" : image?.name;
    
    return (

        <Modal isOpen={isOpen_param}>

            <ModalHeader>
                <div>Image: {image?.name}</div>

                <i
                    className="fa fa-window-close"
                    onClick={() => {
                        func(!isOpen_param);
                    }}
                >
                </i>

            </ModalHeader>

            <ModalBody>
                <Image
                    tenantName={tenantName}
                    phoneNumber={phoneNumber}
                    imageName={imgName}
                    labels={labels}
                    id={
                    image?.id === undefined
                        ? "image"
                        : atob(image?.id).replace("\n", "")
                    }
                >
                </Image>
            </ModalBody>

            <ModalFooter>
                <p><strong>Labels:</strong> {labels?.[0]}</p>
                <p><strong>Confidence:</strong> {labels?.[1]}</p>
                <p><strong>Source:</strong> {labels?.[2]}</p>
            </ModalFooter>

        </Modal>
    );
};

export default ImageModal;