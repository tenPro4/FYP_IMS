import React, { useState, useEffect } from 'react';
import Preloader from "components/Preloader";
import Sidebar from 'components/dashboard/Sidebar';
import Navbar from "components/dashboard/Navbar";
import Footer from "components/dashboard/Footer";
import { Redirect, Route, Switch } from 'react-router-dom';
import routes from 'routes';
import {connect,useDispatch, useSelector} from 'react-redux';
import CheckInDialog from "pages/attendance/CheckInDialog";
import {setCheckInDialogOpen} from "pages/attendance/AttendanceSlice";

const DefaultLayout = ({children}) => {
    const dispatch = useDispatch();
    const status = useSelector((state) => state.attendance.status);
    const [loaded, setLoaded] = useState(false);

    
    if(status !== null){
      if(status.current === undefined || status.current === 0){
          dispatch(setCheckInDialogOpen(true));
      }
  }

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
    <div>
        <Preloader show={loaded ? false : true} />
        <Sidebar />
        <main className="content">
          <Navbar />
          {children}
          <Footer toggleSettings={toggleSettings} showSettings={showSettings} />
          <CheckInDialog/>
        </main>
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.auth
});

export default connect(mapStateToProps)(DefaultLayout);