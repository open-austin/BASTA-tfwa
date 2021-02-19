// A wrapper for <Route> that redirects to the login
import React from "react";
import { Route, Redirect, RouteProps } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import ClipLoader from "react-spinners/ClipLoader";

// screen if you're not yet authenticated.
const PrivateRoute: React.FC<RouteProps> = ({ children, ...rest }) => {
  const profile = useSelector((state: RootState) => state.firebase.profile);
  return (
    <Route
      {...rest}
      render={({ location }) => {
        if (!profile.isLoaded) {
          return <ClipLoader size={150} color={"#123abc"} loading={true} />;
        } else if (!profile.isEmpty) {
          return children;
        } else {
          return (
            <Redirect
              to={{
                pathname: "/login",
                state: { from: location },
              }}
            />
          );
        }
      }}
    />
  );
};

export default PrivateRoute;
