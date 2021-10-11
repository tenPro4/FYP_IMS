import React ,{useState}from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleLeft, faEnvelope } from "@fortawesome/free-solid-svg-icons";
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
import { Link, useHistory, useLocation } from "react-router-dom";
import { Routes } from "routes";
import * as Yup from 'yup';
import { Formik } from 'formik';
import axios from "axios";

export default () => {
  const history = useHistory();
  let location = useLocation();

  const [onSubmit, setOnSubmit] = useState('Recover password');

  return (
    <main>
      <section className="vh-lg-100 mt-4 mt-lg-0 bg-soft d-flex align-items-center">
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
              <div className="signin-inner my-3 my-lg-0 bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                <h3>Forgot your password?</h3>
                <p className="mb-4">
                  Don't fret! Just type in your email and we will send you a
                  code to reset your password!
                </p>
                <Formik
                  initialValues={{
                    email: "demo@devias.io",
                  }}
                  validationSchema={Yup.object().shape({
                    email: Yup.string()
                      .email("Must be a valid email")
                      .max(255)
                      .required("Email is required"),
                  })}
                  onSubmit={(values, { setSubmitting }) => {
                    setTimeout(() => {
                      setOnSubmit("Send again");
                      axios.get(`/account/forgetPassword/${values.email}`);
                      // history.push(`/resetPassword?token=123123&email=${values.email}`);
                      setSubmitting(false);
                    }, 400);
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
                        <Form.Label htmlFor="email">Your Email</Form.Label>
                        <InputGroup>
                          <InputGroup.Text>
                            <FontAwesomeIcon icon={faEnvelope} />
                          </InputGroup.Text>
                          <Form.Control
                            name="email"
                            type="email"
                            placeholder="example@company.com"
                            required
                            autoFocus
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
                      <Button
                        disabled={isSubmitting}
                        variant="primary"
                        type="submit"
                        className="w-100"
                      >
                        {onSubmit}
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
