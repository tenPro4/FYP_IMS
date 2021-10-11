import React, { Fragment, useState, useEffect } from "react";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import Divider from "@material-ui/core/Divider";
import Popover from "@material-ui/core/Popover";
import MoreVertIcon from "@material-ui/icons/MoreVert";
import Today from "@material-ui/icons/Today";
import { useSelector, useDispatch } from "react-redux";
import { setShowDetail,deleteEvent } from "pages/calendar/EventSlice";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";

const ITEM_HEIGHT = 48;

const Content = styled.div`
  padding: 2rem;
  width:'100%'
`;

const DetailEvent = ({ styles, anchorEl,close }) => {
  const dispatch = useDispatch();
  const event = useSelector((state) => state.event.showDetail);
  const open = event !== null;
  const [anchorElOpt, setAnchorElOpt] = useState(null);

  const handleClickOpt = (event) =>{
    setAnchorElOpt(event.currentTarget);
  }

  const handleCloseOpt =() =>{
    setAnchorElOpt(null);
  }

  const handleDeleteEvent = (id) => {
    setAnchorElOpt(null);
    dispatch(setShowDetail(null));
    dispatch(deleteEvent(id));
    close();
  };

  const getDate = (date) => {
    if (date._isAMomentObject) {
      return date.format("MMMM Do YYYY");
    }
    let dd = date.getDate();
    const monthNames = [
      "January",
      "February",
      "March",
      "April",
      "May",
      "June",
      "July",
      "August",
      "September",
      "October",
      "November",
      "December",
    ];
    const mm = monthNames[date.getMonth()]; // January is 0!
    const yyyy = date.getFullYear();

    if (dd < 10) {
      dd = "0" + dd;
    }

    const convertedDate = mm + ", " + dd + " " + yyyy;

    return convertedDate;
  };

  const getTime = (time) => {
    if (time._isAMomentObject) {
      return time.format("LT");
    }
    let h = time.getHours();
    let m = time.getMinutes();

    if (h < 10) {
      h = "0" + h;
    }

    if (m < 10) {
      m = "0" + m;
    }

    const convertedTime = h + ":" + m;
    return convertedTime;
  };

  if (event === null || anchorEl === undefined) {
    return null;
  }

  return (
    <Popover
      open={Boolean(anchorEl)}
      anchorEl={anchorEl}
      className={styles.eventDetail}
      onClose={close}
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'center',
      }}
      transformOrigin={{
        vertical: 'top',
        horizontal: 'center',
      }}
    >
      <IconButton
        aria-label="More"
        aria-owns={anchorElOpt ? 'long-menu' : null}
        aria-haspopup="true"
        className={styles.moreOpt}
        onClick={handleClickOpt}
      >
        <MoreVertIcon />
      </IconButton>
      {event !== null &&
        <Fragment>
          <Menu
            id="long-menu"
            anchorEl={anchorElOpt}
            open={Boolean(anchorElOpt)}
            onClose={handleCloseOpt}
            PaperProps={{
              style: {
                maxHeight: ITEM_HEIGHT * 4.5,
                width: 200,
              },
            }}
          >
            <MenuItem onClick={() => handleDeleteEvent(event.id)}>
              Delete Event
            </MenuItem>
          </Menu>
          <Typography mt={2} variant="h4" style={{ 
            background: `#${event.color}`,
             }} className={styles.eventName}>
            <Today /> {event.title}
          </Typography>
          <div className={styles.time}>
            <Typography>Start: {getDate(event.start)} - {getTime(event.start)}</Typography>
            <Divider className={styles.divider} />
            <Typography>End: {getDate(event.end)} - {getTime(event.end)}</Typography>
            <Divider className={styles.divider} />
            <Typography>{event.description}</Typography>
          </div>
        </Fragment>
      }
    </Popover>
  );
};

export default DetailEvent;
