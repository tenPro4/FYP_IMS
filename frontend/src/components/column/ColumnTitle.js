import React, { useState, useRef, useEffect } from "react";
import styled from "@emotion/styled";
import {
  grid,
  borderRadius,
  Key,
  taskHeaderTextareaWidth,
} from "pages/board/const";
import { P100, PRIMARY, TASK_G as ACTION_G } from "shared/utils/colors";
import { TextareaAutosize, Button, Popover } from "@material-ui/core";
import { useDispatch } from "react-redux";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEllipsisV, faTrash } from "@fortawesome/free-solid-svg-icons";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import {patchColumnTitle,deleteColumn} from './ProjectColumnSlice';

const Container = styled.h4`
  padding: ${grid}px;
  transition: background-color ease 0.2s;
  flex-grow: 1;
  user-select: none;
  position: relative;
  color: ${PRIMARY};
  font-size: 1rem;
  font-weight: bold;
  margin: 0;
  margin-top: 0.5rem;
  display: flex;
  justify-content: space-between;
  min-height: 40px;
  &:focus {
    outline: 2px solid ${P100};
    outline-offset: 2px;
  }
  textarea {
    color: ${PRIMARY};
    font-weight: bold;
    width: ${taskHeaderTextareaWidth}px;
    border: none;
    resize: none;
    border-radius: ${borderRadius}px;
    box-shadow: inset 0 0 0 1px #ccc;
    line-height: 1.43;
    &:focus {
      outline: none;
      box-shadow: inset 0 0 0 2px ${PRIMARY};
    }
  }
`;

const InputTitle = styled.div`
  display: flex;
  align-items: center;
`;

const RegularTitle = styled.div`
  margin: 0;
  font-size: 14px;
  align-self: center;
  word-break: break-word;
  width: ${taskHeaderTextareaWidth}px;
  &:hover {
    cursor: pointer;
  }
`;

const Extra = styled.div`
  display: flex;
  align-items: flex-start;
`;

const InnerExtra = styled.div`
  display: flex;
  align-items: center;
`;

const Count = styled.div`
  overflow-wrap: anywhere;
  font-size: 14px;
`;

const OptionsContent = styled.div`
  padding: 0.75rem;
`;

const ColumnTitle = ({ id, title, tasksCount, ...props }) => {
  const dispatch = useDispatch();
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [pendingTitle, setPendingTitle] = useState(title);
  const [editing, setEditing] = useState(false);
  const titleTextAreaRef = useRef(null);

  useEffect(() => {
    if (!editing && title === pendingTitle) {
      titleTextAreaRef?.current?.blur();
    }
  }, [pendingTitle, editing]);

  const handleKeyDown = (e) => {
    if (e.keyCode === Key.Enter) {
      e.preventDefault();
      if (pendingTitle.length > 0) {
        titleTextAreaRef?.current?.blur();
      }
    }
    if (e.keyCode === Key.Escape) {
      e.preventDefault();
      setPendingTitle(title);
      setEditing(false);
      // blur via useEffect
    }
  };

  const handleSave = () => {
    if (editing && pendingTitle.length > 0) {
      setEditing(false);
      if (pendingTitle !== title) {
        const param = {
          id:id,
          title:pendingTitle
        };
        dispatch(patchColumnTitle(param));
      }
    }
  };

  const handleChange = (e) => {
    setPendingTitle(e.target.value);
  };

  const handleFocus = (e) => {
    e.target.select();
  };

  const handleOptionsClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleOptionsClose = () => {
    setAnchorEl(null);
  };

  const handleDelete = () => {
    if (
      window.confirm(
        "Are you sure? Deleting the column will also delete related tasks and this cannot be undone."
      )
    ) {
      dispatch(deleteColumn(id));
      handleOptionsClose();
    }
  };

  const open = anchorEl;
  const popoverId = open ? `col-${id}options-popover` : undefined;

  return (
    <Container {...props}>
      {editing ? (
        <InputTitle>
          <TextareaAutosize
            ref={titleTextAreaRef}
            value={pendingTitle}
            onChange={handleChange}
            onBlur={handleSave}
            onKeyDown={handleKeyDown}
            data-testid="column-title-textarea"
            onFocus={handleFocus}
            autoFocus
          />
        </InputTitle>
      ) : (
        <RegularTitle onClick={() => setEditing(true)}>
          {pendingTitle}
        </RegularTitle>
      )}
      <Extra>
        <InnerExtra>
          <Count>{tasksCount}</Count>
          <Button
            onClick={handleOptionsClick}
            data-testid="col-options"
            css={css`
              margin-left: 0.25rem;
              min-width: 0;
              padding: 2px 8px;
              height: 22px;
            `}
          >
            <FontAwesomeIcon icon={faEllipsisV} />
          </Button>
        </InnerExtra>
        <Popover
          id={popoverId}
          open={open}
          anchorEl={anchorEl}
          onClose={handleOptionsClose}
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "left",
          }}
          transformOrigin={{
            vertical: "top",
            horizontal: "left",
          }}
        >
          <OptionsContent>
            <Button
              startIcon={<FontAwesomeIcon fixedWidth icon={faTrash} />}
              onClick={handleDelete}
              data-testid="delete-column"
              size="small"
              css={css`
                font-size: 12px;
                font-weight: bold;
                color: ${ACTION_G};
              `}
            >
              Delete column
            </Button>
          </OptionsContent>
        </Popover>
      </Extra>
    </Container>
  );
};

export default ColumnTitle;
