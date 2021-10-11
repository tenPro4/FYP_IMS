import React from "react";
/** @jsx jsx */
import { jsx } from "@emotion/core";
import styled from "@emotion/styled";
import { DragDropContext, Droppable } from "react-beautiful-dnd";
import Column from "components/column/Column";
import reorder, { reorderTasks } from "shared/utils/reorder";
import { useSelector, useDispatch } from "react-redux";
import { useParams } from "react-router-dom";
import Preloader from "components/Preloader";
import SEO from "components/SEO";
import { barHeight, sidebarWidth } from "./const";
import BoardBar from "./BoardBar";
import { fetchProjectDetail } from "./ProjectSlice";
import {
  columnSelectors,
  updateColumns,
} from "components/column/ProjectColumnSlice";
import { sortTask } from "components/task/TaskSlice";

const BoardContainer = styled.div`
  min-width: calc(100vw - ${sidebarWidth});
  min-height: calc(100vh - ${barHeight * 2}px);
  overflow-x: scroll;
  display: flex;
`;

const ColumnContainer = styled.div`
  display: inline-flex;
  width: 100%;
`;

const EmptyBoard = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 50px;
`;

const ColumnsBlock = styled.div``;

const RightMargin = styled.div`
  /* 
  With overflow-x the right-margin of the rightmost column is hidden.
  This is a dummy element that fills up the space to make it 
  seem like there's some right margin.
   */
  &:after {
    content: "";
    display: block;
    width: 0.5rem;
  }
`;

const Board = () => {
  const detail = useSelector((state) => state.project.detail);
  const error = useSelector((state) => state.project.errors);
  const columns = useSelector(columnSelectors.selectAll);
  const tasksByColumn = useSelector((state) => state.projectTask.byColumn);
  const tasksById = useSelector((state) => state.projectTask.byId);
  const dispatch = useDispatch();
  const { id } = useParams();

  React.useEffect(() => {
    if (id) {
      dispatch(fetchProjectDetail(id));
    }
  }, [id]);

  const onDragEnd = (result) => {
    // dropped nowhere
    if (!result.destination) {
      return;
    }

    const source = result.source;
    const destination = result.destination;

    // did not move anywhere - can bail early
    if (
      source.droppableId === destination.droppableId &&
      source.index === destination.index
    ) {
      return;
    }

    // reordering column
    if (result.type === "COLUMN") {
      const newOrdered = reorder(columns, source.index, destination.index);
      // console.log(newOrdered);
      // console.log(newOrdered.map((col) => col.id));
      const param = {
        id: detail.projectId,
        columns: newOrdered,
      };

      dispatch(updateColumns(param));
      return;
    }

    const current = [...tasksByColumn[source.droppableId]];
    const next = [...tasksByColumn[destination.droppableId]];
    const target = current[source.index];

    // moving to same list
    if (source.droppableId === destination.droppableId) {
      const reordered = reorder(current, source.index, destination.index);
      const newOder = {
        ...tasksByColumn,
        [source.droppableId]: reordered,
      };
      const columnParam = [];
      columns.map((col) => {
        const getColumn = {
          Id: col.id,
          Ids: newOder[col.id].map((taskId) => taskId),
        };
        columnParam.push(getColumn);
      });

      const param = {
        projectColumns: columnParam,
        order: Object.values(newOder).flat(),
        updateOrder: newOder,
      };
      dispatch(sortTask(param));
      return;
    }

    // moving to different list

    // remove from original
    current.splice(source.index, 1);
    // insert into next
    next.splice(destination.index, 0, target);

    const newOder = {
      ...tasksByColumn,
      [source.droppableId]: current,
      [destination.droppableId]: next,
    };

    const columnParam = [];
    columns.map((col) => {
      const getColumn = {
        Id: col.id,
        Ids: newOder[col.id].map((taskId) => taskId),
      };
      columnParam.push(getColumn);
    });

    const param = {
      projectColumns: columnParam,
      order: Object.values(newOder).flat(),
      updateOrder: newOder,
    };
    dispatch(sortTask(param));
  };

  if (error) {
    console.log(error);
  }

  if (detail === null || detail.projectId != id) {
    return <Preloader show={true} />;
  }

  return (
    <>
      <BoardBar />
      <SEO title={detail?.name} />
      {columns.length !== 0 ? (
        <BoardContainer data-testid="board-container">
          <ColumnsBlock>
            <DragDropContext onDragEnd={onDragEnd}>
              <Droppable
                droppableId="board"
                type="COLUMN"
                direction="horizontal"
              >
                {(provided) => (
                  <ColumnContainer
                    ref={provided.innerRef}
                    {...provided.droppableProps}
                  >
                    {columns.map((column, index) => {
                      return(
                      <Column
                        key={column.id}
                        id={column.id}
                        title={column.title}
                        index={index}
                        tasks={
                          tasksByColumn[column.id] !== null && tasksByColumn[column.id] !== undefined
                          ? tasksByColumn[column.id].map(
                          (taskId) => tasksById[taskId]
                        )
                        : []
                        }
                      />
                      )}
                    )}
                    {provided.placeholder}
                  </ColumnContainer>
                )}
              </Droppable>
            </DragDropContext>
          </ColumnsBlock>
          <RightMargin />
        </BoardContainer>
      ) : (
        <EmptyBoard>This board is empty.</EmptyBoard>
      )}
    </>
  );
};

export default Board;
