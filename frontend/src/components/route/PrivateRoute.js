import React, { useEffect } from "react";
import { Redirect, Route } from "react-router-dom";
import { Entry } from "pages/entry/Entry";
import { Routes } from "routes";
import { RouteWithLoader } from "components/route/RouteWithLoader";
import { useSelector, useDispatch } from "react-redux";
import { fetchNewAccessJWT, setLogin } from "pages/login/LoginSlice";
import DashBoardLayout from 'pages/DefaultLayout';

const PrivateRoute = ({ component: Component, ...rest }) => {
  const {isAuth} = useSelector( (state) => state.auth);
  const dispatch = useDispatch();

  !isAuth && sessionStorage.getItem("accessJWT") && dispatch(setLogin());

  console.log(isAuth);
  return (
    <Route
      {...rest}
      render={props => (
        (isAuth) ?  
		<DashBoardLayout><Component {...props}/></DashBoardLayout>
		: <Redirect to="/login" />
      )}
    />
  );
};

export default PrivateRoute;
