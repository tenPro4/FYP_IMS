import React,{useEffect,useState} from "react";
import { withStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import styles from "./calendar-jss";
// import events from "./eventData";
import "react-big-calendar/lib/css/react-big-calendar.css";
import AddEvent from "components/calendar/AddEvent";
import {fetchEvents,eventSelectors,setShowDetail} from 'pages/calendar/EventSlice';
import { useSelector, useDispatch } from "react-redux";
import Preloader from "components/Preloader";
import DetailEvent from 'components/calendar/DetailEvent';
import SEO from 'components/SEO';

const localizer = momentLocalizer(moment);

const Event = (event) => {
  return <span className="eventBlock">{event.title}</span>;
};

const EventCalendar = (props) => {
  const dispatch = useDispatch();
  const [anchorEl,setAnchorEl] = useState(null);
  const events = useSelector(eventSelectors.selectAll);
    const { classes } = props;

  useEffect(() =>{
    dispatch(fetchEvents());
  },[]);

  const handleClickOnSelect =(e) =>{
    setTimeout(() => {
      const target = document.getElementsByClassName('rbc-selected')[0];
      console.log(target);
      setAnchorEl(target);
    },500);
    dispatch(setShowDetail(e));
  }

  const handleClose = () =>{
    setAnchorEl(null);
  }

  if(events === null){
      <Preloader show={true}/>
  }

  const eventStyleGetter = (event) => {
    const backgroundColor = "#" + event.color;
    const style = {
      backgroundColor,
      color:'black'
    };
    return {
      style,
    };
  };

  return (
    <div className={classes.root}>
    <SEO title="Events"/>
      <Paper className={classes.root}>
        <Calendar
          localizer={localizer}
          className={classes.calendarWrap}
          selectable
          events={events}
          defaultView="month"
          step={60}
          showMultiDayTimes
          scrollToTime={new Date(1970, 1, 1, 6)}
          defaultDate={new Date()}
          onSelectEvent={handleClickOnSelect}
          eventPropGetter={eventStyleGetter}
          onSelectSlot={(slotInfo) =>
            console.log(
              `selected slot: \n\nstart ${slotInfo.start.toLocaleString()} ` +
                `\nend: ${slotInfo.end.toLocaleString()}` +
                `\naction: ${slotInfo.action}`
            )
          }
          components={{
            event: Event,
          }}
        />
      </Paper>
      <DetailEvent styles={classes} anchorEl={anchorEl} close={handleClose}/>
      <AddEvent styles={classes}/>
    </div>
  );
};

export default withStyles(styles)(EventCalendar);
