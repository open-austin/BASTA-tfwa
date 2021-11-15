import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { ApolloClient, ApolloProvider, InMemoryCache } from "@apollo/client";

import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { store } from "./store/store";
import { ReactReduxFirebaseProvider } from "react-redux-firebase";
import { rrfProps } from "./component/firebase";
import { imageCartVar } from "./cache";

const client = new ApolloClient({
  uri: `${process.env.REACT_APP_API_URL}/graphql`,
  cache: new InMemoryCache({
    typePolicies: {
      imageCartVar: {
        fields: {
          imageCart: {
            read() {
              return imageCartVar();
            },
          },
        },
      },
      Query: {
        fields: {
          feed: {
            // Don't cache separate results based on
            // any of this field's arguments.
            keyArgs: false,
            // Concatenate the incoming list items with
            // the existing list items.
            merge(existing = [], incoming) {
              return [...existing, ...incoming];
            },
          },
        },
      },
    },
  }),
});

ReactDOM.render(
  <React.StrictMode>
    <ApolloProvider client={client}>
      <Provider store={store}>
        <ReactReduxFirebaseProvider {...rrfProps}>
          <BrowserRouter>
            <App />
          </BrowserRouter>
        </ReactReduxFirebaseProvider>
      </Provider>
    </ApolloProvider>
  </React.StrictMode>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
