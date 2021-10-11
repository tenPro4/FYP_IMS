import React, { useState } from "react";
import SimpleBar from "simplebar-react";
import { useLocation } from "react-router-dom";
import { CSSTransition } from "react-transition-group";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faBook,
  faBoxOpen,
  faChartPie,
  faCog,
  faFileAlt,
  faHandHoldingUsd,
  faSignOutAlt,
  faTable,
  faTimes,
  faCalendarAlt,
  faMapPin,
  faInbox,
  faRocket,
  faTasks,
  faSignInAlt,
  faUserLock,
  faCalendarTimes,
} from "@fortawesome/free-solid-svg-icons";
import {
  Nav,
  Badge,
  Image,
  Button,
  Dropdown,
  Accordion,
  Navbar,
} from "@themesberg/react-bootstrap";
import { Link } from "react-router-dom";
import { Routes } from "routes";
import ThemesbergLogo from "assets/img/themesberg.svg";
import ReactHero from "assets/img/technologies/react-hero-logo.svg";
import ProfilePicture from "assets/img/team/profile-picture-3.jpg";
import Assignment from '@material-ui/icons/Assignment';

export default (props = {}) => {
  const location = useLocation();
  const { pathname } = location;
  console.log(location);
  console.log(pathname);
  const [show, setShow] = useState(false);
  const showClass = show ? "show" : "";

  const onCollapse = () => setShow(!show);

  const CollapsableNavItem = (props) => {
    const { eventKey, title, icon, children = null } = props;
    const defaultKey = pathname.indexOf(eventKey) !== -1 ? eventKey : "";

    return (
      <Accordion as={Nav.Item} defaultActiveKey={defaultKey}>
        <Accordion.Item eventKey={eventKey}>
          <Accordion.Button
            as={Nav.Link}
            className="d-flex justify-content-between align-items-center"
          >
            <span>
              <span className="sidebar-icon">
                <FontAwesomeIcon icon={icon} />{" "}
              </span>
              <span className="sidebar-text">{title}</span>
            </span>
          </Accordion.Button>
          <Accordion.Body className="multi-level">
            <Nav className="flex-column">{children}</Nav>
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
    );
  };

  const NavItem = (props) => {
    const {
      title,
      link,
      external,
      target,
      icon,
      image,
      badgeText,
      badgeBg = "secondary",
      badgeColor = "primary",
    } = props;
    const classNames = badgeText
      ? "d-flex justify-content-start align-items-center justify-content-between"
      : "";
    const navItemClassName = link === pathname ? "active" : "";
    const linkProps = external ? { href: link } : { as: Link, to: link };

    return (
      <Nav.Item className={navItemClassName} onClick={() => setShow(false)}>
        <Nav.Link {...linkProps} target={target} className={classNames}>
          <span>
            {icon ? (
              <span className="sidebar-icon">
                <FontAwesomeIcon icon={icon} />{" "}
              </span>
            ) : null}
            {image ? (
              <Image
                src={image}
                width={20}
                height={20}
                className="sidebar-icon svg-icon"
              />
            ) : null}

            <span className="sidebar-text">{title}</span>
          </span>
          {badgeText ? (
            <Badge
              pill
              bg={badgeBg}
              text={badgeColor}
              className="badge-md notification-count ms-2"
            >
              {badgeText}
            </Badge>
          ) : null}
        </Nav.Link>
      </Nav.Item>
    );
  };

  return (
    <>
      <Navbar
        expand={false}
        collapseOnSelect
        variant="dark"
        className="navbar-theme-primary px-4 d-md-none"
      >
        <Navbar.Brand className="me-lg-5" as={Link} to={Routes.Entry.path}>
          <Image src={ReactHero} className="navbar-brand-light" />
        </Navbar.Brand>
        <Navbar.Toggle
          as={Button}
          aria-controls="main-navbar"
          onClick={onCollapse}
        >
          <span className="navbar-toggler-icon" />
        </Navbar.Toggle>
      </Navbar>
      <CSSTransition timeout={300} in={show} classNames="sidebar-transition">
        <SimpleBar
          className={`collapse ${showClass} sidebar d-md-block bg-primary text-white`}
        >
          <div className="sidebar-inner px-4 pt-3">
            <div className="user-card d-flex d-md-none align-items-center justify-content-between justify-content-md-center pb-4">
              <div className="d-flex align-items-center">
                <div className="user-avatar lg-avatar me-4">
                  <Image
                    src={ProfilePicture}
                    className="card-img-top rounded-circle border-white"
                  />
                </div>
                <div className="d-block">
                  <h6>Hi, Jane</h6>
                  <Button
                    as={Link}
                    variant="secondary"
                    size="xs"
                    to={Routes.Entry.path}
                    className="text-dark"
                  >
                    <FontAwesomeIcon icon={faSignOutAlt} className="me-2" />{" "}
                    Sign Out
                  </Button>
                </div>
              </div>
              <Nav.Link
                className="collapse-close d-md-none"
                onClick={onCollapse}
              >
                <FontAwesomeIcon icon={faTimes} />
              </Nav.Link>
            </div>
            <Nav className="flex-column pt-3 pt-md-0">
              <NavItem
                title="IMS"
                link="#"
                image={ReactHero}
              />

              <NavItem
                title="Overview"
                link={Routes.Overview.path}
                icon={faChartPie}
              />
                <NavItem
                title="Project"
                icon={faTasks}
                link={Routes.ProjectBoardList.path}
              />
              <NavItem
                title="Attendance"
                icon={faSignInAlt}
                link={Routes.Attendances.path}
              />
              <NavItem
                title="Leave"
                icon={faCalendarTimes}
                link={Routes.Leaves.path}
              />
              <NavItem
                title="Employee"
                icon={faUserLock}
                link={Routes.Employees.path}
              />
              <NavItem
                title="Event"
                icon={faCalendarAlt}
                link={Routes.EventCalendar.path}
              />
              {/* <NavItem
                external
                title="Messages"
                link="https://demo.themesberg.com/volt-pro-react/#/messages"
                target="_blank"
                badgeText="Pro"
                icon={faInbox}
              />
              <NavItem title="Settings" icon={faCog} link={Routes.Entry.path} />
              <NavItem
                external
                title="Calendar"
                link="https://demo.themesberg.com/volt-pro-react/#/calendar"
                target="_blank"
                badgeText="Pro"
                icon={faCalendarAlt}
              />
              <NavItem
                external
                title="Map"
                link="https://demo.themesberg.com/volt-pro-react/#/map"
                target="_blank"
                badgeText="Pro"
                icon={faMapPin}
              />

              <CollapsableNavItem
                eventKey="tables/"
                title="Tables"
                icon={faTable}
              >
                <NavItem title="Bootstrap Table" link={Routes.Entry.path} />
              </CollapsableNavItem> */}
{/* 
              <CollapsableNavItem
                eventKey="examples/"
                title="Page Examples"
                icon={faFileAlt}
              >
                <NavItem title="Sign In" link={Routes.Login.path} />
                <NavItem title="Sign Up" link={Routes.Register.path} />
                <NavItem
                  title="Forgot password"
                  link={Routes.ForgotPassword.path}
                />
                <NavItem
                  title="Reset password"
                  link={Routes.ResetPassword.path}
                />
                <NavItem title="Lock" link={Routes.Entry.path} />
                <NavItem title="404 Not Found" link={Routes.NotFound.path} />
                <NavItem title="500 Server Error" link={Routes.Entry.path} />
              </CollapsableNavItem> */}

              {/* <NavItem
                external
                title="Plugins"
                link="https://demo.themesberg.com/volt-pro-react/#/plugins/datatable"
                target="_blank"
                badgeText="Pro"
                icon={faChartPie}
              /> */}

              <Dropdown.Divider className="my-3 border-indigo" />

              {/* <CollapsableNavItem
                eventKey="documentation/"
                title="Getting Started"
                icon={faBook}
              >
                <NavItem title="Overview" link={Routes.Entry.path} />
                <NavItem title="Download" link={Routes.Entry.path} />
                <NavItem title="Quick Start" link={Routes.Entry.path} />
                <NavItem title="License" link={Routes.Entry.path} />
                <NavItem title="Folder Structure" link={Routes.Entry.path} />
                <NavItem title="Build Tools" link={Routes.Entry.path} />
                <NavItem title="Changelog" link={Routes.Entry.path} />
              </CollapsableNavItem>
              <CollapsableNavItem
                eventKey="components/"
                title="Components"
                icon={faBoxOpen}
              >
                <NavItem title="Accordion" link={Routes.Entry.path} />
                <NavItem title="Alerts" link={Routes.Entry.path} />
                <NavItem title="Badges" link={Routes.Entry.path} />
                <NavItem
                  external
                  title="Widgets"
                  link="https://demo.themesberg.com/volt-pro-react/#/components/widgets"
                  target="_blank"
                  badgeText="Pro"
                />
                <NavItem title="Breadcrumbs" link={Routes.Entry.path} />
                <NavItem title="Buttons" link={Routes.Entry.path} />
                <NavItem title="Forms" link={Routes.Entry.path} />
                <NavItem title="Modals" link={Routes.Entry.path} />
                <NavItem title="Navbars" link={Routes.Entry.path} />
                <NavItem title="Navs" link={Routes.Entry.path} />
                <NavItem title="Pagination" link={Routes.Entry.path} />
                <NavItem title="Popovers" link={Routes.Entry.path} />
                <NavItem title="Progress" link={Routes.Entry.path} />
                <NavItem title="Tables" link={Routes.Entry.path} />
                <NavItem title="Tabs" link={Routes.Entry.path} />
                <NavItem title="Toasts" link={Routes.Entry.path} />
                <NavItem title="Tooltips" link={Routes.Entry.path} />
              </CollapsableNavItem>
              <NavItem
                external
                title="Themesberg"
                link="https://themesberg.com"
                target="_blank"
                image={ThemesbergLogo}
              /> */}
            </Nav>
          </div>
        </SimpleBar>
      </CSSTransition>
    </>
  );
};
