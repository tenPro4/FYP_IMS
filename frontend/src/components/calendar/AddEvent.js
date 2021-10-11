import React, { useState } from "react";
import Add from "@material-ui/icons/Add";
import Tooltip from "@material-ui/core/Tooltip";
import FloatingPanel from "components/employee/FloatingPanel";
import { setFormOpen,addEvent} from "pages/calendar/EventSlice";
import { useSelector, useDispatch } from "react-redux";
import {
  Radio,
  RadioGroup,
  FormLabel,
  FormControlLabel,
  TextField,
  Button,
  Grid,
  InputAdornment,
  IconButton,
  Fab
} from "@material-ui/core";
import DateFnsUtils from "@date-io/date-fns";
import { Formik } from "formik";
import { format } from "date-fns";
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
  TimePicker,
} from "@material-ui/pickers";
import * as Yup from "yup";
import AccessTime from "@material-ui/icons/AccessTime";

const AddEvent = ({ styles }) => {
  const dispatch = useDispatch();
  const formOpen = useSelector((state) => state.event.formOpen);
  const [startDate, setStart] = useState(new Date());
  const [endDate, setEnd] = useState(new Date());
  const [startTime, setStartTime] = useState(new Date());
  const [endTime, setEndTime] = useState(new Date());

  const handleAddEvent = () => {
    dispatch(setFormOpen(true));
  };

  // const handleChangeTime =(e)=>{
  //   setStartTime(e);
  //   console.log(format(startDate,"yyyy-MM-dd")+" "+format(startTime,"HH:mm tt"));
  // }

  return (
    <div>
      <Tooltip title="Add New Event">
        <Fab
        size="small"
          color="secondary"
          variant="fab"
          onClick={handleAddEvent}
          className={styles.addBtn}
        >
          <Add />
        </Fab>
      </Tooltip>
      <FloatingPanel title="Add Event" open={formOpen} setClose={setFormOpen}>
        <Formik
          initialValues={{
            title: "",
            color: "F8BBD0",
            description:''
          }}
          onSubmit={(values, { setSubmitting,resetForm }) => {
            const param ={
              ...values,
              start:format(startDate,"yyyy-MM-dd")+" "+format(startTime,"HH:mm:ss"),
              end:format(endDate,"yyyy-MM-dd")+" "+format(endTime,"HH:mm:ss"),
            }
            dispatch(addEvent(param));
            resetForm({});
            setSubmitting(false);
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
              <section className={styles.bodyForm}>
                <div>
                  <TextField
                    fullWidth
                    placeholder="Event Name"
                    label="Event Name"
                    name="title"
                    onChange={handleChange}
                    required
                    value={values.title}
                    className={styles.field}
                  />
                </div>
                <div>
                  <Grid
                    container
                    alignItems="flex-start"
                    justify="space-around"
                    direction="row"
                    spacing={12}
                  >
                    <Grid item md={6}>
                      <MuiPickersUtilsProvider utils={DateFnsUtils}>
                        <KeyboardDatePicker
                          className={styles.field}
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
                    </Grid>
                    <Grid item md={6}>
                      <MuiPickersUtilsProvider utils={DateFnsUtils}>
                        <TimePicker
                        name="startTime"
                        onChange={(e) => setStartTime(e)}
                        value={startTime}
                          label="Start Time"
                          mask={[/\d/, /\d/, ":", /\d/, /\d/, " ", /a|p/i, "M"]}
                          placeholder="08:00 AM"
                          InputProps={{
                            endAdornment: (
                              <InputAdornment position="end">
                                <IconButton>
                                  <AccessTime />
                                </IconButton>
                              </InputAdornment>
                            ),
                          }}
                        />
                      </MuiPickersUtilsProvider>
                    </Grid>
                  </Grid>
                </div>
                <div>
                <Grid
                    container
                    alignItems="flex-start"
                    justify="space-around"
                    direction="row"
                    spacing={12}
                  >
                    <Grid item md={6}>
                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                    <KeyboardDatePicker
                      className={styles.field}
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
                    </Grid>
                    <Grid item md={6}>
                    <MuiPickersUtilsProvider utils={DateFnsUtils}>
                        <TimePicker
                        name="endTime"
                        onChange={(e) => setEndTime(e)}
                        value={endTime}
                          label="End Time"
                          mask={[/\d/, /\d/, ":", /\d/, /\d/, " ", /a|p/i, "M"]}
                          placeholder="08:00 AM"
                          InputProps={{
                            endAdornment: (
                              <InputAdornment position="end">
                                <IconButton>
                                  <AccessTime />
                                </IconButton>
                              </InputAdornment>
                            ),
                          }}
                        />
                      </MuiPickersUtilsProvider>
                    </Grid>
                  </Grid>
                  
                </div>
                <div>
                <TextField
                  fullWidth
                  placeholder="Description..."
                  name="description"
                  onChange={handleChange}
                  value={values.description}
                  multiline
                  rows={3}
                  rowsMax={5}
                />
                </div>
                <div>
                  <FormLabel component="label">Label Color</FormLabel>
                  <RadioGroup
                    name="color"
                    className={styles.inlineWrap}
                    value={values.color}
                    onChange={handleChange}
                  >
                    <Radio
                      className={styles.redRadio}
                      value="F8BBD0"
                      classes={{
                        root: styles.redRadio,
                        checked: styles.checked,
                      }}
                    />
                    <Radio
                      className={styles.redRadio}
                      value="C8E6C9"
                      classes={{
                        root: styles.greenRadio,
                        checked: styles.checked,
                      }}
                    />
                    <Radio
                      className={styles.redRadio}
                      value="B3E5FC"
                      classes={{
                        root: styles.blueRadio,
                        checked: styles.checked,
                      }}
                    />
                    <Radio
                      className={styles.redRadio}
                      value="D1C4E9"
                      classes={{
                        root: styles.violetRadio,
                        checked: styles.checked,
                      }}
                    />
                    <Radio
                      className={styles.redRadio}
                      value="FFECB3"
                      classes={{
                        root: styles.orangeRadio,
                        checked: styles.checked,
                      }}
                    />
                  </RadioGroup>
                </div>
              </section>
              <div className={styles.buttonArea}>
                <Button
                  color="secondary"
                  variant="contained"
                  className={styles.button}
                  type="submit"
                  disabled={isSubmitting}
                >
                  Add
                </Button>
              </div>
            </form>
          )}
        </Formik>
      </FloatingPanel>
    </div>
  );
};

export default AddEvent;
