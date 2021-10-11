import "./styles.css";
import React, { useState } from "react";

import { Jumbotron, Button } from "react-bootstrap";
import { toast } from "react-toastify";
import SEO from "components/SEO";
import { useSelector, useDispatch } from "react-redux";
import { useHistory,useLocation } from "react-router-dom";

export const Entry = () => {
  const {isAuth} = useSelector( (state) => state.auth);
  const history = useHistory();

  const handleClick = () => {
    console.log("click toast");
    toast.info("just test");
  };  

  return (
    <>
      <SEO title="Entry" />
      <div className="entry-page bg-info">
        <Jumbotron className="form-box">
          <h2>Entry Page</h2>
          <Button onClick={handleClick}>click test toast</Button>
        </Jumbotron>
      </div>
    </>
  );
};
