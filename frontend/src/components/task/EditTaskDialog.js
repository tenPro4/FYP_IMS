import React, { useEffect, useState, useRef } from "react";
import {
  Dialog,
  TextField,
  Button,
  CircularProgress,
  TextareaAutosize,
  useTheme,
  useMediaQuery,
} from "@material-ui/core";
import { Autocomplete } from "@material-ui/lab";
import { useSelector, useDispatch } from "react-redux";
import styled from "@emotion/styled";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faRocket,
  faTrash,
  faLock,
  faAlignLeft,
  faArrowUp,
} from "@fortawesome/free-solid-svg-icons";
import { PRIMARY, TASK_G } from "shared/utils/colors";
import MarkdownIt from "markdown-it";
import MdEditor from "react-markdown-editor-lite";
import TaskAssignees from "./TaskAssignees";
import {
  patchTask,
  deleteTask,
  setEditDialogOpen,
} from "./TaskSlice";
import {
  PRIORITY_OPTIONS,
  TYPE_OPTIONS,
  PRIORITY_2,
  TYPE_2,
  borderRadius,
  PRIORITY_MAP,
  TYPE_MAP,
  MD_EDITOR_PLUGINS,
  MD_EDITING_CONFIG,
  MD_READ_ONLY_CONFIG,
  MD_EDITOR_CONFIG,
  taskDialogHeight,
  taskSideWidth,
  Key,
  getMetaKey,
} from "pages/board/const";
import { createMdEditorStyles, descriptionStyles } from "pages/board/Styles";
import AvatarTag from "components/AvatarTag";
import AvatarOption from "components/AvatarOption";
import PriorityOption from "components/PriorityOption";
import LabelChip from "components/task/LabelChip";
import Close from "components/boardMember/Close";
import { formatDistanceToNow } from "date-fns";
import "react-markdown-editor-lite/lib/index.css";
import {
  selectAllColumns,
  selectColumnsEntities,
} from "components/column/ProjectColumnSlice";
import CommentSection from "components/comment/CommentSection";

const mdParser = new MarkdownIt({ breaks: true });

//#region style

const Main = styled.div`
  width: 60%;
`;

const Side = styled.div`
  margin-top: 2rem;
  ${(props) => props.theme.breakpoints.up("sm")} {
    max-width: ${taskSideWidth}px;
    min-width: ${taskSideWidth}px;
  }
`;

const Header = styled.div`
  color: ${TASK_G};
  height: 2rem;
  h3 {
    margin: 0 0.25rem 0 0;
  }
`;

const Title = styled.div`
  display: flex;
  align-items: center;
  margin-bottom: 1rem;
  color: ${PRIMARY};
  font-size: 1rem;
  textarea {
    color: ${PRIMARY};
    font-weight: bold;
    font-size: 20px;
    width: 100%;
    margin: 0 2rem 0 0.375rem;
    border: none;
    resize: none;
    &:focus {
      outline: none;
      border-radius: ${borderRadius}px;
      box-shadow: inset 0 0 0 2px ${PRIMARY};
    }
  }
`;

const DescriptionHeader = styled.div`
  display: flex;
  align-items: center;
  h3 {
    margin: 0 0 0 12px;
  }
`;

const Description = styled.div`
  ${descriptionStyles}
`;

const DescriptionActions = styled.div`
  display: flex;
`;

const Text = styled.p`
  color: #626e83;
  margin: 4px 0;
  font-size: 12px;
`;

const ButtonsContainer = styled.div`
  display: flex;
  margin-top: 1rem;
  flex-direction: column;
  align-items: flex-start;
`;

const DESCRIPTION_PLACEHOLDER = "Write here...";

const DialogTitle = styled.h3`
  color: ${PRIMARY};
  margin-top: 0;
`;

const Content = styled.div`
  display: flex;
  justify-content: space-between;
  padding: 2rem;
`;

const EditorWrapper = styled.div`
  margin: 1rem 0;
  margin-right: 2rem;
  ${(props) => createMdEditorStyles(props.editing)};

  .rc-md-editor {
    min-height: ${(props) => (props.editing ? 180 : 32)}px;
    border: none;
    .section-container {
      ${(props) =>
        props.editing &&
        `
        outline: none;
        box-shadow: inset 0 0 0 2px ${PRIMARY};
      `};
      padding: ${(props) => (props.editing ? "8px" : "0px")} !important;
      &.input {
        line-height: 20px;
      }
    }
  }
`;

