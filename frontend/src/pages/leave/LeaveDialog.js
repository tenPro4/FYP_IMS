import React, { useState, useEffect } from "react";
import {
  Dialog,
  Avatar,
  Button,
  Fab,
  Card,
  CardContent,
  Grid,
  useMediaQuery,
  TextField,
  useTheme,
  DialogTitle,
  InputLabel,
  FormControl,
  Box,
  Divider,
  MenuItem,
  Select
} from "@material-ui/core";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import { setDetailDialogOpen,addLeaveRequest,updateLeaveRequest } from "./LeaveSlice";
import Close from "components/boardMember/Close";
import { useDispatch, useSelector } from "react-redux";
import Input from "@material-ui/icons/Input";
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
} from "@material-ui/pickers";
import MomentUtils from "@date-io/moment";
import DateFnsUtils from "@date-io/date-fns";
import { DropzoneDialog } from "material-ui-dropzone";
import FileUpload from "@material-ui/icons/CloudUpload";
import { Formik } from "formik";
import { format } from "date-fns";

const styles = (theme) => ({
  button: {
    margin: theme.spacing(1),
    marginTop: 2,
  },
  rightIcon: {
    marginLeft: theme.spacing(1),
  },
  supportFile: {
    marginBottom: 5,
  },
});

const SecondaryText = styled.p`
  margin: 0;
  font-size: 14px;
  color: #777;
  word-break: break-all;
`;

