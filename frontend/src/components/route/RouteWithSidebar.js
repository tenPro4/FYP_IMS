import React, { useState, useEffect } from 'react';
import { Route} from "react-router-dom";
import Preloader from "components/Preloader";
import Sidebar from 'components/dashboard/Sidebar';
import Navbar from "components/dashboard/Navbar";
import Footer from "components/dashboard/Footer";
import { useHistory,useLocation } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import {fetchNewAccessJWT,setLogin} from 'pages/login/LoginSlice';
import { connect } from "react-redux";
import PropTypes from "prop-types";

const RouteWithSidebar = ({ component: Component,auth,...rest }) => {
  const [loaded, setLoaded] = useState(false);
  const history = useHistory();
  const dispatch = useDispatch();
  console.log(auth);

  useEffect(() => {
    const timer = setTimeout(() => setLoaded(true), 1000);
    return () => clearTimeout(timer);
  }, []);

  const localStorageIsSettingsVisible = () => {
    return localStorage.getItem('settingsVisible') === 'false' ? false : true
  }

  const [showSettings, setShowSettings] = useState(localStorageIsSettingsVisible);

  const toggleSettings = () => {
    setShowSettings(!showSettings);
    localStorage.setItem('settingsVisible', !showSettings);
  }

  return (
    <Route {...rest} render={props => (
      <>
        <Preloader show={loaded ? false : true} />
        <Sidebar />
        <main className="content">
          <Navbar />
          <Component {...props} />
          <Footer toggleSettings={toggleSettings} showSettings={showSettings} />
        </main>
      </>
    )}
    />
  );
};

const mapStateToProps = state => ({
  auth: state.auth
});

RouteWithSidebar.propTypes = {
auth: PropTypes.object
};

export default connect(mapStateToProps)(RouteWithSidebar);