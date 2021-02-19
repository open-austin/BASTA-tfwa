import React from "react";
import StyledFirebaseAuth from "react-firebaseui/StyledFirebaseAuth";
import firebase from "firebase";
import { setSignedIn, setUserInfo } from "../store/auth";
import { useDispatch } from "react-redux";
import { AppDispatch, store } from "../store/store";
import {
  ReactReduxFirebaseConfig,
  ReactReduxFirebaseProviderProps,
} from "react-redux-firebase";

const firebaseConfig = {
  apiKey: process.env.REACT_APP_FIREBASE_API_KEY,
  authDomain: `${process.env.REACT_APP_PROJECT_ID}.firebaseapp.com`,
  databaseURL: `https://${process.env.REACT_APP_PROJECT_ID}.firebaseio.com`,
  projectId: process.env.REACT_APP_PROJECT_ID,
  storageBucket: `${process.env.REACT_APP_PROJECT_ID}.appspot.com`,
  messagingSenderId: process.env.REACT_APP_FIREBASE_MESSAGING_SENDER_ID,
  appId: process.env.REACT_APP_FIREBASE_APP_ID,
  measurementId: process.env.REACT_APP_GA_MEASUREMENT_ID,
};
firebase.initializeApp(firebaseConfig);

const rrfConfig: Partial<ReactReduxFirebaseConfig> = {
  userProfile: "users",
  enableClaims: true,
};

export const rrfProps: ReactReduxFirebaseProviderProps = {
  firebase,
  config: rrfConfig,
  dispatch: store.dispatch,
};

export const getToken = async () => {
  return await firebase.auth().currentUser?.getIdToken();
};

// This adds firebaseui to the page
// It does everything else on its own
const FirebaseAuth = () => {
  const dispatch: AppDispatch = useDispatch();

  // This is our firebaseui configuration object
  const uiConfig: firebaseui.auth.Config = {
    signInSuccessUrl: "/dashboard",
    signInFlow: "redirect",
    credentialHelper: "none",
    signInOptions: [
      firebase.auth.EmailAuthProvider.PROVIDER_ID,
      firebase.auth.GoogleAuthProvider.PROVIDER_ID,
    ],
    callbacks: {
      signInSuccessWithAuthResult: (authResult: any, redirectUrl: string) => {
        dispatch(setSignedIn(true));
        const user = firebase.auth().currentUser;
        if (user?.email) {
          user?.getIdTokenResult().then((token) => {
            dispatch(
              setUserInfo({
                admin: token.claims.admin,
                email: token.claims.email,
              })
            );
          });
        }

        console.log("signInSuccessWithAuthResult", authResult, redirectUrl);
        return true;
      },
    },
  };

  return (
    <StyledFirebaseAuth uiConfig={uiConfig} firebaseAuth={firebase.auth()} />
  );
};

export default FirebaseAuth;
