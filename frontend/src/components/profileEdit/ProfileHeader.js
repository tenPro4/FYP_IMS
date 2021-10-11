import React from "react";
import { Link } from "react-router-dom";
import { Badge, Media } from "reactstrap";
// import 'bootstrap/dist/css/bootstrap.min.css';
// import 'bootstrap/scss/bootstrap.scss';
// import '@owczar/dashboard-style--airframe/scss/style.scss';
import profileImage from 'assets/img/team/profile-picture-3.jpg';
import {Image} from '@themesberg/react-bootstrap';
import { Routes } from 'routes';
/** @jsx jsx */
import { Avatar } from "@material-ui/core";
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";

const ProfileHeader = ({user}) => {
  const {employeeImage,firstName} = user;

  const imageUrl =
  employeeImage !== null
    ? employeeImage.imageHeader + employeeImage.imageBinary
    : "";

  return (
    <React.Fragment>
      {/* START Header */}
      <Media className="mb-3">
        <Media left middle className="mr-3 align-self-center">
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
          {firstName.charAt(0)}
        </Avatar>
        </Media>
        <Media body>
          <h5 className="mb-1 mt-0">
            <Link to={Routes.Profile.path}>
              {firstName}
            </Link>{" "}
            <span className="text-muted mx-1"> / </span> Profile Edit
          </h5>
          <span className="text-muted">Edit Your Name, Address, etc.</span>
        </Media>
      </Media>
      {/* END Header */}
    </React.Fragment>
  );
};

export default ProfileHeader;
