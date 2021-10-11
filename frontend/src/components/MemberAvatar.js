import { Avatar } from "@material-ui/core";
import React from "react";
import { avatarStyles } from "pages/board/Styles";

const MemberAvatar = ({ member }) => {

  const {employeeImage} = member;

  const imageUrl = employeeImage !== null 
  ? employeeImage.imageHeader+employeeImage.imageBinary
  : '';

  return (
    <Avatar css={avatarStyles} src={imageUrl} alt="member-avatar">
      {member?.firstName?.charAt(0) || "-"}
    </Avatar>
  );
};

export default MemberAvatar;
