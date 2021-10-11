import React, { useEffect } from "react";
import { Container, Row, Col } from "reactstrap";
import PropTypes from "prop-types";
import classNames from "classnames";
import classes from "./Card.scss";
import ProfileHeader from "components/profileEdit/ProfileHeader";
import ProfileLeftNav from "components/profileEdit/ProfileLeftNav";
import * as Yup from "yup";
import { Formik } from "formik";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleLeft,
  faEnvelope,
  faUnlockAlt,
} from "@fortawesome/free-solid-svg-icons";
import {
  faFacebookF,
  faGithub,
  faTwitter,
} from "@fortawesome/free-brands-svg-icons";
import { Form, InputGroup, Alert } from "@themesberg/react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { login, clearErrors, authSelectors } from "pages/login/LoginSlice";
import Preloader from "components/Preloader";
import { useParams } from "react-router-dom";
import {
  fetchUserDetailWithId,
  updateUserBasicInformation,
} from "pages/Profile/ProfileSlice";
import {
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Divider,
  Grid,
  TextField,
} from "@material-ui/core";
import SEO from "components/SEO";

const genders = [
  {
    value: "f",
    label: "Female",
  },
  {
    value: "m",
    label: "Male",
  },
];

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

const ProfileEdit = () => {
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

  const { firstName, lastName, phoneNumber, gender, description } = userDetail;

  return (
    <React.Fragment>
    <SEO title="Profile-Edit"/>
      <Container>
        <HeaderMain title="Profile Edit" className="mb-5 mt-4" />
        <Row>
          <Col lg={12}>
            <ProfileHeader user={userDetail} />
          </Col>
          <Col lg={3}>
            <ProfileLeftNav />
          </Col>
          <Col lg={9}>
            <Card>
              <Formik
                initialValues={{
                  empId: userDetail.employeeId,
                  firstName: firstName,
                  lastName: lastName,
                  phoneNumber: Number(phoneNumber),
                  gender: gender,
                  description: description,
                }}
                validationSchema={Yup.object().shape({
                  firstName: Yup.string()
                    .max(255)
                    .required("First name is required"),
                  lastName: Yup.string()
                    .max(255)
                    .required("Last name is required"),
                })}
                onSubmit={(values, { setSubmitting }) => {
                  setTimeout(() => {
                    console.log(values);
                    dispatch(updateUserBasicInformation(values));
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
                  values,
                }) => (
                  <form className="mt-4" onSubmit={handleSubmit}>
                    <CardHeader
                      subheader="The information can be edited"
                      title="Profile"
                    />
                    <Divider />
                    <CardContent>
                      <Grid container spacing={3}>
                        <Grid item md={6} xs={12}>
                          <TextField
                            fullWidth
                            helperText="Please specify the first name"
                            label="First name"
                            name="firstName"
                            onChange={handleChange}
                            required
                            value={values.firstName}
                            variant="outlined"
                          />
                        </Grid>
                        <Grid item md={6} xs={12}>
                          <TextField
                            fullWidth
                            label="Last name"
                            name="lastName"
                            onChange={handleChange}
                            required
                            value={values.lastName}
                            variant="outlined"
                          />
                        </Grid>
                        <Grid item md={6} xs={12}>
                          <TextField
                            fullWidth
                            label="Phone Number"
                            name="phoneNumber"
                            type="number"
                            onChange={handleChange}
                            required
                            value={values.phoneNumber}
                            variant="outlined"
                          />
                        </Grid>
                        <Grid item md={6} xs={12}>
                          <TextField
                            fullWidth
                            label="Select Gender"
                            name="gender"
                            onChange={handleChange}
                            required
                            select
                            SelectProps={{ native: true }}
                            value={values.gender}
                            variant="outlined"
                          >
                            {genders.map((option) => (
                              <option key={option.value} value={option.value}>
                                {option.label}
                              </option>
                            ))}
                          </TextField>
                        </Grid>
                        <Grid item xs={12}>
                          <TextField
                            fullWidth
                            placeholder="About You..."
                            name="description"
                            onChange={handleChange}
                            multiline
                            value={values.description}
                            rows={2}
                            rowsMax={4}
                          />
                        </Grid>
                      </Grid>
                    </CardContent>
                    <Divider />
                    <Box
                      sx={{
                        display: "flex",
                        justifyContent: "flex-end",
                        p: 2,
                      }}
                    >
                      <Button
                        color="primary"
                        variant="contained"
                        disabled={isSubmitting}
                        type="submit"
                      >
                        Save details
                      </Button>
                    </Box>
                  </form>
                )}
              </Formik>
            </Card>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

HeaderMain.propTypes = {
  title: PropTypes.string,
  subTitle: PropTypes.node,
  className: PropTypes.string,
};
HeaderMain.defaultProps = {
  title: "Waiting for Data...",
  subTitle: "Waiting for Data...",
  className: "my-4",
};

export default ProfileEdit;
