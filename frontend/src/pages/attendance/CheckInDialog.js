import React, { useState,useEffect } from "react";
import {
  Dialog,
  Avatar,
  Button,
  Fab,
  useMediaQuery,
  useTheme,
  DialogTitle,
} from "@material-ui/core";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import {setCheckInDialogOpen,checkIn,checkOut} from 'pages/attendance/AttendanceSlice';
import Close from "components/boardMember/Close";
import { useDispatch, useSelector } from "react-redux";
import Input from "@material-ui/icons/Input";

const Container = styled.div`  
  display: block;
    margin: 0 auto;
  ${(props) => props.theme.breakpoints.down("xs")}
`;

const SecondaryText = styled.p`
  margin: 0;
  font-size: 14px;
  color: #777;
  word-break: break-all;
`;

const CheckInDialog = () => {
    const theme = useTheme();
    const dispatch = useDispatch();
    const [text,setText] = useState("Check In");
    const [description,setDescription] = useState("You haven't check in today.");
    const status = useSelector((state) => state.attendance.status);
    const loading = useSelector((state) => state.attendance.loading);
    const open = useSelector((state) => state.attendance.checkInDialog);
    const xsDown = useMediaQuery(theme.breakpoints.down("xs"));
    const user = useSelector((state) => state.profile.userDetail);
    
    useEffect(() => {
        if(status !== null){
            if(status.current === 1){
                setText("Check Out");
                setDescription("Want to check out?88 see you again!")
            }
            else if(status.current === 0){
                setText("Check In");
                setDescription("Welcome back.")
            }
        }
      }, [status]);
    
    const handleClose = () =>{
     dispatch(setCheckInDialogOpen(false));
    }

    if(user === null || status === null){
        return null;
    }

    const handleCheckIn =() =>{
        if(text === "Check In"){
            dispatch(checkIn());
        }else{
            dispatch(checkOut());
        }
    }

    const imageUrl = user !== null 
    ? user.employeeImage.imageHeader+user.employeeImage.imageBinary
    : '';

    return(
        <Dialog
        open={open}
        onClose={handleClose}
        maxWidth="xs"
        fullWidth
        fullScreen={xsDown}
      >
          <Close onClose={handleClose} />
          <DialogTitle id="member-detail">Check In</DialogTitle>
          <Container theme={theme}>
          <Avatar
              css={css`
                height: 8rem;
                width: 8rem;
                font-size: 40px;
                margin-bottom: 1rem;
              `}
              src={imageUrl} 
              alt={user.employeeImage !== null ?  user.employeeImage.imageName : ''}
            >
              {user.firstName.charAt(0)}
            </Avatar>
            <Fab variant="extended" color={text === "Check In" ? "primary":"secondary"} aria-label="checkin"
            onClick={handleCheckIn}
            disabled={loading}
             css={css`
                margin-bottom: 1rem;
              `}
            >
              <Input/>
              {text}
            </Fab>
            <SecondaryText
            css={css`
                  margin-bottom: 1.5rem;
                  text-align: 'center';
                `}>
                {description}
            </SecondaryText>
          </Container>
      </Dialog>
    )
}

export default CheckInDialog;