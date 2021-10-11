import React, { useEffect } from "react";
import ProfileHeader from "components/profileEdit/ProfileHeader";
import ProfileLeftNav from "components/profileEdit/ProfileLeftNav";
import {
  Container,
  Row,
  Col,
  Input,
  Card,
  CardBody,
  CardFooter,
  CardTitle,
  Label,
  FormGroup,  
} from "reactstrap";
import Preloader from "components/Preloader";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { fetchUserDetailWithId,changeUserPassword } from "pages/Profile/ProfileSlice";
import * as Yup from "yup";
import { Formik } from "formik";
import {
  Form,
  Button,
  InputGroup,
  Alert,
} from "@themesberg/react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleLeft,
  faEnvelope,
  faUnlockAlt,
} from "@fortawesome/free-solid-svg-icons";
import SEO from "components/SEO";

const HeaderMain = (props) => {
  return (
    <React.Fragment>
      {/* START H1 Header */}
      <div className={` d-flex ${props.className}`}>
        <h1 className="display-4 mr-3 mb-0 align-self-start">{props.title}</h1>
      </div>
      {/* END H1 Header */}
    </React.Fragment>
  );
};

const PasswordEdit = () => {
  const { id } = useParams();
  const dispatch = useDispatch();
  const userDetail = useSelector((state) => state.profile.userDetail);
  const loading = useSelector((state) => state.profile.loading);

  useEffect(() => {
    dispatch(fetchUserDetailWithId(id));
  }, []);

  if (userDetail === null) {
    return <Preloader show={loading} />;
  }

  return (
    <React.Fragment>
    <SEO title="Password-Edit"/>
      <Container>
        <HeaderMain title="Password Edit" className="mb-5 mt-4" />
        {/* START Content */}
        <Row>
          <Col lg={12}>
            <ProfileHeader user={userDetail} />
          </Col>
          <Col lg={3}>
            <ProfileLeftNav />
          </Col>
          <Col lg={9}>
            <Card className="mb-3">
              <CardBody>
                <div className="d-flex mb-4">
                  <CardTitle tag="h6">Account Edit</CardTitle>
                  <span className="ml-auto align-self-start small">
                    Fields mark as <span className="text-danger">*</span> is
                    required.
                  </span>
                </div>
                <Formik
                  initialValues={{
                    email:"example@com.my",
                    oldPassword:"",
                    password: "",
                    confirmPassword: "",
                  }}
                  validationSchema={Yup.object().shape({
                    oldPassword:Yup.string()
                    .required("Old Password is required"),
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
                    
                    dispatch(changeUserPassword(values));
                    resetForm({});
                    setSubmitting(false);

                    // try {
                    //     await auth.passwordUpdate(values.oldPassword, values.passwordOne)
                    //     resetForm({})
                    //     setStatus({success: true})
                    // } catch (error) {
                    //     setStatus({success: false})
                    //     setSubmitting(false)
                    //     setErrors({submit: error.message})
                    // }
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
                    <Form.Group id="oldPassword" className="mb-4">
                        <Form.Label>Old Password</Form.Label>
                        <InputGroup>
                          <InputGroup.Text>
                            <FontAwesomeIcon icon={faUnlockAlt} />
                          </InputGroup.Text>
                          <Form.Control
                            error={Boolean(touched.oldPassword && errors.oldPassword)}
                            name="oldPassword"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            type="password"
                            value={values.oldPassword}
                            placeholder="Old Password"
                            required
                          />
                        </InputGroup>
                        {errors.oldPassword && (
                          <Alert variant="danger">{errors.oldPassword}</Alert>
                        )}
                      </Form.Group>
                      <Form.Group id="password" className="mb-4">
                        <Form.Label>New Password</Form.Label>
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
                            placeholder="New Password"
                            required
                          />
                        </InputGroup>
                        {errors.password && (
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
                      >
                        Change password
                      </Button>
                    </form>
                  )}
                </Formik>
              </CardBody>
              <CardFooter className="small">
                <i className="fa fa-fw fa-support text-muted mr-2"></i>
                If you have trouble with the configuration, you can contact us.{" "}
                <a href="#">We Can Help</a>
              </CardFooter>
            </Card>
            <Card className="mb-3 b-danger">
              <CardBody>
                <CardTitle tag="h6" className="text-danger">
                  Delete Account
                </CardTitle>
                <p>
                  Once you delete your account, there is no going back. Please
                  be certain.
                </p>
                <Button color="danger" outline>
                  Yes, Delete
                </Button>
              </CardBody>
              <CardFooter className="small">
                <i className="fa fa-fw fa-support text-muted mr-2"></i>
                Are you sure you want to remove your account permanently?
                <strong>This process cannot restore, please consider carefully.</strong>
              </CardFooter>
            </Card>
          </Col>
        </Row>
        {/* END Content */}
      </Container>
    </React.Fragment>
  );
};

export default PasswordEdit;
