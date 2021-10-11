import React, { useState,useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleLeft,
  faEnvelope,
  faUnlockAlt,
  faUser,
} from "@fortawesome/free-solid-svg-icons";
import {
  faFacebookF,
  faGithub,
  faTwitter,
} from "@fortawesome/free-brands-svg-icons";
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
import { Link } from "react-router-dom";

import { Routes } from "routes";
import BgImage from "assets/img/illustrations/signin.svg";
import * as Yup from "yup";
import { Formik } from "formik";
import SEO from "components/SEO";
import {register} from 'pages/login/LoginSlice';
import { useDispatch, useSelector} from "react-redux";


export default () => {
  const [showTerm, setShowTerm] = useState(false);
  const handleCloseModal = () => setShowTerm(false);
  const dispatch = useDispatch();
  const {registerErrors,loading} = useSelector( (state) => state.auth);

  return (
    <main>
      <Modal
        as={Modal.Dialog}
        centered
        show={showTerm}
        onHide={handleCloseModal}
      >
        <Modal.Header>
          <Modal.Title className="h6">Terms of Service</Modal.Title>
          <Button
            variant="close"
            aria-label="Close"
            onClick={handleCloseModal}
          />
        </Modal.Header>
        <Modal.Body>
          <p>
            With less than a month to go before the European Union enacts new
            consumer privacy laws for its citizens, companies around the world
            are updating their terms of service agreements to comply.
          </p>
          <p>
            The European Union’s General Data Protection Regulation (G.D.P.R.)
            goes into effect on May 25 and is meant to ensure a common set of
            data rights in the European Union. It requires organizations to
            notify users as soon as possible of high-risk data breaches that
            could personally affect them.
          </p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseModal}>
            I Got It
          </Button>
          <Button
            variant="link"
            className="text-gray ms-auto"
            onClick={handleCloseModal}
          >
            Close
          </Button>
        </Modal.Footer>
      </Modal>
      <SEO title="Register" />
      <section className="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <Container>
          <p className="text-center">
            <Card.Link
              as={Link}
              to="/login"
              className="text-gray-700"
            >
              <FontAwesomeIcon icon={faAngleLeft} className="me-2" /> Back to
              Sign in Page
            </Card.Link>
          </p>
          <Row
            className="justify-content-center form-bg-image"
            style={{ backgroundImage: `url(${BgImage})` }}
          >
            <Col
              xs={12}
              className="d-flex align-items-center justify-content-center"
            >
              <div className="mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                <div className="text-center text-md-center mb-4 mt-md-0">
                  <h3 className="mb-0">Create an account</h3>
                </div>
                <Formik
                  initialValues={{
                    email: "",
                    firstName: "",
                    lastName: "",
                    password: "",
                    confirmPassword: "",
                    policy: false,
                  }}
                  validationSchema={Yup.object().shape({
                    email: Yup.string()
                      .email("Must be a valid email")
                      .max(255)
                      .required("Email is required"),
                    firstName: Yup.string()
                      .max(255)
                      .required("First name is required"),
                    lastName: Yup.string()
                      .max(255)
                      .required("Last name is required"),
                    password: Yup.string()
                      .max(255)
                      .required("Password is required")
                      .matches(
                        /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/,
                        "Must Contain 8 Characters, One Uppercase, One Lowercase, One Number and one special case Character"
                      ),
                    confirmPassword: Yup.string().test(
                      "match",
                      "Password do not match",
                      function (confirmPassword) {
                        return confirmPassword === this.parent.password;
                      }
                    ),
                    policy: Yup.boolean().oneOf(
                      [true],
                      "This field must be checked"
                    ),
                  })}
                  onSubmit={(values, { setSubmitting }) => {
                    dispatch(register(values));

                  }}
                >
                  {({
                    errors,
                    handleBlur,
                    handleChange,
                    handleSubmit,
                    isSubmitting,
                    touched,
                    values,
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
                            onBlur={handleBlur}
                            onChange={handleChange}
                            value={values.email}
                            error={Boolean(touched.email && errors.email)}
                            required
                            type="email"
                            placeholder="example@company.com"
                          />
                        </InputGroup>
                        {touched.email && errors.email && (
                          <Alert variant="danger">{errors.email}</Alert>
                        )}
                      </Form.Group>
                      <Row className="mb-3">
                      <Form.Group as={Col} id="firstName" >
                        <Form.Label>First Name</Form.Label>
                        <InputGroup>
                          <InputGroup.Text>
                            <FontAwesomeIcon icon={faUser} />
                          </InputGroup.Text>
                          <Form.Control
                            name="firstName"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            value={values.firstName}
                            error={Boolean(
                              touched.firstName && errors.firstName
                            )}
                            required
                            placeholder="First Name"
                          />
                        </InputGroup>
                        {touched.firstName && errors.firstName && (
                          <Alert variant="danger">{errors.firstName}</Alert>
                        )}
                      </Form.Group>
                      <Form.Group as={Col} id="lastName">
                        <Form.Label>Last Name</Form.Label>
                        <InputGroup>
                          <InputGroup.Text>
                            <FontAwesomeIcon icon={faUser} />
                          </InputGroup.Text>
                          <Form.Control
                            name="lastName"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            value={values.lastName}
                            error={Boolean(touched.lastName && errors.lastName)}
                            required
                            placeholder="Last Name"
                          />
                        </InputGroup>
                        {touched.lastName && errors.lastName && (
                          <Alert variant="danger">{errors.lastName}</Alert>
                        )}
                      </Form.Group>
                      </Row>
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
                        {touched.password && errors.password && (
                            <Alert variant="danger">{errors.password}</Alert>
                          )}
                      </Form.Group>
                      <Form.Group id="confirm_password" className="mb-4">
                        <Form.Label>Confirm Password</Form.Label>
                        <InputGroup>
                          <InputGroup.Text>
                            <FontAwesomeIcon icon={faUnlockAlt} />
                          </InputGroup.Text>
                          <Form.Control
                            error={Boolean(
                              touched.confirmPassword && errors.confirmPassword
                            )}
                            name="confirmPassword"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            type="password"
                            value={values.confirmPassword}
                            placeholder="Confirm Password"
                            required
                          />
                        </InputGroup>
                        {touched.confirmPassword && errors.confirmPassword && (
                          <Alert variant="danger">
                            {errors.confirmPassword}
                          </Alert>
                        )}
                      </Form.Group>
                      <FormCheck type="checkbox" className="d-flex mb-4">
                        <FormCheck.Input
                          name="policy"
                          onChange={handleChange}
                          required
                          id="terms"
                          className="me-2"
                        />
                        <FormCheck.Label htmlFor="terms">
                          I agree to the{" "}
                          <Card.Link onClick={() => setShowTerm(true)}>
                            terms and conditions
                          </Card.Link>
                        </FormCheck.Label>
                      </FormCheck>
                      <Button
                        disabled={isSubmitting}
                        variant="primary"
                        type="submit"
                        className="w-100"
                      >
                        Sign Up
                      </Button>
                    </form>
                  )}
                </Formik>
                <div className="mt-3 mb-4 text-center">
                  <span className="fw-normal">or</span>
                </div>
                <div className="d-flex justify-content-center my-4">
                  <Button
                    variant="outline-light"
                    className="btn-icon-only btn-pill text-facebook me-2"
                  >
                    <FontAwesomeIcon icon={faFacebookF} />
                  </Button>
                  <Button
                    variant="outline-light"
                    className="btn-icon-only btn-pill text-twitter me-2"
                  >
                    <FontAwesomeIcon icon={faTwitter} />
                  </Button>
                  <Button
                    variant="outline-light"
                    className="btn-icon-only btn-pil text-dark"
                  >
                    <FontAwesomeIcon icon={faGithub} />
                  </Button>
                </div>
                <div className="d-flex justify-content-center align-items-center mt-4">
                  <span className="fw-normal">
                    Already have an account?
                    <Card.Link
                      as={Link}
                      to={Routes.Login.path}
                      className="fw-bold"
                    >
                      {` Login here `}
                    </Card.Link>
                  </span>
                </div>
                <div className="d-flex justify-content-center align-items-center mt-4">
                  <span className="fw-normal">
                    Register but not receive email?
                    <Card.Link
                      as={Link}
                      to={Routes.ResendEmail.path}
                      className="fw-bold"
                    >
                      {` Click here `}
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
};