//#endregion

const EditTaskDialog = () => {
  const theme = useTheme();
  const dispatch = useDispatch();
  const taskId = useSelector((state) => state.projectTask.editDialogOpen);
  const open = taskId !== null;
  const tasksByColumn = useSelector((state) => state.projectTask.byColumn);
  const tasksById = useSelector((state) => state.projectTask.byId);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const xsDown = useMediaQuery(theme.breakpoints.down("xs"));
  const titleTextAreaRef = useRef(null);
  const task = tasksById[taskId];
  const [editingDescription, setEditingDescription] = useState(false);
  const wrapperRef = useRef(null);
  const editorRef = useRef(null);
  const cancelRef = useRef(null);

  useEffect(() => {
    if (taskId && tasksById[taskId]) {
      setDescription(tasksById[taskId].description);
      setTitle(tasksById[taskId].title);
    }
  }, [open, taskId]);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (
        wrapperRef.current &&
        !wrapperRef.current.contains(event.target) &&
        cancelRef.current &&
        !cancelRef.current?.contains(event.target)
      ) {
        handleSaveDescription();
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [wrapperRef, taskId, description]);

  useEffect(() => {
    if (editingDescription && editorRef && editorRef.current) {
      editorRef.current.setSelection({
        start: 0,
        end: description.length,
      });
    }
  }, [editingDescription]);

  if (taskId === null) {
    return null;
  }

  const handleClose = () => {
    dispatch(setEditDialogOpen(null));
    setEditingDescription(false);
  };

  const handleKeyDown = (e) => {
    // don't listen for input when inputs are focused
    if (document.activeElement || document.activeElement) {
      return;
    }

    if (e.key === "Backspace" && e.metaKey) {
      // handleDelete();
    }

    if (e.key === "Escape" && e.metaKey) {
      handleClose();
    }

    if (e.key === "l" && e.metaKey) {
      e.preventDefault();
      // handleNotImplemented();
    }
  };

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
  };

    const handleSaveTitle = () => {
    if (taskId) {
      const param ={
        id:taskId,
        title:title
      };
      dispatch(patchTask(param));
    }
  };

  const handleTitleKeyDown = (e) => {
    if (e.keyCode === Key.Enter) {
      e.preventDefault();
      titleTextAreaRef?.current?.blur();
    }
    if (e.keyCode === Key.Escape) {
      // Prevent propagation from reaching the Dialog
      e.stopPropagation();
    }
  };

  const handleDescriptionClick = () => {
    setEditingDescription(true);
  };

  const handleEditorKeyDown = (e) => {
    if (e.keyCode == Key.Enter && e.metaKey) {
      handleSaveDescription();
    }
    if (e.keyCode === Key.Escape) {
      // Prevent propagation from reaching the Dialog
      e.stopPropagation();
      handleCancelDescription();
    }
  };

  
  const handleSaveDescription = () => {
    if (taskId) {
      // dispatch(patchTask({ id: taskId, fields: { description } }));
      const param ={
        id:taskId,
        description:description
      };
      dispatch(patchTask(param));
      setEditingDescription(false);
    }
  };

  const handleCancelDescription = () => {
    if (taskId && tasksById[taskId]) {
      setDescription(tasksById[taskId].description);
      setEditingDescription(false);
    }
  };

  const handleEditorChange = ({ text }) => {
    setDescription(text);
  };

  const handlePriorityChange = (_, priority) => {
    if (priority) {
      const param ={
        id:task.taskId,
        taskPriority:priority.value
      };
      dispatch(patchTask(param));
    }
  };

  const handleTypeChange = (_,type) =>{
    if(type){
      const param ={
        id:task.taskId,
        taskType:type.value
      };
      dispatch(patchTask(param));
    }
  }

  const handleDelete = () => {
    if (window.confirm("Are you sure? Deleting a task cannot be undone.")) {
      dispatch(deleteTask(taskId));
      handleClose();
    }
  };

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      onKeyDown={handleKeyDown}
      fullWidth
      keepMounted={false}
      fullScreen={xsDown}
      css={css`
        .MuiDialog-paper {
          max-width: 920px;
        }
      `}
    >
      <Content theme={theme}>
        <Close onClose={handleClose} />
        <Main>
          <Header>id: {task.taskId}</Header>
          <Title>
            <FontAwesomeIcon icon={faArrowUp} />
            <TextareaAutosize
              ref={titleTextAreaRef}
              value={title}
              onChange={handleTitleChange}
              onBlur={handleSaveTitle}
              onKeyDown={handleTitleKeyDown}
              data-testid="task-title"
            />
          </Title>
          <DescriptionHeader>
            <FontAwesomeIcon icon={faAlignLeft} />
            <h3>Description</h3>
          </DescriptionHeader>
          <Description
            key={`${taskId}${editingDescription}`}
            data-testid="task-description"
          >
            <EditorWrapper
              onDoubleClick={
                editingDescription ? undefined : handleDescriptionClick
              }
              editing={editingDescription}
              ref={wrapperRef}
              theme={theme}
              onKeyDown={handleEditorKeyDown}
            >
              <MdEditor
                ref={editorRef}
                plugins={MD_EDITOR_PLUGINS}
                config={
                  editingDescription ? MD_EDITING_CONFIG : MD_READ_ONLY_CONFIG
                }
                value={
                  editingDescription
                    ? description
                    : description || DESCRIPTION_PLACEHOLDER
                }
                renderHTML={(text) => mdParser.render(text)}
                onChange={handleEditorChange}
                placeholder={DESCRIPTION_PLACEHOLDER}
              />
            </EditorWrapper>
            {editingDescription && (
              <DescriptionActions>
              <Button
                  variant="contained"
                  data-testid="save-description"
                  onClick={handleSaveDescription}
                  color="primary"
                  size="small"
                  style={{
                    margin:5
                  }}
                >
                  Save ({getMetaKey()}+⏎)
                </Button>
                <Button
                  variant="outlined"
                  data-testid="cancel-description"
                  onClick={handleCancelDescription}
                  ref={cancelRef}
                  size="small"
                  style={{
                    margin:5
                  }}
                >
                  Cancel (Esc)
                </Button>
              </DescriptionActions>
            )}
          </Description>
          <CommentSection taskId={taskId} />
        </Main>
        <Side theme={theme}>
        <TaskAssignees task={task} />
        <Autocomplete
            id="priority-select"
            size="small"
            blurOnSelect
            autoHighlight
            options={PRIORITY_OPTIONS}
            getOptionLabel={(option) => option.label}
            value={
              PRIORITY_MAP[task.taskPriority] !== undefined
              ? PRIORITY_MAP[task.taskPriority]
              : PRIORITY_OPTIONS[0]
              }
            onChange={handlePriorityChange}
            renderInput={(params) => (
              <TextField {...params} label="Priority" variant="outlined" />
            )}
            renderOption={(option) => <PriorityOption option={option} />}
            openOnFocus
            disableClearable
            data-testid="edit-priority"
            css={css`
              width: 100%;
              margin-top: 1rem;
            `}
          />
          <Autocomplete
          id="type-select"
          size="small"
          autoHighlight
          options={TYPE_OPTIONS}
          getOptionLabel={(option) => option.label}
          value={
            TYPE_MAP[task.taskType] !== undefined
              ? TYPE_MAP[task.taskType]
              : TYPE_OPTIONS[0]
          }
          onChange={handleTypeChange}
          renderOption={(option) => <LabelChip option={option} size="small" />}
          renderInput={(params) => (
            <TextField {...params} label="Type" variant="outlined" />
          )}
          openOnFocus
          disableClearable
          css={css`
            width: 100%;
            margin-top: 1rem;
          `}
        />
          <ButtonsContainer>
          <Button
              startIcon={<FontAwesomeIcon fixedWidth icon={faTrash} />}
              onClick={handleDelete}
              data-testid="delete-task"
              size="small"
              css={css`
                font-size: 12px;
                font-weight: bold;
                color: ${TASK_G};
                margin-bottom: 2rem;
              `}
            >
              Delete task ({getMetaKey()}+⌫)
            </Button>
          </ButtonsContainer>
          <Text>
            Updated {formatDistanceToNow(new Date(task.dateUpdated))} ago
          </Text>
          <Text
            css={css`
              margin-bottom: 1rem;
            `}
          >
            Created {formatDistanceToNow(new Date(task.dateCreated))} ago
          </Text>
        </Side>
      </Content>
    </Dialog>
  );
};

export default EditTaskDialog;
