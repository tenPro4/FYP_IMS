import React,{useEffect} from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleLeft,
  faEnvelope,
  faUnlockAlt,
} from "@fortawesome/free-solid-svg-icons";
import {
  Col,
  Row,
  Form,
  Card,
  Button,
  Container,
  InputGroup,
  Alert
} from "@themesberg/react-bootstrap";
import { Link, useParams,useLocation,useHistory } from "react-router-dom";
import { Routes } from "routes";
import * as Yup from 'yup';
import { Formik } from 'formik';
import axios from "axios";
import { toast } from 'react-toastify';
import {resetPassword} from './ResetPasswordSlice';
import { useDispatch, useSelector} from "react-redux";

export default () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const {reset,loading} = useSelector( (state) => state.pass);
  const history = useHistory();
  const queryParams = new URLSearchParams(location.search);
  const token = queryParams.get('token');
  const email = queryParams.get('email');

  return (
    <main>
      <section className="bg-soft d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <Container>
          <Row className="justify-content-center">
            <p className="text-center">
              <Card.Link
                as={Link}
                to={Routes.Login.path}
                className="text-gray-700"
              >
                <FontAwesomeIcon icon={faAngleLeft} className="me-2" /> Back to
                sign in
              </Card.Link>
            </p>
            <Col
              xs={12}
              className="d-flex align-items-center justify-content-center"
            >
              <div className="bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                <h3 className="mb-4">Reset password</h3>
                <Formik
                  initialValues={{
                    token:token,
                    email:email,
                    password: "",
                    confirmPassword: "",
                  }}
                  validationSchema={Yup.object().shape({
                    password: Yup.string()
                      .max(255)
                      .required("Password is required")
                      .matches(
                        /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$/,
                        "Must Contain 6 Characters, One Uppercase, One Lowercase, One Number and one special case Character"
                        ),
                    confirmPassword: Yup.string().test(
                      "match",
                      "Password do not match",
                      function (confirmPassword) {
                        return confirmPassword === this.parent.password;
                      }
                    ),
                  })}
                  onSubmit={(values, { setSubmitting,resetForm }) => {
                    setSubmitting(true)
                    dispatch(resetPassword(values));
                    resetForm({});

                    setSubmitting(loading);
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
                        {errors.password && (
                            <Alert variant="danger">
                              {errors.password}
                            </Alert>
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
                        {errors.confirmPassword && (
                            <Alert variant="danger">
                              {errors.confirmPassword}
                            </Alert>
                          )}
                      </Form.Group>
                      <Button
                        disabled={isSubmitting}
                        variant="primary"
                        type="submit"
                        className="w-100"
                      >
                        Reset password
                      </Button>
                    </form>
                  )}
                </Formik>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </main>
  );
};
