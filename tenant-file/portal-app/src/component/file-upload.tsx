import React, { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import styled from 'styled-components';

const StyledFileUpload = styled.div`
  border: 8px dashed grey;
  width: 80%;
  margin: 0 auto;
  height: 80%;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;

  &.active {
    border: 8px dashed ${(props) => props.theme.darkSecondary};
    background-color: rgba(0, 0, 0, 0.2);
  }

  .las {
    font-size: 5rem;
  }
`;

// Posssible query shape
// const UPLOAD_FILE = gql`
//   mutation SingleUpload($file: Upload!) {
//     singleUpload(file: $file) {
//       filename
//       mimetype
//       encoding
//     }
//   }
// `;

const FileUpload = () => {
  // state for file upload below
  // const [uploadFile, { data }] = useMutation(UPLOAD_FILE);
  const onDrop = useCallback((acceptedFiles) => {
    // Do something with the files
    // uploadFile(acceptedFiles);
    console.log(acceptedFiles);
    window.alert(`uploading ${acceptedFiles[0].name}`);
  }, []);

  const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

  return (
    <>
      <StyledFileUpload
        className={isDragActive ? 'active' : ''}
        {...getRootProps()}
      >
        <input {...getInputProps()} />
        <i className="las la-cloud-upload-alt"></i>
        <p>Drag n Drop or Click to Upload</p>
      </StyledFileUpload>
    </>
  );
};

export default FileUpload;
