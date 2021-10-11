import React from "react";
import { Chip, Avatar, ChipProps } from "@material-ui/core";

const AvatarTag = ({ option, ...rest }) => {

  const {employeeImage} = option;
  const imageUrl = employeeImage !== null &&  employeeImage !== undefined
  ? employeeImage.imageHeader+employeeImage.imageBinary
  : '';

  return (
    <Chip
      key={option.employeeId}
      avatar={<Avatar alt={employeeImage !== null ?  employeeImage.imageName : ''} src={imageUrl} />}
      variant="outlined"
      label={option.firstName}
      size="small"
      {...rest}
    />
  );
};

export default AvatarTag;
