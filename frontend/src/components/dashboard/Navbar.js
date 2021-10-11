import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faBell,
  faCog,
  faEnvelopeOpen,
  faSearch,
  faSignOutAlt,
  faSignInAlt,
  faUserShield,
} from "@fortawesome/free-solid-svg-icons";
import { faUserCircle } from "@fortawesome/free-regular-svg-icons";
import {
  Row,
  Col,
  Nav,
  Form,
  Image,
  Navbar,
  Dropdown,
  Container,
  ListGroup,
  InputGroup,
} from "@themesberg/react-bootstrap";
import NOTIFICATIONS_DATA from "data/notifications";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "pages/login/LoginSlice";
import { useHistory } from "react-router-dom";
import { Link,useLocation } from "react-router-dom";
import { Routes } from "routes";
import { Avatar } from "@material-ui/core";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import {setCheckInDialogOpen,checkOut} from 'pages/attendance/AttendanceSlice';
import BreadCrumb from 'components/breadCrumb/BreadCrumb';

export default (props) => {
  const [notifications, setNotifications] = useState(NOTIFICATIONS_DATA);
  const areNotificationsRead = notifications.reduce(
    (acc, notif) => acc && notif.read,
    true
  );
  const dispatch = useDispatch();
  const user = useSelector((state) => state.profile.userDetail);
  const status = useSelector((state) => state.attendance.status);
  const history = useHistory();

  const markNotificationsAsRead = () => {
    setTimeout(() => {
      setNotifications(notifications.map((n) => ({ ...n, read: true })));
    }, 300);
  };

  const Notification = (props) => {
    const { link, sender, image, time, message, read = false } = props;
    const readClassName = read ? "" : "text-danger";

    return (
      <ListGroup.Item action href={link} className="border-bottom border-light">
        <Row className="align-items-center">
          <Col className="col-auto">
            <Image
              src={image}
              className="user-avatar lg-avatar rounded-circle"
            />
          </Col>
          <Col className="ps-0 ms--2">
            <div className="d-flex justify-content-between align-items-center">
              <div>
                <h4 className="h6 mb-0 text-small">{sender}</h4>
              </div>
              <div className="text-end">
                <small className={readClassName}>{time}</small>
              </div>
            </div>
            <p className="font-small mt-1 mb-0">{message}</p>
          </Col>
        </Row>
      </ListGroup.Item>
    );
  };

  const handleLogout = () => {
    sessionStorage.removeItem("accessJWT");
    localStorage.removeItem("ims");
    dispatch(logout());
    if(status !== null && status.current === 1){
        dispatch(checkOut());
    }
    history.push("/login");
  };

  if (user === null) {
    return null;
  }

  const { employeeImage } = user;

  const imageUrl =
    employeeImage !== null
      ? employeeImage.imageHeader + employeeImage.imageBinary
      : "";

  const location = useLocation();
  const { pathname } = location;
  const locationLink = { pathname: `/overview${pathname}`};

  return (
    <Navbar variant="dark" expanded className="ps-0 pe-2 pb-0">
      <Container fluid className="px-0">
        <div className="d-flex justify-content-between w-100">
          <div className="d-flex align-items-center">
            <Form className="navbar-search">
              <Form.Group id="topbarSearch">
               <BreadCrumb theme="dark" separator=" › " location={locationLink}/>
              </Form.Group>
            </Form>
          </div>
          <Nav className="align-items-center">
            <Dropdown as={Nav.Item} onToggle={markNotificationsAsRead}>
              <Dropdown.Toggle
                as={Nav.Link}
                className="text-dark icon-notifications me-lg-3"
              >
                <span className="icon icon-sm">
                  <FontAwesomeIcon icon={faBell} className="bell-shake" />
                  {areNotificationsRead ? null : (
                    <span className="icon-badge rounded-circle unread-notifications" />
                  )}
                </span>
              </Dropdown.Toggle>
              <Dropdown.Menu className="dashboard-dropdown notifications-dropdown dropdown-menu-lg dropdown-menu-center mt-2 py-0">
                <ListGroup className="list-group-flush">
                  <Nav.Link
                    href="#"
                    className="text-center text-primary fw-bold border-bottom border-light py-3"
                  >
                    Notifications
                  </Nav.Link>

                  {notifications.map((n) => (
                    <Notification key={`notification-${n.id}`} {...n} />
                  ))}

                  <Dropdown.Item className="text-center text-primary fw-bold py-3">
                    View all
                  </Dropdown.Item>
                </ListGroup>
              </Dropdown.Menu>
            </Dropdown>

            <Dropdown as={Nav.Item}>
              <Dropdown.Toggle as={Nav.Link} className="pt-1 px-0">
                <div className="media d-flex align-items-center">
                  <Avatar
                    css={css`
                      height: 2.5rem;
                      width: 2.5rem;
                      font-size: 15px;
                      margin-bottom: 0.5rem;
                    `}
                    src={imageUrl}
                    alt={employeeImage !== null ? employeeImage.imageName : ""}
                  >
                    {user.firstName.charAt(0)}
                  </Avatar>
                  <div className="media-body ms-2 text-dark align-items-center d-none d-lg-block">
                    <span className="mb-0 font-small fw-bold">
                      {user.firstName} {user.lastName}
                    </span>
                  </div>
                </div>
              </Dropdown.Toggle>
              <Dropdown.Menu className="user-dropdown dropdown-menu-right mt-2">
                <Dropdown.Item
                  as={Link}
                  to={Routes.Profile.path}
                  className="fw-bold"
                >
                  <FontAwesomeIcon icon={faUserCircle} className="me-2" /> My
                  Profile
                </Dropdown.Item>
                <Dropdown.Item className="fw-bold" onClick={() => dispatch(setCheckInDialogOpen(true))}>
                  <FontAwesomeIcon icon={faSignInAlt} className="me-2" /> Check In
                </Dropdown.Item>
                {/* <Dropdown.Item className="fw-bold">
                  <FontAwesomeIcon icon={faUserShield} className="me-2" />{" "}
                  Support
                </Dropdown.Item> */}

                <Dropdown.Divider />

                <Dropdown.Item className="fw-bold" onClick={handleLogout}>
                  <FontAwesomeIcon
                    icon={faSignOutAlt}
                    className="text-danger me-2"
                  />{" "}
                  Logout
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </Nav>
        </div>
      </Container>
    </Navbar>
  );
};
