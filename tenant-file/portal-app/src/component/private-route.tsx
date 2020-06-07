// A wrapper for <Route> that redirects to the login
import React from "react";
import { Route, Redirect, RouteProps } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";

// screen if you're not yet authenticated.
const PrivateRoute: React.FC<RouteProps> = ({ children, ...rest }) => {
  const signedIn = useSelector((state: RootState) => state.auth.signedIn);
  console.log("Signed In:", signedIn);
  return (
    <Route
      {...rest}
      render={({ location }) =>
        signedIn ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
};

export default PrivateRoute;
