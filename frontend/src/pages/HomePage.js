import React, { useState, useEffect } from "react";
import {
  BrowserRouter as Router,
  Route,
  Switch,
  Redirect,
} from "react-router-dom";
import PrivateRoute from "components/route/PrivateRoute";
import RouteWithLoader from "components/route/RouteWithLoader";
import RouteWithSidebar from "components/route/RouteWithSidebar";
import { Routes } from "routes";
import NotFound from "pages/NotFound";
import { Entry } from "./entry/Entry";
import { Test } from "./Test";
import Register from "pages/register/register";
import ForgotPassword from "pages/forgot-password/ForgotPassword";
import ResetPassword from "pages/reset-password/ResetPassword";
import BoardList from "pages/board/BoardList";
import Board from "./board/Board";
import BoardBar from "./board/BoardBar";
import BoardContent from "./board/BoardContent";
import EmailValidate from "./EmailValidation";
import ResendEmail from "./ResendVerificationEmail";
import DashBoardLayout from "pages/DefaultLayout";
import Loadable from "react-loadable";
import Loading from "shared/components/Loading";
import { useSelector, useDispatch } from "react-redux";
import { fetchNewAccessJWT, setLogin } from "pages/login/LoginSlice";
import Profile from 'pages/Profile/Profile';
import ProfileEdit from 'pages/ProfileEdit/ProfileEdit';
import PasswordEdit from 'pages/ProfileEdit/PasswordEdit';
import AddressEdit from 'pages/ProfileEdit/AddressEdit';
import {fetchUserDetail} from 'pages/Profile/ProfileSlice';
import AttendanceList from 'pages/attendance/AttendanceList';
import Demo from 'pages/attendance/Demo';
import {fetchAttendanceStatus} from "pages/attendance/AttendanceSlice";
import Overview from 'pages/overview/Overview';
import LeaveList from 'pages/leave/LeaveList';
import Employee from "pages/employee/Employee";
import EmailVerify from 'pages/login/EmailVerify';
import EventCalendar from 'pages/calendar/Calendar';

const login = Loadable({
  loader: () => import("pages/login/login"),
  loading: Loading,
});

export default () => {
  const dispatch = useDispatch();
  const auth = useSelector((state) => state.auth);
  useEffect(() => {
    !sessionStorage.getItem("accessJWT") &&
      localStorage.getItem("ims") &&
      dispatch(fetchNewAccessJWT(localStorage.getItem("ims")));
    !auth.isAuth && sessionStorage.getItem("accessJWT") && dispatch(setLogin());

    auth.isAuth && dispatch(fetchAttendanceStatus());

    dispatch(fetchUserDetail());
  }, [dispatch, auth]);

  return (
    <Router>
      <Switch>
        <RouteWithLoader exact path={Routes.Test.path} component={Test}/>
        <RouteWithLoader exact path={Routes.Entry.path} component={Entry}/>
        <RouteWithLoader exact path={Routes.ResendEmail.path} component={ResendEmail}/>
        <RouteWithLoader exact path={Routes.Login.path} component={login} />
        <RouteWithLoader exact path={Routes.EmailVerify.path} component={EmailVerify}/>
        
        <RouteWithLoader exact path={Routes.Register.path} component={Register}/>
        <RouteWithLoader exact path={Routes.EmailValidation.path} component={EmailValidate}/>
        <RouteWithLoader exact path={Routes.ForgotPassword.path} component={ForgotPassword}/>
        <RouteWithLoader exact path={Routes.ResetPassword.path} component={ResetPassword}/>
        
        <PrivateRoute exact path={Routes.PasswordEdit.path} component={PasswordEdit} />
        <PrivateRoute exact path={Routes.ProjectBoardList.path} component={BoardList} />
        <PrivateRoute exact path={Routes.ProjectBoard.path} component={Board} />
        <PrivateRoute exact path={Routes.Profile.path} component={Profile} />
        <PrivateRoute exact path={Routes.ProfileEdit.path} component={ProfileEdit} />
        <PrivateRoute exact path={Routes.AddressEdit.path} component={AddressEdit} />
        <PrivateRoute exact path={Routes.Attendances.path} component={AttendanceList} />
        <PrivateRoute exact path={Routes.Leaves.path} component={LeaveList} />
        <PrivateRoute exact path={Routes.Overview.path} component={Overview} />
        <PrivateRoute exact path={Routes.Employees.path} component={Employee}/>
        <PrivateRoute exact path={Routes.EventCalendar.path} component={EventCalendar}/>
        <Route exact path={Routes.NotFound.path} component={NotFound}/>
      </Switch>
    </Router>
  );
};
