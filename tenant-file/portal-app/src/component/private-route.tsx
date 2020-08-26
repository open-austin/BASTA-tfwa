// A wrapper for <Route> that redirects to the login
import React from "react";
import { Route, Redirect, RouteProps } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { SignedInStatus } from "../store/auth";
import ClipLoader from "react-spinners/ClipLoader";

// screen if you're not yet authenticated.
const PrivateRoute: React.FC<RouteProps> = ({ children, ...rest }) => {
  const signedInStatus = useSelector(
    (state: RootState) => state.auth.signedInStatus
  );
  console.log("Signed In:", signedInStatus);
  return (
    <Route
      {...rest}
      render={({ location }) => {
        switch (signedInStatus) {
          case SignedInStatus.LoggedIn: {
            return children;
          }
          case SignedInStatus.LoggedOut: {
            return (
              <Redirect
                to={{
                  pathname: "/login",
                  state: { from: location },
                }}
              />
            );
          }
          case SignedInStatus.Unknown: {
            return <ClipLoader size={150} color={"#123abc"} loading={true} />;
          }
        }
      }}
    />
  );
};

export default PrivateRoute;
