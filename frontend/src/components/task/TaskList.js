import React from "react";
import styled from "@emotion/styled";
import {R50,T50, COLUMN_COLOR} from 'shared/utils/colors';
import {grid,barHeight,taskWidth} from 'pages/board/const';
import {
    DroppableProvided,
    DroppableStateSnapshot,
    Droppable,
  } from "react-beautiful-dnd";
  import Task from "./Task";
  import AddTask from "./AddTask";
  /** @jsx jsx */
import { css,jsx } from "@emotion/core";

export const getBackgroundColor = (isDraggingOver,isDraggingFrom) => {
  if (isDraggingOver) {
    return R50;
  }
  if (isDraggingFrom) {
    return T50;
  }
  return COLUMN_COLOR;
};

const Wrapper = styled.div`
  background-color: ${(props) =>
    getBackgroundColor(props.isDraggingOver, props.isDraggingFrom)};
  display: flex;
  flex-direction: column;
  padding: ${grid}px;
  border: ${grid}px;
  padding-bottom: 0;
  transition: background-color 0.2s ease, opacity 0.1s ease;
  user-select: none;
  width: ${taskWidth}px;
`;

const scrollContainerHeight = 250;

const DropZone = styled.div`
  /* stop the list collapsing when empty */
  min-height: ${scrollContainerHeight}px;
  /*
    not relying on the items for a margin-bottom
    as it will collapse when the list is empty
  */
  padding-bottom: ${grid}px;
`;

const Container = styled.div``;


const InnerTaskList = ({ tasks }) => (
  <>
    {tasks.map((task, index) => (
      <Task key={task.taskId} task={task} index={index} />
    ))}
  </>
);

const InnerList = ({
  columnId,
  tasks,
  dropProvided,
  index,
}) => (
  <Container>
    <DropZone
      data-testid="drop-zone"
      ref={dropProvided.innerRef}
      css={css`
        max-height: calc(100vh - ${barHeight * 5}px);
        overflow-y: scroll;
      `}
    >
      <InnerTaskList tasks={tasks} />
      {dropProvided.placeholder}
    </DropZone>
    <AddTask columnId={columnId} index={index} />
  </Container>
);


const TaskList = ({ columnId, listType, tasks, index }) => {
  return (
  <Droppable droppableId={columnId.toString()} type={listType}>
    {(dropProvided,dropSnapshot) => (
      <Wrapper
        isDraggingOver={dropSnapshot.isDraggingOver}
        isDraggingFrom={(dropSnapshot.draggingFromThisWith)}
        {...dropProvided.droppableProps}
      >
        <InnerList
          columnId={columnId}
          tasks={tasks}
          dropProvided={dropProvided}
          index={index}
        />
      </Wrapper>
    )}
  </Droppable>
  )};

export default TaskList;
