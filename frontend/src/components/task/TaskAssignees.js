/** @jsx jsx */
import { css,jsx } from "@emotion/core";
import styled from "@emotion/styled";
import { Avatar, Button, Popover } from "@material-ui/core";
import AssigneeAutoComplete from "components/AssigneeAutoComplete";
import Close from "components/boardMember/Close";
import { modalPopperIndex, modalPopperWidth } from "pages/board/const";
import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { PRIMARY } from "shared/utils/colors";
import {patchTaskAssignee} from "./TaskSlice";

const Container = styled.div`
  margin-bottom: 1rem;
`;

const ContentTitle = styled.h3`
  margin: 0.5rem 1rem;
  margin-bottom: 0;
  font-size: 1rem;
  font-weight: normal;
`;

const Content = styled.div`
  border-bottom: 1px solid #e1e4e8;
  padding: 8px 0;
  width: ${modalPopperWidth}px;
`;

const AssigneeContainer = styled.div`
  padding: 16px;
`;

const Label = styled.p`
  color: #757575;
  margin: 0;
`;

const List = styled.div`
  display: flex;
  align-items: center;
  margin: 0.5rem 0;
  overflow-wrap: anywhere;
`;

const TaskAssignees = ({ task }) => {
    const dispatch = useDispatch();
    const [anchorEl, setAnchorEl] = useState(null);
    const [pendingAssignees, setPendingAssignees] = useState([]);
    const members = useSelector((state) => state.projectMember.memberList);
    const assignees = task.assignees;
    const optionMembers = [];
    const optionAssignees =[];

    if(assignees === null){
      return null;
    }

    members.map((member) =>{
      const temp ={
        employeeId:member.employeeId,
        employee:{
          firstName : member.employee.firstName,
          lastName : member.employee.lastName,
          employeeImage : member.employee.employeeImage,
        }
      }
      optionMembers.push(temp);
    });

    assignees.map((assignee) =>{
      const temp ={
        employeeId:assignee.employeeId,
        employee:{
          firstName : assignee.employee.firstName,
          lastName : assignee.employee.lastName,
          employeeImage : assignee.employee.employeeImage,
        }
      }
      optionAssignees.push(temp);
    });

    const handleTagsChange = (_event, newValues) => {
      setPendingAssignees(newValues);
    };

  const handleClick = (event) => {  
    setPendingAssignees(optionAssignees);
    console.log(pendingAssignees)
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    const currentIds = optionAssignees.map((a) => a.employeeId);
    const pendingIds = pendingAssignees.map((member) => member.employeeId);
    if (
      !(
        pendingIds.length === currentIds.length &&
        pendingIds
          .sort()
          .every((value, index) => value === currentIds.sort()[index])
      )
    ) {
      console.log(pendingAssignees)
      console.log(pendingIds);
      const param ={
        id:task.taskId,
        assignees:pendingIds
      }
      dispatch(patchTaskAssignee(param));
    }
    setAnchorEl(null);
  };

  const open = anchorEl;
  const popoverId = open ? "task-assignees-popover" : undefined;

  return (
    <Container>
      <Label>Assignees</Label>
      {optionAssignees.map((assignee) => (
        <List key={assignee.employeeId}>
          <div>
            <Avatar
              css={css`
                height: 2rem;
                width: 2rem;
                margin-right: 0.5rem;
              `}
              src={
                assignee.employee.employeeImage !== null
                ? assignee.employee.employeeImage.imageHeader+assignee.employee.employeeImage.imageBinary
                :''
                }
              alt={
                assignee.employee.employeeImage !== null
                ? assignee.employee.employeeImage.imageName
                :''
              }
            >
              {assignee.employee.firstName.charAt(0)}
            </Avatar>
          </div>
          <div>{assignee.employee.firstName} {assignee.employee.lastName}</div>
        </List>
      ))}
      <Button
        size="small"
        onClick={handleClick}
        data-testid="open-edit-assignees"
        css={css`
          color: ${PRIMARY};
          font-size: 0.7rem;
        `}
      >
        Change
      </Button>
      <Popover
        id={popoverId}
        open={open}
        anchorEl={anchorEl}
        transitionDuration={0}
        // style={{ zIndex: modalPopperIndex }}
        onClose={handleClose}
        css={css`
          .MuiPaper-rounded {
            border-radius: 0;
          }
        `}
      >
        <Content>
          <Close onClose={handleClose} onPopper />
          <ContentTitle>Assigned Task Members</ContentTitle>
          <AssigneeContainer>
            <AssigneeAutoComplete
              assignee={pendingAssignees}
              members={optionMembers}
              setAssignee={handleTagsChange}
              controlId="assignee-select"
            />
          </AssigneeContainer>
        </Content>
      </Popover>
    </Container>
  );
};

export default TaskAssignees;
