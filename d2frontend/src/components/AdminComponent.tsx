import React, { useEffect } from "react";
import { useState } from "react";
import { Rune } from "../models/rune";
import httpcommon from "../util/httpcommon";
import RunelistComponent from "./RunelistComponent";




const AdminComponent: React.FC = () => {
  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-3">
          <h1>Runes</h1>
          <RunelistComponent chunksize={5} />
        </div>
        <div className="col-9">
          <h1>Middle</h1>
        </div>
      </div>
    </div>
  );
}

export default AdminComponent;
