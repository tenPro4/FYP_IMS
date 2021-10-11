import React, { useState, useEffect } from 'react';
import { Route,Redirect} from "react-router-dom";
import Preloader from "components/Preloader";
import {connect} from 'react-redux';
import PropTypes from 'prop-types';
import { useSelector, useDispatch } from "react-redux";
import { fetchNewAccessJWT, setLogin } from "pages/login/LoginSlice";

const RouteWithLoader = ({ component: Component,auth,...rest }) => {
    const [loaded, setLoaded] = useState(false);
    const dispatch = useDispatch();
  
    return (
      <Route 
        {...rest} 
        render= {props => (
          // <Preloader show={loaded ? false : true} /> 
        (auth.isAuth)
        ? <Redirect to={{ pathname: '/'}} />
        :  <Component {...props} /> 
      )} />
    ); 
};

const mapStateToProps = (state) =>({
  auth:state.auth
});

RouteWithLoader.propTypes = {
  auth:PropTypes.object
};

export default connect(mapStateToProps)(RouteWithLoader);

