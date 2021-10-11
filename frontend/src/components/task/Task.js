import React from "react";
import styled from "@emotion/styled";
import {  Draggable} from "react-beautiful-dnd";
import { N30, N0, N70, PRIMARY } from "shared/utils/colors";
import { PRIO_COLORS } from "pages/board/const";
import { taskContainerStyles } from "pages/board/Styles";
import { AvatarGroup } from "@material-ui/lab";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import { useSelector, useDispatch } from "react-redux";
import { Avatar } from "@material-ui/core";
import TaskLabels from "./TaskLabels";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowUp } from "@fortawesome/free-solid-svg-icons";
import AssigneesData from "./assignees.json";
import {setEditDialogOpen} from './TaskSlice';

const getBackgroundColor = (isDragging, isGroupedOver) => {
  if (isDragging) {
    return "#eee";
  }

  if (isGroupedOver) {
    return N30;
  }

  return N0;
};

const getBorderColor = (isDragging) => (isDragging ? "orange" : "transparent");

const Container = styled.span`
  border-color: ${(props) => getBorderColor(props.isDragging)};
  background-color: ${(props) =>
    getBackgroundColor(props.isDragging, props.isGroupedOver)};
  box-shadow: ${({ isDragging }) =>
    isDragging ? `2px 2px 1px ${N70}` : "none"};

  &:focus {
    border-color: #aaa;
  }
`;

export const Content = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
`;

const TextContent = styled.div`
  position: relative;
  padding-right: 14px;
  word-break: break-word;
  color: ${PRIMARY};
  font-weight: bold;
  font-size: 12px;
`;

const Footer = styled.div`
  height: 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

const CardIcon = styled.div`
  display: flex;
  font-size: 0.75rem;
`;

const TaskId = styled.small`
  flex-grow: 1;
  flex-shrink: 1;
  margin: 0;
  font-weight: normal;
  text-overflow: ellipsis;
  text-align: left;
  font-weight: bold;
  color: #aaa;
  font-size: 8px;
`;

const Assignees = styled.div``;

const getStyle = (provided, style) => {
  if (!style) {
    return provided.draggableProps.style;
  }

  return {
    ...provided.draggableProps.style,
    ...style,
  };
};

export const TaskFooter = ({ task }) => {
  return (
    <Footer>
      <CardIcon data-testid="task-priority">
        <FontAwesomeIcon icon={faArrowUp} color={PRIO_COLORS[task.taskPriority]} />
      </CardIcon>
      {task.assignees !== null && (
        <Assignees>
          <AvatarGroup
            max={3}
            css={css`
              & .MuiAvatarGroup-avatar {
                height: 1.25rem;
                width: 1.25rem;
                font-size: 8px;
                margin-left: -4px;
                border: none;
              }
            `}
          >
            {task.assignees.map((assignee) => {
              const {employeeImage} = assignee.employee;
              const imageUrl = employeeImage !== null 
              ? employeeImage.imageHeader+employeeImage.imageBinary
              : '';
              return (
              <Avatar
                key={assignee.employeeId}
                css={css`
                  height: 1.25rem;
                  width: 1.25rem;
                  font-size: 8px;
                  margin-left: -12px;
                `}
                src={imageUrl}
                alt={employeeImage !== null ?  employeeImage.imageName : ''}
              >
                {assignee.employee.firstName.charAt(0)}
              </Avatar>
              );
            })}
          </AvatarGroup>
        </Assignees>
      )}
    </Footer>
  );
};

const Task = ({ task, style, index }) => {
  const dispatch = useDispatch();

  console.log(task);

  const handleClick = () => {
    console.log(task.taskId);
    dispatch(setEditDialogOpen(task.taskId));
  };

  return (
    <Draggable key={task.taskId} draggableId={`task-${task.taskId}`} index={index}>
      {(
        dragProvided,dragSnapshot) => (
        <Container
          isDragging={dragSnapshot.isDragging}
          isGroupedOver={dragSnapshot.combineTargetFor}
          ref={dragProvided.innerRef}
          {...dragProvided.draggableProps}
          {...dragProvided.dragHandleProps}
          style={getStyle(dragProvided, style)}
          data-is-dragging={dragSnapshot.isDragging}
          data-testid={`task-${task.taskId}`}
          data-index={index}
          aria-label={`task ${task.title}`}
          onClick={handleClick}
          css={taskContainerStyles}
        >
          <Content>
            <TextContent>{task.title}</TextContent>
            <TaskId>id: {task.taskId}</TaskId>
            <TaskLabels task={task} />
            <TaskFooter task={task} />
          </Content>
        </Container>
      )}
    </Draggable>
  );
};

export default React.memo(Task);
