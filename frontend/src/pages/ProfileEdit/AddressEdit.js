import React, { useEffect } from "react";
import ProfileHeader from "components/profileEdit/ProfileHeader";
import ProfileLeftNav from "components/profileEdit/ProfileLeftNav";
import {
  fetchUserDetailWithId,
  changeUserAddress,
} from "pages/Profile/ProfileSlice";
import * as Yup from "yup";
import { Formik } from "formik";
import Preloader from "components/Preloader";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { Col, Row, Form, Button } from "@themesberg/react-bootstrap";
import { Container, Card, CardBody, CardFooter, CardTitle } from "reactstrap";
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

const AddressEdit = () => {
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

  const { employeeAddress } = userDetail;  
  return (
    <React.Fragment>
    <SEO title="Address-Edit"/>
      <Container>
        <HeaderMain title="Address Edit" className="mb-5 mt-4" />
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
                    empId: id,
                    homeAddress: employeeAddress.homeAddress,
                    city: employeeAddress.city,
                    country: employeeAddress.country,
                    postalCode: employeeAddress.postalCode,
                  }}
                  validationSchema={Yup.object().shape({

                })}
                  onSubmit={(values, { setSubmitting}) => {
                    dispatch(changeUserAddress(values));
                    setSubmitting(false);
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
                      <h5 className="my-4">Address</h5>
                      <Row>
                        <Col sm={12} className="mb-3">
                          <Form.Group id="address">
                            <Form.Label>Address</Form.Label>
                            <Form.Control
                              required
                              type="text"
                              placeholder="Enter your home address"
                              name="homeAddress"
                              value={values.homeAddress}
                              onBlur={handleBlur}
                              onChange={handleChange}
                            />
                          </Form.Group>
                        </Col>
                      </Row>
                      <Row>
                        <Col sm={4} className="mb-3">
                          <Form.Group id="city">
                            <Form.Label>City</Form.Label>
                            <Form.Control
                              required
                              name="city"
                              type="text"
                              placeholder="City"
                              onBlur={handleBlur}
                              onChange={handleChange}
                              value={values.city}
                            />
                          </Form.Group>
                        </Col>
                        <Col sm={4} className="mb-3">
                          <Form.Group id="country">
                            <Form.Label>Country</Form.Label>
                            <Form.Control
                              required
                              name="country"
                              type="text"
                              placeholder="Country"
                              onBlur={handleBlur}
                              onChange={handleChange}
                              value={values.country}
                            />
                          </Form.Group>
                        </Col>
                        <Col sm={4} className="mb-3">
                          <Form.Group id="postalCode">
                            <Form.Label>Postal code</Form.Label>
                            <Form.Control
                              required
                              name="postalCode"
                              type="number"
                              placeholder="Postal code"
                              onBlur={handleBlur}
                              onChange={handleChange}
                              value={values.postalCode}
                            />
                          </Form.Group>
                        </Col>
                      </Row>
                      <Button
                        disabled={isSubmitting}
                        variant="primary"
                        type="submit"
                      >
                        Update Address
                      </Button>
                    </form>
                  )}
                </Formik>
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default AddressEdit;
