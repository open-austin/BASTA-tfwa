import { graphql } from 'graphql';
import React from 'react'
import {RouteComponentProps} from 'react-router-dom';
import ImageDetailsStyles from './styles/ImageDetailsStyles';
import { gql, selectHttpOptionsAndBody, useQuery } from '@apollo/client';

// Thanks to W3Schools for this example: https://www.w3schools.com/html/tryit.asp?filename=tryhtml_layout_float

// Might need to make a query to GraphQL to retrieve the image for the given ID.


/* query RetrieveImage($id: Int!) {
    image(id: $id) {
      name
    }
} */

const RETRIEVE_IMAGE = gql`
  query somethingelse($id: Int!) {
    tenant(id: $id) {
        name
    }
  }
`;

type TParams = {
    id: string;
}

const ImageDetails: React.FC<RouteComponentProps<TParams>> = ({ match }) => {

    console.log("Match ID is " + match.params.id);

    const { loading, error, data } = useQuery(RETRIEVE_IMAGE, {
        variables: {
            id: parseInt(match.params.id)
        }
    });

    /* if (loading) {
        setTimeout(console.log("Data is " + data), 3000);
    } */

    if (!loading) {
        console.log("Data done loading and value is " + data.tenant.name);
    }

    /* if (loading) return null;
    if (error) return `Error! ${error}`; */



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
                {data.tenant.name}
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