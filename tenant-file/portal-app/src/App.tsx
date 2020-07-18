import React from "react";

import { useFirebaseAppInitialization } from "./component/firebase";

import "bootstrap/dist/css/bootstrap.min.css";
import { Switch, Route } from "react-router-dom";
import Login from "./component/login";
import Layout from "./component/layout";
import PrivateRoute from "./component/private-route";
import DisplayImages from "./component/display-images";
import Admin from "./component/admin";

// import { Server } from "miragejs";

// new Server({
//   routes() {
//     this.namespace = "/api";

//     this.get("/images", () => {
//       return {
//         images: ["test", "test2"],
//       };
//     });
//   },
// });

function App() {
  useFirebaseAppInitialization();
  return (
    <Layout>
      <Switch>
        <PrivateRoute path="/signed-in">
          <p>SIGNED IN</p>
          <DisplayImages />
        </PrivateRoute>
        <PrivateRoute path="/admin">
          <Admin />
        </PrivateRoute>
        <Route path="/login">
          <Login />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
