import React, { useState, useEffect } from "react";
import Preloader from "components/Preloader";
import {
  Col,
  Row,
  Form,
  Card,
  Button,
  FormCheck,
  Container,
  InputGroup,
  Modal,
  Alert,
} from "@themesberg/react-bootstrap";
import { Routes } from "routes";
import { Link, useLocation, useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { verifyEmail,setVerify } from "./LoginSlice";

const EmailVerify = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const history = useHistory();
  const verify = useSelector((state) => state.auth.verify);
  const loading = useSelector((state) => state.auth.loading);
  const queryParams = new URLSearchParams(location.search);
  const token = queryParams.get("token");
  const email = queryParams.get("email");

  useEffect(() => {
    const param = {
      token,
      email,
    };
    // dispatch(verifyEmail(param));
    dispatch(setVerify(false));
  }, []);

  useEffect(() => {
    if (verify === true) {
      setTimeout(() => {
        history.push(Routes.Login.path);
      }, 5000);
    }
  }, [verify]);

  if (loading) {
    <Preloader show={loading} />;
  }

  return (
    <div className="d-flex justify-content-center my-5 mt-lg-6 mb-lg-5">
      {verify === true ? (
        <Alert variant="success" className="text-center">
          Email validate success. Your page will back to Login page in 5
          seconds.
          <a href={Routes.Login.path}>If not please click here.</a>
        </Alert>
      ) : (
        <Alert variant="danger" className="text-center">
          Oops!Seem something wrong.Please ensure your verification link is
          correct.
          <a href={Routes.ResendEmail.path}>Click here to gain new verify email.</a>
        </Alert>
      )}
    </div>
  );
};

export default EmailVerify;
