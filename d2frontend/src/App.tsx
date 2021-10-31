
import RuneFileViewer from './components/RuneFileViewer';
import AdminComponent from './components/AdminComponent';
import React from 'react';
import { Route } from "react-router";
import { ROUTE } from './models/Routes';
import RunewordComponent from './components/RunewordComponent';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <div>
      <Route path={ROUTE.HOME}>
        <RunewordComponent />
      </Route>
      <Route path={ROUTE.ADMIN}>
        <AdminComponent />
      </Route>
      <ToastContainer />
    </div>
  );
}

export default App;
