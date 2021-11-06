
import AdminComponent from './components/AdminComponent';
import { Route } from "react-router";
import { ROUTE } from './models/Routes';
import RunewordSerchComponent from './components/RunewordSerchComponent';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <div>
      <Route exact path={ROUTE.HOME}>
        <RunewordSerchComponent />
      </Route>
      <Route path={ROUTE.ADMIN}>
        <AdminComponent />
      </Route>
      <ToastContainer />
    </div>
  );
}

export default App;
