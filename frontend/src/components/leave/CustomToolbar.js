import React from "react";
import IconButton from "@material-ui/core/IconButton";
import Tooltip from "@material-ui/core/Tooltip";
import AddIcon from "@material-ui/icons/Add";
import { useSelector, useDispatch } from "react-redux";
import { 
    setDetailDialogOpen,
} from "pages/leave/LeaveSlice";

export default () =>{
  
    const dispatch = useDispatch();

  const handleClick = () => {
    dispatch(setDetailDialogOpen('add'));
  }

    return (
        <React.Fragment>
        <Tooltip title={"Add leave"}>
            <IconButton onClick={handleClick}>
            <AddIcon/>
            </IconButton>
        </Tooltip>
        </React.Fragment>
    );
}