const LeaveDialog = () => {
  const theme = useTheme();
  const dispatch = useDispatch();
  const leave = useSelector((state) => state.leave.detailDialog);
  const user = useSelector((state) => state.profile.userDetail);
  const [startDate,setStart] = useState(new Date());
  const [endDate,setEnd] = useState(new Date());
  const open = leave !== null;
  const [upload, setUpload] = useState(false);
  const [uploadFile, setFile] = useState(null);
  const xsDown = useMediaQuery(theme.breakpoints.down("xs"));

  useEffect(() =>{
    if(leave !== 'add' && leave !== null){
      setStart(new Date(leave.start));
      setEnd(new Date(leave.end));
    }
  },[leave])

  const handleClose = () => {
    dispatch(setDetailDialogOpen(null));
  };

  const handleSaveFile = (files) => {
    setFile(files[0]);
    console.log(files[0]);
    setUpload(false);
  };

  if (leave === null || user === null) {
    return null;
  }

  const name = leave === 'add' ? user.firstName+" "+user.lastName : leave.employee.firstName+" "+leave.employee.lastName;

  return (
    <>
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth="xs"
      fullWidth
      fullScreen={xsDown}
      css={css`
        .MuiDialog-paper {
          max-width: 820px;
        }
      `}
    >
      <Close onClose={handleClose} />
      <DialogTitle id="member-detail">{leave === 'add' ? 'Add':'Edit'} Leave</DialogTitle>
      <Card>
      <Formik
        initialValues={{
          departmentId:leave === 'add' ? user.department.departmentId: leave.departmentId,
          leaveType: leave === 'add' ? 1 : leave.leaveType,
          approved: leave === 'add' ? false : leave.approved,
          description: leave === 'add' ? '' : leave.description,
        }}
        onSubmit={(values, { setSubmitting,resetForm }) => {
          setTimeout(() => {
            if(leave !== 'add'){
              var param ={
                ...values,
                id:leave.id,
                upload:uploadFile,
                start: format(startDate,"yyyy/MM/dd"),
                end:format(endDate,"yyyy/MM/dd"),
              }

              dispatch(updateLeaveRequest(param));
            }else{
              const param ={
                ...values,
                upload:uploadFile,
                start: format(startDate,"yyyy/MM/dd"),
                end:format(endDate,"yyyy/MM/dd"),
              }
              console.log(param);
              dispatch(addLeaveRequest(param));
              
            }
            dispatch(setDetailDialogOpen(null));
            resetForm({});
            setFile(null);
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
        <form className="mt-4" onSubmit={handleSubmit}>
        <CardContent>
<Grid container spacing={3}>
  <Grid item md={6} xs={12}>
    <TextField
      fullWidth
      label="Name"
      name="name"
      required
      disabled ={true}
      value={name}
      variant="outlined"
    />
  </Grid>
  <Grid item md={6} xs={12}>
  <InputLabel id="demo-simple-select-label">Type</InputLabel>
    <Select
      labelId="demo-simple-select-label"
      id="demo-simple-select"
      name="leaveType"
      style={{ width: "200px", marginBottom: "10px", marginLeft: 10 }}
      value={values.leaveType}
        onChange={handleChange}
    >
      <MenuItem value={1}>Sick</MenuItem>
      <MenuItem value={2}>Work Shift</MenuItem>
      <MenuItem value={3}>Other</MenuItem>
    </Select>
  </Grid>
  <Grid item md={6} xs={12}>
    <InputLabel id="demo-simple-select-label">Approval</InputLabel>
    <Select
      labelId="demo-simple-select-label"
      id="demo-simple-select"
      name="approved"
      style={{ width: "200px", marginBottom: "10px", marginLeft: 10 }}
        onChange={handleChange}
        value={values.approved}
    >
      <MenuItem value={false}>UnApprove</MenuItem>
      <MenuItem value={true}>Approve</MenuItem>
    </Select>
  </Grid>
  <Grid item md={6} xs={12}>
    <InputLabel className={styles.supportFile}>
      Support File
    </InputLabel>
    <Button
      className={styles.button}
      variant="contained"
      color="default"
      onClick={() => setUpload(true)}
    >
      Upload
      <FileUpload className={styles.rightIcon} />
    </Button>
    {uploadFile !== null && <SecondaryText>{uploadFile.name}</SecondaryText>}
  </Grid>
  <Grid item md={6} xs={12}>
    <FormControl>
      <MuiPickersUtilsProvider utils={DateFnsUtils}>
        <KeyboardDatePicker
          label="Start Date"
          name="start"
          format="dd/MM/yyyy"
          placeholder="10/10/2018"
          mask={[
            /\d/,
            /\d/,
            "/",
            /\d/,
            /\d/,
            "/",
            /\d/,
            /\d/,
            /\d/,
            /\d/,
          ]}
          value={startDate}
          onChange={(e) => setStart(e)}
          animateYearScrolling={false}
        />
      </MuiPickersUtilsProvider>
    </FormControl>
  </Grid>
  <Grid item md={6} xs={12}>
    <FormControl>
      <MuiPickersUtilsProvider utils={DateFnsUtils}>
        <KeyboardDatePicker
          label="End Date"
          name="end"
          format="dd/MM/yyyy"
          placeholder="10/10/2018"
          mask={[
            /\d/,
            /\d/,
            "/",
            /\d/,
            /\d/,
            "/",
            /\d/,
            /\d/,
            /\d/,
            /\d/,
          ]}
          value={endDate}
          onChange={(e) => setEnd(e)}
          animateYearScrolling={false}
        />
      </MuiPickersUtilsProvider>
    </FormControl>
  </Grid>
  <Grid item xs={12}>
    <TextField
      fullWidth
      placeholder="Describe the detail..."
      name="description"
      onChange={handleChange}
      value={values.description}
      multiline
      rows={2}
      rowsMax={4}
    />
  </Grid>
  <Divider/>
        <Box
          sx={{
            display: "flex",
            justifyContent: "flex-end",
            p: 2,
          }}
        >
          <Button color="primary" variant="contained" type="submit" disabled={isSubmitting}>
            Save details
          </Button>
        </Box>
</Grid>
</CardContent>
        </form>
      )}
      </Formik>
      </Card>
      <DropzoneDialog
        open={upload}
        onSave={handleSaveFile}
        acceptedFiles={["application/pdf"]}
        showPreviews={true}
        maxFileSize={5000000}
        onClose={() => setUpload(false)}
      />
    </Dialog>
    </>
  );
};

export default LeaveDialog;
