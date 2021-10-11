import React, { useState } from "react";
import {
  Dialog,
  Avatar,
  Button,
  Fab,
  useMediaQuery,
  useTheme,
  DialogTitle,
} from "@material-ui/core";
import { Alert } from "@material-ui/lab";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import { faAngleLeft } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useDispatch, useSelector } from "react-redux";
import Close from "./Close";
import {
  removeProjectMember,
  setDialogMember,
  removeFromMember,
  removeMemberFromProject,
} from "./ProjectMemberSlice";
import {projectOwner} from "pages/board/ProjectSlice"

const Container = styled.div`
  display: flex;
  align-items: center;
  padding: 0.5rem 2rem 2rem 2rem;
  ${(props) => props.theme.breakpoints.down("xs")} {
    flex-direction: column;
  }
`;

const PrimaryText = styled.h3`
  margin-top: 0;
  word-break: break-all;
`;

const Main = styled.div`
  margin-left: 2rem;
  font-size: 16px;
`;

const SecondaryText = styled.p`
  margin: 0;
  font-size: 14px;
  color: #777;
  word-break: break-all;
`;

const ConfirmAction = styled.div`
  display: flex;
  justify-content: space-between;
`;

const MemberDialog = ({ project }) => {
  const theme = useTheme();
  const dispatch = useDispatch();
  const owner = useSelector((state) =>projectOwner(state));
  const detail = useSelector((state) => state.project.detail);

  const xsDown = useMediaQuery(theme.breakpoints.down("xs"));
  const [confirmDelete, setConfirmDelete] = useState(false);
  const member = useSelector((state) => state.projectMember.dialogMember);
  const memberIsOwner = member?.employeeId === project.employeeLeaderId;
  const open = member !== null;

  if (!member) {
    return null;
  }

  const handleClose = () => {
    dispatch(setDialogMember(null));
    setConfirmDelete(false);
  };

  const handleRemoveMember = async () => {
    try {
      const param = {
        employeeId:member.employeeId,
        projectId: detail.projectId,
      };
      dispatch(removeMemberFromProject(param))
      handleClose();
    } catch (err) {
      
    }
  };

  const {employeeImage} = member.employee;
  const imageUrl = employeeImage !== null 
  ? employeeImage.imageHeader+employeeImage.imageBinary
  : '';

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="xs"
      fullWidth
      fullScreen={xsDown}
    >
      <Close onClose={handleClose} />
      <DialogTitle id="member-detail">Member</DialogTitle>
      <Container theme={theme}>
        {confirmDelete ? (
          <div>
            <Alert
              severity="error"
              css={css`
                margin-bottom: 2rem;
              `}
            >
              Are you sure you want to remove this member?
            </Alert>
            <ConfirmAction>
              <Fab
                size="small"
                onClick={() => setConfirmDelete(false)}
                css={css`
                  box-shadow: none;
                  &.MuiFab-sizeSmall {
                    width: 32px;
                    height: 32px;
                  }
                `}
              >
                <FontAwesomeIcon icon={faAngleLeft} color="#555" />
              </Fab>
              <Button
                size="small"
                color="secondary"
                variant="contained"
                onClick={handleRemoveMember}
                css={css`
                  font-size: 0.625rem;
                `}
              >
                Remove member
              </Button>
            </ConfirmAction>
          </div>
        ) : (
          <>
            <Avatar
              css={css`
                height: 8rem;
                width: 8rem;
                font-size: 40px;
                margin-bottom: 1rem;
              `}
              src={imageUrl} 
              alt={employeeImage !== null ?  employeeImage.imageName : ''}
            >
              {member.employee.firstName.charAt(0)}
            </Avatar>
            <Main>
              <PrimaryText>
                {member.employee.firstName} {member.employee.lastName}
              </PrimaryText>
              <SecondaryText>
                Name: <b>{member.employee.firstName}</b>
              </SecondaryText>
              <SecondaryText
                css={css`
                  margin-bottom: 1.5rem;
                `}
              >
                Card No: <b>{member?.employee.cardNo || "-"}</b>
              </SecondaryText>
              {memberIsOwner && (
                <Alert severity="info">Owner of this board</Alert>
              )}
              {!memberIsOwner && (
                <Button
                  size="small"
                  css={css`
                    color: #333;
                    font-size: 0.625rem;
                  `}
                  variant="outlined"
                  onClick={() => setConfirmDelete(true)}
                >
                  Remove from board
                </Button>
              )}
            </Main>
          </>
        )}
      </Container>
    </Dialog>
  );
};

export default MemberDialog;
