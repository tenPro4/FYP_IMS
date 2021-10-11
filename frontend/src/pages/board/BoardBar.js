import React from "react";
import styled from "@emotion/styled";
import { useSelector, useDispatch } from "react-redux";
import { barHeight } from "./const";
import { AvatarGroup } from "@material-ui/lab";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import { avatarStyles } from "./Styles";
import MemberDetail from "components/boardMember/MemberDetail";
import MemberDialog from "components/boardMember/MemberDialog";
import EditTaskDialog from "components/task/EditTaskDialog";
import CreateTaskDialog from "components/task/CreateTaskDialog";
import { Button } from "@material-ui/core";
import { PRIMARY } from "shared/utils/colors";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faPen } from "@fortawesome/free-solid-svg-icons";
import { useParams } from "react-router-dom";
// import MemberListDialog from "features/member/MemberList";
// import MemberFilter from "features/member/MemberFilter";
import {
  setMemberListOpen,
} from "components/boardMember/ProjectMemberSlice";
import MemberInvite from 'components/boardMember/MemberInvite';
import MemberListDialog from 'components/boardMember/MemberList';
import {addColumn} from 'components/column/ProjectColumnSlice';
import {setEditDialogOpen} from 'pages/board/ProjectSlice';
import EditProjectDialog from "pages/board/EditProjectDialog";

const Container = styled.div`
  height: ${barHeight}px;
  display: flex;
  margin-left: 0.3rem;
  margin-right: 0.3rem;
  font-weight: bold;
  font-size: 1.25rem;
`;

const Items = styled.div`
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  overflow-x: scroll;
`;

const Left = styled.div`
  white-space: nowrap;
  display: flex;
  margin-right: 1rem;
`;

const Right = styled.div`
  white-space: nowrap;
`;

const Name = styled.div`
  color: #6869f6;
`;

const buttonStyles = css`
  color: ${PRIMARY};
  .MuiButton-iconSizeSmall > *:first-of-type {
    font-size: 12px;
  }
`;

const BoardBar = () => {
  const dispatch = useDispatch();
  const members = useSelector((state) => state.projectMember.memberList);
  const error = useSelector((state) => state.project.errors);
  const detail = useSelector((state) => state.project.detail);
  const user = useSelector((state) => state.profile.userDetail);
  const owner = user !== null
  ? detail.employeeLeaderId === user.employeeId
  : false;
  const { id } = useParams();

  if (detail.projectId != id || error || detail === null || user === null) {
    return null;
  }

  const handleAddColumn = () => {
    dispatch(addColumn(detail.projectId));
  };

  const handleEditProject =() =>{
    dispatch(setEditDialogOpen(true));
  }

  return (
    <Container>
      <Items>
        <Left>
          <Name>{detail.name}</Name>
          <AvatarGroup
            max={3}
            data-testid="member-group"
            css={css`
              margin-left: 1.5rem;
              & .MuiAvatarGroup-avatar {
                ${avatarStyles}
                border: none;
              }
              &:hover {
                cursor: pointer;
              }
            `}
            onClick={(e) => {
              if (e.target.classList.contains("MuiAvatarGroup-avatar")) {
                dispatch(setMemberListOpen(true));
              }
            }}
          >
            {members.map((member) => (
              <MemberDetail
                key={member.employeeId}
                member={member}
                isOwner={detail.employeeLeaderId === member.employeeId}
              />
            ))}
          </AvatarGroup>
          {owner && <MemberInvite projectId={detail.projectId} />}
              {/* <MemberFilter boardId={detail.id} /> */}
        </Left>
        <Right>
        <Button
            size="small"
            css={css`
              ${buttonStyles}
              margin-right: 0.5rem;
              font-weight: 600;
            `}
            onClick={handleEditProject}
            startIcon={<FontAwesomeIcon icon={faPen} />}
            data-testid="open-labels-dialog"
          >
            Edit Project
          </Button>
          <Button
            size="small"
            css={css`
              ${buttonStyles}
              font-weight: 600;
            `}
            onClick={handleAddColumn}
            startIcon={<FontAwesomeIcon icon={faPlus} />}
            data-testid="add-col"
          >
            Add Column
          </Button>
        </Right>
      </Items>
      <MemberDialog project={detail} />
      <MemberListDialog />
      <EditTaskDialog />
      <EditProjectDialog/>
      <CreateTaskDialog />
    </Container>
  );
};

export default BoardBar;
