import React from 'react';

import 'bootstrap/dist/css/bootstrap.min.css';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'line-awesome/dist/line-awesome/css/line-awesome.min.css';
import { Switch, Route, Redirect } from 'react-router-dom';
import Login from './component/login';
import Layout from './component/layout';
import PrivateRoute from './component/private-route';
import DisplayImages from './component/display-images';
import Admin from './component/admin';
import Dashboard from './component/dashboard';
import Properties from './component/properties';
import Home from './component/home';
import AddTenant from "./component/add-tenant";

function App() {
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
        <PrivateRoute path="/dashboard">
          <Dashboard />
        </PrivateRoute>
        <PrivateRoute path="/add-tenant">
          <AddTenant />
        </PrivateRoute>
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/properties">
          <Properties />
        </Route>
        <Route path="/home">
          <Home />
        </Route>
        <Route exact path="/">
          <Redirect to="/home" />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
