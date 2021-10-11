import React, { useEffect, useState, useRef } from "react";
import { withStyles,MuiThemeProvider } from '@material-ui/core/styles';
import {useTheme,makeStyles} from "@material-ui/core";
import EmployeeHeader from 'components/employee/Header';
import EmployeeList from 'components/employee/List';
import EmployeeDetail from 'components/employee/Detail';
import styles from './employee-jss';
import SEO from 'components/SEO';
import EditForm from 'components/employee/EditForm';

const Employee = (props) => {
    const {classes} = props;

    return(
        <div>
            <SEO title="Employees"/>
            <div className={classes.root}>
                <EmployeeHeader/>
                <EmployeeList
                clippedRight
                styles={classes}
                />
                <EmployeeDetail
                    styles={classes}
                />
                <EditForm styles={classes}/>
            </div>
        </div>
    );
}

export default withStyles(styles)(Employee);