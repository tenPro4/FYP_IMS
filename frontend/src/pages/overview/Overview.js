import React, { Component } from 'react'
import SEO from "components/SEO";
import {setCheckInDialogOpen} from "pages/attendance/AttendanceSlice";
import { useDispatch, useSelector } from "react-redux";
import {Button} from "@material-ui/core";

export default () =>{

    const dispatch = useDispatch();
    const status = useSelector((state) => state.attendance.status);

    if(status !== null){
        if(status.current === undefined || status.current === 0){
            dispatch(setCheckInDialogOpen(true));
        }
    }

    const openDialog =() =>{
        dispatch(setCheckInDialogOpen(true));
    }

    return(
        <div>
            <SEO title="Overview"/>
            <Button onClick={openDialog}>Open Dialog</Button>
        </div>
    )
}
