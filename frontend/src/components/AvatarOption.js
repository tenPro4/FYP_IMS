import React from "react";
import { Avatar, ChipProps } from "@material-ui/core";
/** @jsx jsx */
import { css,jsx } from "@emotion/core";
import styled from "@emotion/styled";

const Username = styled.span`
  margin-left: 0.5rem;
  word-break: break-all;
`;

const AvatarOption = ({ option }) => {

  const {employeeImage} = option.employee;
  const imageUrl = employeeImage !== null 
  ? employeeImage.imageHeader+employeeImage.imageBinary
  : '';

    return (
      <>
        <Avatar
          css={css`
            height: 1.5rem;
            width: 1.5rem;
          `}
          alt={employeeImage !== null ?  employeeImage.imageName : ''}
          src={imageUrl}
        />
        <Username>{`${option.employee.firstName} ${option.employee.lastName}`}</Username>
      </>
    );
  };
  
  export default AvatarOption;