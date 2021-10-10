import React from "react";
import {
  GetImagesForPhone_phone_images,
  GetImagesForPhone_phone_images_labels,
} from "../types/GetImagesForPhone";
import { useState } from "react";
import Image from "./image";
import styles from "./image-table-collapse.module.css";
import { Collapse, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";

type Props = {
  image: GetImagesForPhone_phone_images | null;
  tenantName: string;
  phoneNumber: string;
  id: string | undefined;
  index: number;
};
const ImageTableCollapse: React.FC<Props> = ({
  index,
  id,
  tenantName,
  phoneNumber,
  image,
}: Props) => {
  const [isOpen, setOpen] = useState(false);

  const [showImageModal, setShowImageModal] = useState(false);

  //   const collapseHidden = !isOpen ? styles.collapseRow : styles.showRow;
  //   const collapseHidden = !isOpen ? styles.collapsedTd : styles.showTd;
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
  return (
    <>

      <Modal isOpen={showImageModal}>

        <ModalHeader>
          This is the Modal Header.
        </ModalHeader>

        <ModalBody>
          This is the Modal Body.

          <Image
             tenantName={tenantName}
             phoneNumber={phoneNumber}
            imageName={image?.id === undefined ? "" : image?.thumbnailName}
            labels={
              image?.id === undefined
                ? ["", ""]
                : ([
                    sortedLabels?.[0].label,
                    sortedLabels?.[0].confidence,
                    sortedLabels?.[0].source,
                  ] as string[])
            }
            id={
              image?.id === undefined
                ? "image"
                : atob(image?.id).replace("\n", "")
            }
          ></Image>

        </ModalBody>

        <ModalFooter>
          This is the Modal Footer.  Click <button
            className="btn btn-sm btn-link"
            onClick={() => {
              let anchor = document.createElement("a");
              anchor.style.display = "none";
              anchor.click();
              setShowImageModal(!showImageModal);
            }}
            >
            here
          </button> to close.
        </ModalFooter>

      </Modal>
     
      {" "}
      <tr key={`image${index}`}>
        <td onClick={() => setShowImageModal(!showImageModal)}>
          <Image
             tenantName={tenantName}
             phoneNumber={phoneNumber}
            imageName={image?.id === undefined ? "" : image?.thumbnailName}
            labels={
              image?.id === undefined
                ? ["", ""]
                : ([
                    sortedLabels?.[0].label,
                    sortedLabels?.[0].confidence,
                    sortedLabels?.[0].source,
                  ] as string[])
            }
            id={
              image?.id === undefined
                ? "image"
                : atob(image?.id).replace("\n", "")
            }
          ></Image>
        </td>

        <td onClick={() => setOpen(!isOpen)}>{sortedLabels?.[0].label}</td>
        <td onClick={() => setOpen(!isOpen)}>{sortedLabels?.[0].source}</td>
        <td onClick={() => setOpen(!isOpen)}>
          {((sortedLabels[0]?.confidence ?? 0) * 100)?.toFixed(1) + "%"}
        </td>
        <td></td>
      </tr>
      <tr>
        <td className={styles.collapsedTd} colSpan={5}>
          <div className="table-responsive">
            <table className="table-sm" role="grid" id="dataTable">
              <tbody>
                <Collapse onDoubleClick={() => setOpen(false)} isOpen={isOpen}>
                  {sortedLabels?.map((label) => (
                    <tr>
                      <td></td>
                      <td>{label?.label}</td>
                      <td>{label?.source}</td>
                      <td>
                        {((label?.confidence ?? 0) * 100)?.toFixed(1) + "%"}
                      </td>
                      <td></td>
                    </tr>
                  ))}
                </Collapse>
              </tbody>
            </table>
          </div>
        </td>
      </tr>
    </>
  );
};

export default ImageTableCollapse;
