import React, { useEffect, useState } from "react";
import {
  Dialog,
  TextField,
  Button,
  CircularProgress,
  useTheme,
  useMediaQuery,
} from "@material-ui/core";
import { Autocomplete } from "@material-ui/lab";
import { useSelector, useDispatch } from "react-redux";
import styled from "@emotion/styled";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRocket } from "@fortawesome/free-solid-svg-icons";
import MarkdownIt from "markdown-it";
import MdEditor from "react-markdown-editor-lite";
import "react-markdown-editor-lite/lib/index.css";
import { setCreateDialogOpen, createTask } from "./TaskSlice";
import {
  PRIMARY,
  PRIORITY_OPTIONS,
  TYPE_OPTIONS,
  PRIORITY_2,
  TYPE_2,
  MD_EDITOR_PLUGINS,
  MD_EDITOR_CONFIG,
  Key,
  getMetaKey,
} from "pages/board/const";
import { createMdEditorStyles } from "pages/board/Styles";
import AvatarTag from "components/AvatarTag";
import AvatarOption from "components/AvatarOption";
import PriorityOption from "components/PriorityOption";
import LabelChip from "components/task/LabelChip";

const mdParser = new MarkdownIt();

const DialogTitle = styled.h3`
  color: ${PRIMARY};
  margin-top: 0;
`;

const Content = styled.div`
  display: flex;
  flex-direction: column;
  padding: 2rem;
`;

const EditorWrapper = styled.div`
  margin: 1rem 0;
  ${createMdEditorStyles(false)}
  .rc-md-editor {
    min-height: 160px;
  }
`;

const Footer = styled.div`
  display: flex;
  justify-content: flex-end;
  border-top: 1px solid #ccc;
  padding: 1rem 2rem;
`;

const CreateTaskDialog = () => {
  const theme = useTheme();
  const dispatch = useDispatch();
  // const labelsOptions = useSelector(selectAllLabels);
  const members = useSelector((state) => state.projectMember.memberList);
  const open = useSelector((state) => state.projectTask.createDialogOpen);
  const columnId = useSelector((state) => state.projectTask.createDialogColumn);
  const createLoading = useSelector((state) => state.projectTask.createLoading);
  const [titleTouched, setTitleTouched] = useState(false);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [assignees, setAssignees] = useState([]);
  const [priority, setPriority] = useState({
    value: "2",
    label: "Medium",
  });
  const [type, setType] = useState({
    value: "2",
    label: "Bug",
  });
  const xsDown = useMediaQuery(theme.breakpoints.down("xs"));

  const handleEditorChange = ({ text }) => {
    setDescription(text);
  };

  const setInitialValues = () => {
    if (columnId) {
      setTitleTouched(false);
      setTitle("");
      setDescription("");
      setAssignees([]);
      setPriority(PRIORITY_2);
      setType(TYPE_2);
    }
  };

  useEffect(() => {
    setInitialValues();
  }, [open]);

  const handleClose = () => {
    if (window.confirm("Are you sure? Any progress made will be lost.")) {
      dispatch(setCreateDialogOpen(false));
    }
  };

  const handleCreate = async () => {
    setTitleTouched(true);
    if (columnId && priority) {
      const newTask = {
        title,
        description,
        id: columnId,
        taskType: type.value,
        assignees: assignees.map((a) => a.employeeId),
        taskPriority: priority.value,
      };
      dispatch(createTask(newTask));
    }
  };

  const handleKeyDown = (e) => {
    if (e.keyCode == Key.Enter && e.metaKey) {
      handleCreate();
    }
  };

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="sm"
      fullWidth
      keepMounted={false}
      fullScreen={xsDown}
    >
      <Content onKeyDown={handleKeyDown}>
        <DialogTitle>New issue</DialogTitle>
        <TextField
          autoFocus
          id="create-task-title"
          data-testid="create-task-title"
          label="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          variant="outlined"
          fullWidth
          size="small"
          onBlur={() => setTitleTouched(true)}
          error={titleTouched && !title}
        />

        <EditorWrapper>
          <MdEditor
            plugins={MD_EDITOR_PLUGINS}
            config={MD_EDITOR_CONFIG}
            value={description}
            renderHTML={(text) => mdParser.render(text)}
            onChange={handleEditorChange}
            placeholder="Describe the issue..."
          />
        </EditorWrapper>

        <Autocomplete
          multiple
          filterSelectedOptions
          disableClearable
          openOnFocus
          id="create-assignee-select"
          size="small"
          options={members}
          getOptionLabel={(option) => option.employee.firstName}
          value={assignees}
          onChange={(_event, value) => setAssignees(value)}
          renderOption={(option) => <AvatarOption option={option} />}
          renderInput={(params) => (
            <TextField {...params} label="Assignees" variant="outlined" />
          )}
          renderTags={(value, getTagProps) =>
            value.map((option, index) => (
              <AvatarTag
                key={option.employeeid}
                option={option.employee}
                {...getTagProps({ index })}
              />
            ))
          }
          css={css`
            width: 100%;
            margin-top: 1rem;
          `}
        />

        <Autocomplete
          id="create-priority-select"
          size="small"
          autoHighlight
          options={PRIORITY_OPTIONS}
          getOptionLabel={(option) => option.label}
          value={priority}
          onChange={(_, value) => setPriority(value)}
          renderOption={(option) => <PriorityOption option={option} />}
          renderInput={(params) => (
            <TextField {...params} label="Priority" variant="outlined" />
          )}
          openOnFocus
          disableClearable
          css={css`
            width: 100%;
            margin-top: 1rem;
          `}
        />

        <Autocomplete
          id="create-type-select"
          size="small"
          autoHighlight
          options={TYPE_OPTIONS}
          getOptionLabel={(option) => option.label}
          value={type}
          onChange={(_, value) => setType(value)}
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

        {/* <Autocomplete
            multiple
            id="create-labels-select"
            size="small"
            filterSelectedOptions
            autoHighlight
            openOnFocus
            options={labelsOptions}
            getOptionLabel={(option) => option.name}
            value={labels}
            onChange={(_, newLabels) => setLabels(newLabels)}
            renderInput={(params) => (
              <TextField {...params} label="Labels" variant="outlined" />
            )}
            renderTags={(value, getTagProps) =>
              value.map((option, index) => (
                <LabelChip
                  key={option.id}
                  label={option}
                  size="small"
                  {...getTagProps({ index })}
                />
              ))
            }
            renderOption={(option) => <LabelChip label={option} size="small" />}
            css={css`
              margin-top: 1rem;
              width: 100%;
            `}
          /> */}
      </Content>

      <Footer theme={theme}>
        <Button
          startIcon={
            createLoading ? (
              <CircularProgress color="inherit" size={16} />
            ) : (
              <FontAwesomeIcon icon={faRocket} />
            )
          }
          variant="contained"
          color="primary"
          size="small"
          onClick={handleCreate}
          disabled={createLoading}
          data-testid="task-create"
          css={css`
            ${theme.breakpoints.down("xs")} {
              flex-grow: 1;
            }
          `}
        >
          Create issue ({getMetaKey()}+⏎)
        </Button>
        <Button
          css={css`
            margin-left: 1rem;
          `}
          onClick={handleClose}
        >
          Cancel (Esc)
        </Button>
      </Footer>
    </Dialog>
  );
};

export default CreateTaskDialog;
