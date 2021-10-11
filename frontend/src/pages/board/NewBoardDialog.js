import React, { useState } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  DialogContentText,
  TextField,
  Button,
} from "@material-ui/core";
import { useDispatch, useSelector } from "react-redux";
import { Alert } from "@material-ui/lab";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import { boardCardBaseStyles } from "./Styles";
import { getMetaKey } from "./const";
import {
  createNewProject,
  projectOwner,
  setCreateDialogOpen,
} from "./ProjectSlice";
import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { Formik } from "formik";

const openBtnStyles = css`
  ${boardCardBaseStyles}
  background-color: #e0e2e5;
  color: #333;
  width: 100%;
  font-size: 0.7rem;

  display: flex;
  align-items: center;
  justify-content: center;

  &:hover {
    background-color: #d0d2d5;
  }
`;

const NewBoardDialog = ({ setDialogOpen }) => {
  const dispatch = useDispatch();
  const error = useSelector((state) => state.project.error);
  const open = useSelector((state) => state.project.createDialogOpen);
  const profile = useSelector((state) => state.profile.userDetail);
  const { register, handleSubmit, errors, reset } = useForm();

  const handleOpen = () => {
    reset();
    dispatch(setCreateDialogOpen(true));
  };

  const handleClose = () => {
    dispatch(setCreateDialogOpen(false));
  };

  const onSubmit = (values) => console.log(values);

  return (
    <div>
      <Button css={openBtnStyles} onClick={handleOpen}>
        Create new Project
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="new-board"
        fullWidth
        maxWidth="xs"
      >
        <DialogTitle id="new-project-title">New Project</DialogTitle>
        <Formik
          initialValues={{
            name: "",
            description: "",
            employeeLeaderId: profile.employeeId,
          }}
          validationSchema={Yup.object().shape({
            name: Yup.string().max(255).required("Project Name is required"),
          })}
          onSubmit={(values, { setSubmitting }) => {
            setTimeout(() => {
              dispatch(createNewProject(values));
              setSubmitting(false);
            }, 500);
          }}
        >
          {({
            errors,
            handleBlur,
            handleChange,
            handleSubmit,
            isSubmitting,
            touched,
            values,
          }) => (
            <form onSubmit={handleSubmit}>
              <DialogContent>
                <DialogContentText>
                  Create a new private project. Only members of the project will
                  be able to see and edit it.
                </DialogContentText>
                <TextField
                  autoFocus
                  onChange={handleChange}
                  value={values.name}
                  margin="dense"
                  id="project-name"
                  label="Project name"
                  fullWidth
                  name="name"
                />
              </DialogContent>
              <DialogActions>
                <Button
                  type="submit"
                  color="primary"
                  data-testid="create-project-btn"
                >
                  Create Project
                </Button>
              </DialogActions>
            </form>
          )}
        </Formik>
      </Dialog>
    </div>
  );
};

export default NewBoardDialog;
