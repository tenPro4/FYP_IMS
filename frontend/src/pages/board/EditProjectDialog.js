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
import {
  PRIORITY_OPTIONS,
  TYPE_OPTIONS,
  STATUS_OPTIONS,
  PRIORITY_2,
  TYPE_2,
  borderRadius,
  PRIORITY_MAP,
  TYPE_MAP,
  STATUS_MAP,
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
import Close from "components/boardMember/Close";
import { formatDistanceToNow } from "date-fns";
import "react-markdown-editor-lite/lib/index.css";
import {setEditDialogOpen,updateProject} from './ProjectSlice';
import StatusChip from './StatusChip';
import axios from 'axios';
import { useHistory } from "react-router-dom";
import { Routes } from "routes";

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

const EditProjectDialog = () => {
  const theme = useTheme();
  const dispatch = useDispatch();
  const user = useSelector((state) => state.profile.userDetail);
  const detail = useSelector((state) => state.project.detail);
  const open = useSelector((state) => state.project.editDialogOpen);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const xsDown = useMediaQuery(theme.breakpoints.down("xs"));
  const titleTextAreaRef = useRef(null);
  const [editingDescription, setEditingDescription] = useState(false);
  const wrapperRef = useRef(null);
  const editorRef = useRef(null);
  const cancelRef = useRef(null);
  const history = useHistory();
  

  useEffect(() => {
    setDescription(detail.description);
    setTitle(detail.name);
  }, [open]);

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
  }, [wrapperRef, description]);

  useEffect(() => {
    if (editingDescription && editorRef && editorRef.current) {
      editorRef.current.setSelection({
        start: 0,
        end: description.length,
      });
    }
  }, [editingDescription]);

  if (detail === null) {
    return null;
  }

  const handleClose = () => {
    dispatch(setEditDialogOpen(false));
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
        var param = JSON.parse(JSON.stringify(detail));;
        param.name = title;
        dispatch(updateProject(param));
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
    var param = JSON.parse(JSON.stringify(detail));;
    param.description = description;
    dispatch(updateProject(param));
    setEditingDescription(false);
  };

  const handleCancelDescription = () => {
    setDescription(detail.description);
    setEditingDescription(false);
  };

  const handleEditorChange = ({ text }) => {
    setDescription(text);
  };

  const handleProjectChange = (_,status) =>{
    if(status){
        var param = JSON.parse(JSON.stringify(detail));;
        param.status = status.value;
        dispatch(updateProject(param));
    }
  }

  const handleDelete = async() => {
    if (window.confirm("Are you sure? Deleting this project cannot be undo.")) {
        await axios.delete(`/project/delete/${detail.projectId}`);
        handleClose();
        history.push(Routes.ProjectBoardList.path);
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
          <Title>
            <FontAwesomeIcon icon={faArrowUp} />
            <TextareaAutosize
              ref={titleTextAreaRef}
              value={title}
              onChange={handleTitleChange}
              onBlur={handleSaveTitle}
              onKeyDown={handleTitleKeyDown}
              data-testid="project-title"
            />
          </Title>
          <DescriptionHeader>
            <FontAwesomeIcon icon={faAlignLeft} />
            <h3>Description</h3>
          </DescriptionHeader>
          <Description
            key={`${detail.projectId}${editingDescription}`}
            data-testid="project-description"
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
        </Main>
        <Side theme={theme}>
          <Autocomplete
          id="status-select"
          size="small"
          autoHighlight
          options={STATUS_OPTIONS}
          getOptionLabel={(option) => option.label}
          value={
            STATUS_MAP[detail.status] !== undefined
            ? STATUS_MAP[detail.status]
            : STATUS_OPTIONS[0]
            }
          onChange={handleProjectChange}
          renderOption={(option) => <StatusChip option={option} size="small" />}
          renderInput={(params) => (
            <TextField {...params} label="Status" variant="outlined" />
          )}
          openOnFocus
          disableClearable
          css={css`
            width: 100%;
            margin-top: 1rem;
          `}
        />
          <ButtonsContainer>
          {user.employeeId === detail.employeeLeaderId && 
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
              Delete Project ({getMetaKey()}+⌫)
            </Button>
          }
          </ButtonsContainer>
          <Text>
            Updated {formatDistanceToNow(new Date(detail.dateUpdated))} ago
          </Text>
          <Text
            css={css`
              margin-bottom: 1rem;
            `}
          >
            Created {formatDistanceToNow(new Date(detail.dateCreated))} ago
          </Text>
        </Side>
      </Content>
    </Dialog>
  );
};

export default EditProjectDialog;
