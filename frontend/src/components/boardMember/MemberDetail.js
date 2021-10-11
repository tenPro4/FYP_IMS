import React from "react";
import { Avatar } from "@material-ui/core";
import {avatarStyles} from 'pages/board/Styles';
/** @jsx jsx */
import { css, keyframes,jsx } from "@emotion/core";
import { useDispatch } from "react-redux";
import {OWNER_COLOR} from 'shared/utils/colors';
import {setDialogMember} from "./ProjectMemberSlice";

const scaleUp = keyframes`
    0% {
        transform: scale(1.0);
    }
    100% {
        transform: scale(1.15);
    }
`;

const animationStyles = css`
  animation: 0.2s ${scaleUp} forwards;
`;

const MemberDetail = ({ member, isOwner }) => {
    const dispatch = useDispatch();
    const {employeeImage} = member.employee;

    const handleClick = () => {
      dispatch(setDialogMember(member));
    };

    const imageUrl = employeeImage !== null 
    ? employeeImage.imageHeader+employeeImage.imageBinary
    : '';
  
    return (
      <Avatar
        css={css`
          ${avatarStyles}
          ${isOwner &&
          `border: 1px solid ${OWNER_COLOR}; border-radius: 50%;`}
          &:hover {
            ${animationStyles}
          }
        `}
        onClick={handleClick}
        data-testid={`member-${member.employeeId}`}
        src={imageUrl} 
        alt={employeeImage !== null ?  employeeImage.imageName : ''}
      >
        {member.employee.firstName.charAt(0)}
      </Avatar>
    );
};

export default MemberDetail;
