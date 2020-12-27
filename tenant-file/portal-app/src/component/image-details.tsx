import { graphql } from 'graphql';
import React from 'react'
import {RouteComponentProps} from 'react-router-dom';
import ImageDetailsStyles from './styles/ImageDetailsStyles';
import { gql, useQuery } from '@apollo/client';

// Thanks to W3Schools for this example: https://www.w3schools.com/html/tryit.asp?filename=tryhtml_layout_float

// Might need to make a query to GraphQL to retrieve the image for the given ID.

const RETRIEVE_IMAGE = gql`
query RetrieveImage($id: Int!) {
    image(id: $id) {
      name
    }
}
`;

type TParams = {
    id: string;
}

const ImageDetails: React.FC<RouteComponentProps<TParams>> = ({ match }) => {

    const { loading, error, data } = useQuery(RETRIEVE_IMAGE, {
        variables: {
            id: match
        }
    });

    if (loading) return null;
    if (error) return `Error! ${error}`;



    /* retrieveImage({
        variables: {
            id: match
        }
    }); */

    return (
        <div>
            <style>
                {ImageDetailsStyles}
            </style>
            <section>
                This is the section.
                Image: 
                {/* {data.image.name} */}
                Labels:
                Image ID:
                {match.params.id}
                Date added:
                [Download button]
            </section>
            <aside>
                This is the aside.
                [Image here]
            </aside>
        </div>
    )
}

export default ImageDetails;