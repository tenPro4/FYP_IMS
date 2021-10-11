import React, {useState,useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleLeft, faEnvelope, faUnlockAlt } from "@fortawesome/free-solid-svg-icons";
import { faFacebookF, faGithub, faTwitter } from "@fortawesome/free-brands-svg-icons";
import { Col, Row, Form, Card, Button, FormCheck, Container, InputGroup,Alert } from '@themesberg/react-bootstrap';
import { Link } from 'react-router-dom';
import SEO from "components/SEO";
import * as Yup from 'yup';
import { Formik } from 'formik';
import { Routes } from "routes";
import BgImage from "assets/img/illustrations/signin.svg";
import { useDispatch, useSelector} from "react-redux";
import {login,clearErrors,authSelectors} from './LoginSlice';
import { useHistory,useLocation } from "react-router-dom";
import { fetchNewAccessJWT, setLogin } from "pages/login/LoginSlice";

export default () =>{
    const dispatch = useDispatch();
    const {isAuth} = useSelector( (state) => state.auth);
    const history = useHistory();
    let location = useLocation();
    let { from } = location.state || { from: { pathname: "/" } };

    return(
        <main>
            <SEO title="Sign In"/>
            <section className="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
                <Container>
                    <Row className="justify-content-center form-bg-image" style={{ backgroundImage: `url(${BgImage})` }}>
                        <Col xs={12} className="d-flex align-items-center justify-content-center">
                            <div className="bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                                <div className="text-center text-md-center mb-4 mt-md-0">
                                    <h3 className="mb-0">Sign in to our platform</h3>
                                </div>
                                <Formik
                                    initialValues={{
                                    email: 'demo@devias.io',
                                    password: 'Password123',
                                    remember:false
                                    }}
                                    validationSchema={
                                        Yup.object().shape({
                                            email: Yup.string().email('Must be a valid email').max(255).required('Email is required'),
                                            password: Yup.string().max(255).required('Password is required'),
                                    })}
                                    onSubmit={(values, { setSubmitting }) => {
                                        setTimeout(() => {
                                            dispatch(login(values));
                                            setSubmitting(false);
                                            }, 500);
                                    }}
                                >
                                    {({
                                    errors,
                                    handleBlur,
                                    handleChange,
                                    handleSubmit,
                                    isSubmitting,
                                    touched,
                                    values
                                    }) => (
                                    <form className="mt-4" onSubmit={handleSubmit}>
                                    <Form.Group id="email" className="mb-4">
                                        <Form.Label>Your Email</Form.Label>
                                            <InputGroup>
                                                <InputGroup.Text>
                                                    <FontAwesomeIcon icon={faEnvelope} />
                                                </InputGroup.Text>
                                                <Form.Control 
                                                    name="email"
                                                    type="email" 
                                                    placeholder="example@company.com"
                                                    required  
                                                    onBlur={handleBlur}
                                                    onChange={handleChange}
                                                    error={Boolean(touched.email && errors.email)}
                                                    value={values.email}
                                                 />
                                            </InputGroup>
                                            {errors.email && (
                                                <Alert variant="danger">
                                                {errors.email}
                                                </Alert>
                                            )}
                                    </Form.Group>
                                    <Form.Group>
                                        <Form.Group id="password" className="mb-4">
                                            <Form.Label>Password</Form.Label>
                                            <InputGroup>
                                                <InputGroup.Text>
                                                    <FontAwesomeIcon icon={faUnlockAlt} />
                                                </InputGroup.Text>
                                                <Form.Control
                                                    error={Boolean(touched.password && errors.password)}
                                                    name="password"
                                                    onBlur={handleBlur}
                                                    onChange={handleChange}
                                                    type="password"
                                                    value={values.password}
                                                    placeholder="Password"
                                                    required
                                                />
                                            </InputGroup>
                                        </Form.Group>
                                            <div className="d-flex justify-content-between align-items-center mb-4">
                                                <Form.Check type="checkbox">
                                                    <FormCheck.Input 
                                                        id="defaultCheck5" 
                                                        className="me-2" 
                                                        name="remember"
                                                        onChange={handleChange}
                                                        />
                                                    <FormCheck.Label htmlFor="defaultCheck5" className="mb-0">Remember me</FormCheck.Label>
                                                </Form.Check>
                                                <Card.Link as={Link} to={Routes.ForgotPassword.path} className="small text-end">Lost password?</Card.Link>
                                            </div>
                                    </Form.Group>
                                    <Button disabled={isSubmitting} variant="primary" type="submit" className="w-100">
                                        Sign in
                                    </Button>
                                    </form>
                                    )}
                                </Formik>
                                <div className="mt-3 mb-4 text-center">
                                    <span className="fw-normal">or login with</span>
                                    </div>
                                    <div className="d-flex justify-content-center my-4">
                                    <Button variant="outline-light" className="btn-icon-only btn-pill text-facebook me-2">
                                        <FontAwesomeIcon icon={faFacebookF} />
                                    </Button>
                                    <Button variant="outline-light" className="btn-icon-only btn-pill text-twitter me-2">
                                        <FontAwesomeIcon icon={faTwitter} />
                                    </Button>
                                    <Button variant="outline-light" className="btn-icon-only btn-pil text-dark">
                                        <FontAwesomeIcon icon={faGithub} />
                                    </Button>
                                </div>
                                <div className="d-flex justify-content-center align-items-center mt-4">
                                    <span className="fw-normal">
                                        Not registered?
                                        <Card.Link as={Link} to={Routes.Register.path} className="fw-bold">
                                        {` Create account `}
                                        </Card.Link>
                                    </span>
                                </div>
                            </div>
                        </Col>
                    </Row>
                </Container>
            </section>
        </main>
    );
}