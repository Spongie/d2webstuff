
import RuneFileViewer from './components/RuneFileViewer';
import AdminComponent from './components/AdminComponent';
import React from 'react';
import { Route } from "react-router";
import { ROUTE } from './models/Routes';
import RunewordComponent from './components/RunewordComponent';


function App() {
  return (
    <div>
      <Route path={ROUTE.HOME}>
        <RunewordComponent />
      </Route>
      <Route path={ROUTE.ADMIN}>
        <AdminComponent />
      </Route>
    </div>
  );
}

export default App;
