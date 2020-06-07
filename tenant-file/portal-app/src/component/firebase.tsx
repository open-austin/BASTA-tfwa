import React from "react";
import StyledFirebaseAuth from "react-firebaseui/StyledFirebaseAuth";
import firebase from "firebase";
import { setSignedIn, setUserInfo } from "../store/auth";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../store/store";
import { useHistory } from "react-router-dom";

const firebaseConfig = {
  apiKey: "AIzaSyB_r9GSDbaPbvrKb7tGSBB--Z37Z1LJJN4",
  authDomain: "tenant-file-fc6de.firebaseapp.com",
  databaseURL: "https://tenant-file-fc6de.firebaseio.com",
  projectId: "tenant-file-fc6de",
  storageBucket: "tenant-file-fc6de.appspot.com",
  messagingSenderId: "457573947987",
  appId: "1:457573947987:web:da3e4ae3fa7b535d4504a6",
  measurementId: "G-L20GR057G8",
};

// This must run before any other firebase functions
firebase.initializeApp(firebaseConfig);

export const useFirebaseAppInitialization = () => {
  const dispatch: AppDispatch = useDispatch();
  firebase.auth().onAuthStateChanged((user) => {
    if (user) {
      if (user) {
        dispatch(setSignedIn(true));
      }
    } else {
      console.log("NO USER");
    }
  });
};

export const getToken = async () => {
  return await firebase.auth().currentUser?.getIdToken();
};

// This adds firebaseui to the page
// It does everything else on its own
const FirebaseAuth = () => {
  const dispatch: AppDispatch = useDispatch();
  let history = useHistory();

  // This is our firebaseui configuration object
  const uiConfig: firebaseui.auth.Config = {
    signInSuccessUrl: "/signed-in",
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

        history.push("/signed-in");
        // I don't want to redirect because that causes a hard refresh
        return false;
      },
    },
  };

  return (
    <StyledFirebaseAuth uiConfig={uiConfig} firebaseAuth={firebase.auth()} />
  );
};

export default FirebaseAuth;
