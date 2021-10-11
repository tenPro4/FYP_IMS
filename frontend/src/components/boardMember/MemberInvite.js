import React, { useState } from "react";
import { Button, Popover, Box } from "@material-ui/core";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import { useDispatch } from "react-redux";
import UserSearch from './UserSearch';
import {addMembersToProject} from './ProjectMemberSlice';

const InviteMember = styled.div`
  margin-left: 0.5rem;
`;

const Content = styled.div`
  padding: 2rem;
`;

const Description = styled.p`
  margin-top: 0;
  margin-bottom: 1rem;
  font-size: 0.875rem;
  font-weight: bold;
`;

const MemberInvite = ({ projectId }) => {
  const [anchorEl, setAnchorEl] = useState(null);
  const [tagsValue, setTagsValue] = useState([]);
  const dispatch = useDispatch();

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const postInviteMember = async (users) => {
    //   dispatch(
    //     createSuccessToast(
    //       `Invited ${newMembers.map((m) => m.username).join(", ")}`
    //     )
    //   );
    const param = {
        members:users,
        projectId:projectId
    };
    dispatch(addMembersToProject(param));
    handleClose();
    setTagsValue([]);
  };

  const handleClickInvite = () => {
    postInviteMember(tagsValue.map((v) => v.employeeId));
  };

  return (
    <>
      <InviteMember>
        <Button
          variant="outlined"
          size="small"
          css={css`
            text-transform: none;
          `}
          onClick={handleClick}
          aria-controls="member-invite-menu"
          aria-haspopup="true"
          data-testid="member-invite"
        >
          Invite
        </Button>
      </InviteMember>
      <Popover
        id="member-invite-menu"
        anchorEl={anchorEl}
        getContentAnchorEl={null}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "left",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "left",
        }}
        open={anchorEl}
        onClose={handleClose}
        transitionDuration={0}
      >
        <Content>
          <Description>Invite to this board</Description>
          <Box display="flex" alignItems="center">
            <UserSearch
              projectId={projectId}
              tagsValue={tagsValue}
              setTagsValue={setTagsValue}
            />
            <Button
              color="primary"
              variant="contained"
              css={css`
                font-size: 0.625rem;
                margin-left: 0.5rem;
              `}
              onClick={handleClickInvite}
              data-testid="invite-selected"
              disabled={tagsValue.length === 0}
            >
              Invite
            </Button>
          </Box>
        </Content>
      </Popover>
    </>
  );
};

export default MemberInvite;
