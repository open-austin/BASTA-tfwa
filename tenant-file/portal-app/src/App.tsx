import React from 'react';

import 'bootstrap/dist/css/bootstrap.min.css';
import '@fortawesome/fontawesome-free/css/all.min.css';
import { Switch, Route } from 'react-router-dom';
import Login from './component/login';
import Layout from './component/layout';
import PrivateRoute from './component/private-route';
import DisplayImages from './component/display-images';
import Admin from './component/admin';
import Dashboard from './component/dashboard';
import Properties from './component/properties';

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
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/properties">
          <Properties />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
