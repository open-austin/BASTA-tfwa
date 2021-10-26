// Credit to:
// https://stackoverflow.com/questions/55834987/in-typescript-why-is-jsx-intrinsicelements-not-working-as-documentation-describ/55835142
// https://stackoverflow.com/questions/66207765/react-typescript-expects-at-least-3-arguments-but-the-jsx-factory-react-cr

import styles from "./imageModalStyling.module.css";

import React, { useState } from 'react';

import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

type Props = {
    isOpen_param: boolean
    customHeaderContent: any
    customBodyContent: any
    customFooterContent: any
}

const ImageModal: React.FC<Props> = ({
    isOpen_param,
    customHeaderContent,
    customBodyContent,
    customFooterContent
}: Props) => {

    const [showImageModal, setShowImageModal] = useState(isOpen_param);
    
    return (
        <Modal isOpen={showImageModal}>

            <ModalHeader>
                {customHeaderContent}

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
                {customBodyContent}
            </ModalBody>

            <ModalFooter>
                {customFooterContent}
            </ModalFooter>

        </Modal>
    );
};

export default ImageModal;