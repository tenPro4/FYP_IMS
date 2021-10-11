import React from "react";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ArrowBack from "@material-ui/icons/ArrowBack";
import IconButton from "@material-ui/core/IconButton";
import PermContactCalendar from "@material-ui/icons/PermContactCalendar";
import Add from "@material-ui/icons/Add";
import { useSelector, useDispatch } from "react-redux";
import styles from "pages/employee/employee-jss";

const EmployeeHeader = (props) => {
  const {classes} = props;

  return (
    <AppBar position="absolute" className={classes.appBar}>
      <Toolbar>
        <Typography
          variant="subheading"
          className={classes.title}
          color="inherit"
          noWrap
        >
          <PermContactCalendar /> Employee
        </Typography>
      </Toolbar>
    </AppBar>
  );
};

export default withStyles(styles)(EmployeeHeader);
