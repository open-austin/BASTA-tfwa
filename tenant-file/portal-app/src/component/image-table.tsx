import React from "react";
import {} from "module";
import "@firebase/storage";
import { gql, useQuery } from "@apollo/client";
import styles from "./tenant-details.module.css";
import {
  GetImagesForPhone,
  // GetImagesForPhone_phone_images,
  // GetImagesForPhone_phone_images_labels,
} from "../types/GetImagesForPhone";
// import "@google.picker";

// import GooglePicker from "react-google-picker";
import ImageTableCollapse from "./image-table-collapse";
import { useState } from "react";
// import { Label } from "reactstrap";

type Props = {
  phoneId: string;
  tenantName: string;
  phoneNumber: string;
};

const ImageTable: React.FC<Props> = ({
  phoneId,
  tenantName,
  phoneNumber,
}: Props) => {
  const GET_IMAGES_FOR_PHONE = gql`
    query GetImagesForPhone($id: ID!, $limit: Int = 5, $cursor: String = null) {
      phone(id: $id) {
        images(first: $limit, after: $cursor) {
          pageInfo {
            endCursor
            hasNextPage
            hasPreviousPage
            startCursor
          }
          edges {
            node {
              id
              name
              thumbnailName
              labels {
                label
                source
                confidence
              }
            }
          }
        }
      }
    }
  `;

  //const storage = firebase.app().storage();
  // const sortLabels = (
  //   images: GetImagesForPhone_phone_images[]
  // ): GetImagesForPhone_phone_images_labels[] | undefined => {
  //   return images
  //     ?.map((image) => image?.labels)
  //     .sort(
  //       (left, right) => (left?.confidence ?? 0) + (right?.confidence ?? 0)
  //     );
  // };
  const { data, error, loading, fetchMore } = useQuery<GetImagesForPhone>(
    GET_IMAGES_FOR_PHONE,
    {
      variables: {
        id: phoneId,
      },
    }
  );

  const handleScroll = (e: any) => {
    console.log(`HandleScroll`);
    //this needs to "add on" to the table and now rerender it. An independent component that addes new rows?
    const bottom =
      e.target.scrollHeight - e.target.scrollTop === e.target.clientHeight;
    const lastItem = data?.phone?.images?.pageInfo?.endCursor;
    const hasMoreItems = data?.phone?.images?.pageInfo?.hasNextPage;
    if (bottom && hasMoreItems) {
      console.log(`bottom hit`);
      fetchMore({
        variables: {
          // name: nameQuery,
          limit: 5,
          cursor: lastItem,
        },
        updateQuery: (previous, { fetchMoreResult }) => {
          if (fetchMoreResult!.phone!.images!.edges!.length > 0) {
            fetchMoreResult!.phone!.images!.edges = [
              ...(previous.phone?.images?.edges ?? []),
              ...(fetchMoreResult?.phone?.images?.edges ?? []),
            ];
            return fetchMoreResult!;
          } else {
            return previous!;
          }
        },
      });
    }
  };

  const [searchTerm, setSearchTerm] = useState("");
  const renderRows = () => {
    return data?.phone?.images?.edges
      ?.filter(
        (edge) =>
          searchTerm.length === 0 ||
          edge?.node?.labels?.some((l) =>
            l.label.toLowerCase().includes(searchTerm.toLowerCase(), 0)
          )
      )
      .map((edge, index) => {
        return (
          <>
            <ImageTableCollapse
              id={
                edge?.node?.id === undefined
                  ? "image"
                  : atob(edge?.node?.id).replace("\n", "")
              }
              phoneNumber={phoneNumber}
              tenantName={tenantName}
              image={edge.node}
              index={index}
            ></ImageTableCollapse>
          </>
        );
      });
  };
  if (loading) return <p>Loading...</p>;
  if (error) return <p>{` ERROR:  ${error.message}`}</p>;

  return (
    <div className="container-fluid">
      <div className="card shadow">
        <div className={styles.cardHeader}>
          <div className="d-flex">
            <h4 className="m-0 font-weight-bold mr-3">Images</h4>
{/* 
            <input
              className={styles.bastaBtnSm}
              id="myFileInput"
              type="file"
              accept="image/*"
              capture="camera"
              multiple
            /> */}
            <div
              className="text-md-right dataTables_filter"
              id="dataTable_filter"
            >
              <label>
                <input
                  onChange={(e) => setSearchTerm(e.currentTarget.value)}
                  type="search"
                  className="form-control"
                  aria-controls="dataTable"
                  placeholder="Search by label"
                />
              </label>
            </div>
          </div>
        </div>
        <div className="card-body">
          <div className="row">
            <div className="col-md-6 text-nowrap">
              <div
                id="dataTable_length"
                className="dataTables_length"
                aria-controls="dataTable"
              >
                {/* <label>
                  Show&nbsp;
                  <select className="form-control form-control-sm custom-select custom-select-sm">
                    <option value={10} selected>
                      10
                    </option>
                    <option value={25}>25</option>
                    <option value={50}>50</option>
                    <option value={100}>100</option>
                  </select>
                </label> */}
              </div>
            </div>
            <div className="col-md-6"></div>
          </div>
          <div
            onScroll={handleScroll}
            className={styles.scrollFeed}
            id="dataTable"
            role="grid"
            aria-describedby="dataTable_info"
          >
            <table className="table my-0" id="dataTable">
              <thead>
                <tr>
                  <th>Image</th>
                  <th>Label</th>
                  <th>Source</th>
                  <th>Confidnece</th>
                  <th>Date Received</th>
                </tr>
              </thead>
              <tbody>{renderRows()}</tbody>
              <tfoot>
                <tr>
                  <td>
                    <strong>Image</strong>
                  </td>
                  <td>
                    <strong>Label</strong>
                  </td>
                  <td>
                    <strong>Source</strong>
                  </td>
                  <td>
                    <strong>Confidence</strong>
                  </td>
                  <td>
                    <strong>Date Received</strong>
                  </td>
                </tr>
              </tfoot>
            </table>
          </div>
          {/* <div className="row">
            <div className="col-md-6 align-self-center">
              <p
                id="dataTable_info"
                className="dataTables_info"
                role="status"
                aria-live="polite"
              ></p>
            </div>
            <div className="col-md-6">
              <nav className="d-lg-flex justify-content-lg-end dataTables_paginate paging_simple_numbers">
                <ul className="pagination">
                  <li className="page-item disabled">
                    <a className="page-link" href="#" aria-label="Previous">
                      <span aria-hidden="true">«</span>
                    </a>
                  </li>
                  <li className="page-item active">
                    <a className="page-link" href="#">
                      1
                    </a>
                  </li>
                  <li className="page-item">
                    <a className="page-link" href="#">
                      2
                    </a>
                  </li>
                  <li className="page-item">
                    <a className="page-link" href="#">
                      3
                    </a>
                  </li>
                  <li className="page-item">
                    <a className="page-link" href="#" aria-label="Next">
                      <span aria-hidden="true">»</span>
                    </a>
                  </li>
                </ul>
              </nav>
            </div>
          </div> */}
        </div>
      </div>
    </div>
  );
};
export default ImageTable;
