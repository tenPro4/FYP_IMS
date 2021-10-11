/** @jsx jsx */
import {jsx} from "@emotion/core";
import styled from "@emotion/styled";
import ColumnTitle from './ColumnTitle';
import {grid} from 'pages/board/const';
import TaskList from 'components/task/TaskList';
import React from "react";
import {Draggable} from "react-beautiful-dnd";
import {COLUMN_COLOR} from 'shared/utils/colors';

const Container = styled.div`
  margin: ${grid / 2}px;
  display: flex;
  flex-direction: column;
  border-top: 3px solid #cfd3dc;
`;

const Header = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: ${COLUMN_COLOR};
  transition: background-color 0.2s ease;
  [data-rbd-drag-handle-context-id="0"] {
    cursor: initial;
  }
`;

const Column = ({ id, title, tasks, index }) => {
  return (
    <Draggable draggableId={`col-${id}`} index={index}>
      {(provided, snapshot) => (
        <Container
          ref={provided.innerRef}
          {...provided.draggableProps}
          data-testid={`col-${title}`}
        >
          <Header isDragging={snapshot.isDragging}>
            <ColumnTitle
              {...provided.dragHandleProps}
              id={id}
              title={title}
              tasksCount={tasks.length}
              aria-label={`${title} task list`}
              data-testid="column-title"
            />
          </Header>
          <TaskList columnId={id} listType="TASK" tasks={tasks} index={index} />
        </Container>
      )}
    </Draggable>
  );
};

export default Column;