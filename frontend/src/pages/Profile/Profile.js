import React,{useEffect} from "react";
import {
    Button,
    Card,
    CardHeader,
    CardBody,
    FormGroup,
    Form,
    Input,
    Container,
    Row,
    Col,
  } from "reactstrap";
  // core components
  import UserHeader from "components/profile/UserHeader";
  import { useParams } from "react-router-dom";
  import './Styles.scss';
  import './argon-dashboard-react.css'
  import {useDispatch,useSelector} from 'react-redux';
  import {fetchUserDetail,updateUserAvatar} from 'pages/Profile/ProfileSlice';
import Preloader from 'components/Preloader';
import SEO from "components/SEO";

  const Profile = () => {
    const { id } = useParams();
    const dispatch = useDispatch();
    const userDetail = useSelector((state) => state.profile.userDetail);
    const loading = useSelector((state) => state.profile.loading);
    const userAvatar = useSelector((state) => state.profile.userAvatar);

    const handleImageChange = (e) =>{
      
      const employeeImage = e.target.files[0];

      var formData = new FormData();
      formData.append("image",employeeImage);

      console.log(formData);
      dispatch(updateUserAvatar(formData));
    }

    const handleEditPicture = () =>{
      const inputElement = document.getElementById("imageInput");
      inputElement.click();
    }

    if(userDetail === null){
      return <Preloader show={loading}/>
    }

    return (
      <>
        <UserHeader firstName={userDetail.firstName} empId={userDetail.employeeId}/>
        <SEO title="Profile"/>
        {/* Page content */}
        <Container className="mt--7" fluid>
          <Row className="p-5">
            <Col className="order-xl-2 mb-5 mb-xl-0" xl="4">
              <Card className="card-profile shadow">
              <Row className="justify-content-center">
                  <Col className="order-lg-2" lg="3">
                    <div className="card-profile-image">
                      <a onClick={() => handleEditPicture()}>
                        <img
                          alt={userAvatar.imageName}
                          className="rounded-circle"
                          src={`${userAvatar.imageHeader}${userAvatar.imageBinary}`}
                        />
                      </a>
                      <input
                        type="file" 
                        hidden="hidden" 
                        name="imageInput"
                        id="imageInput"
                        onChange={(e) => handleImageChange(e)} />
                    </div>
                  </Col>
                </Row>
                <CardBody className="pt-8 pt-md-10">
                  <div className="text-center ">
                    <h3>
                    {userDetail.firstName}
                      <span className="font-weight-light">, 27</span>
                    </h3>
                    <div className="h5 font-weight-300">
                      <i className="ni location_pin mr-2" />
                      Bucharest, Romania
                    </div>
                    <div className="h5 mt-4">
                      <i className="ni business_briefcase-24 mr-2" />
                      Solution Manager - Creative Tim Officer
                    </div>
                    <div>
                      <i className="ni education_hat mr-2" />
                      University of Computer Science
                    </div>
                    <hr className="my-4" />
                    <p>
                      Ryan — the name taken by Melbourne-raised, Brooklyn-based
                      Nick Murphy — writes, performs and records all of his own
                      music.
                    </p>
                  </div>
                </CardBody>
              </Card>
            </Col>
            <Col className="order-xl-1" xl="8">
              <Card className="bg-light shadow">
                <CardHeader className="bg-white border-0">
                  <Row className="align-items-center">
                    <Col xs="8">
                      <h3 className="mb-0">My account</h3>
                    </Col>
                    <Col className="text-right" xs="4">
                    </Col>
                  </Row>
                </CardHeader>
                <CardBody>
                  <Form>
                    <h6 className="heading-small text-muted mb-4">
                      User information
                    </h6>
                    <div className="pl-lg-4">
                      <Row>
                        <Col lg="6">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-username"
                            >
                              Card No
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail.cardNo}
                            </div>
                          </FormGroup>
                        </Col>
                        <Col lg="6">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-email"
                            >
                              Phone Number
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail?.phoneNumber}
                            </div>
                          </FormGroup>
                        </Col>
                      </Row>
                      <Row>
                        <Col lg="6">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-first-name"
                            >
                              First name
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail?.firstName}
                            </div>
                          </FormGroup>
                        </Col>
                        <Col lg="6">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-last-name"
                            >
                              Last name
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail?.lastName}
                            </div>
                          </FormGroup>
                        </Col>
                      </Row>
                    </div>
                    <hr className="my-4" />
                    {/* Address */}
                    <h6 className="heading-small text-muted mb-4">
                      Address information
                    </h6>
                    <div className="pl-lg-4">
                      <Row>
                        <Col md="12">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-address"
                            >
                              Address
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail.employeeAddress?.homeAddress}
                            </div>
                          </FormGroup>
                        </Col>
                      </Row>
                      <Row>
                        <Col lg="4">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-city"
                            >
                              City
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail.employeeAddress?.city}
                            </div>
                          </FormGroup>
                        </Col>
                        <Col lg="4">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-country"
                            >
                              Country
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail.employeeAddress?.country}
                            </div>
                          </FormGroup>
                        </Col>
                        <Col lg="4">
                          <FormGroup>
                            <label
                              className="form-control-label"
                              htmlFor="input-country"
                            >
                              Postal code
                            </label>
                            <div className="h5 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail.employeeAddress?.postalCode}
                            </div>
                          </FormGroup>
                        </Col>
                      </Row>
                    </div>
                    <hr className="my-4" />
                    {/* Description */}
                    <h6 className="heading-small text-muted mb-4">About me</h6>
                    <div className="pl-lg-4">
                      <FormGroup>
                        <label>About Me</label>
                        <div className="h4 font-weight-300">
                              <i className="ni location_pin" />
                              {userDetail?.description}
                            </div>
                      </FormGroup>
                    </div>
                  </Form>
                </CardBody>
              </Card>
            </Col>
          </Row>
        </Container>
      </>
    );
  };
  
  export default Profile;