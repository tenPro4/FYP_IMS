import React, { useState } from "react";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import withWidth, { isWidthDown } from "@material-ui/core/withWidth";
import classNames from "classnames";
import Tooltip from "@material-ui/core/Tooltip";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";
import ExpandIcon from "@material-ui/icons/CallMade";
import MinimizeIcon from "@material-ui/icons/CallReceived";
import styles from "./panel-jss";
import { useSelector, useDispatch } from "react-redux";

const FloatingPanel = (props) => {
  const { classes,width,children,title,open,setClose } = props;
  const dispatch = useDispatch();
  const [expanded, setExpand] = useState(false);
  const extraSize = false;

  const handleCloseForm = () =>{
      dispatch(setClose(false));
  }

  if (!open) {
    return null;
  }

  return (
    <div>
      <div
        className={classNames(
          classes.formOverlay,
          open && (isWidthDown("md", width) || expanded)
            ? classes.showForm
            : classes.hideForm
        )}
      />
      <section
        className={classNames(
          !open ? classes.hideForm : classes.showForm,
          expanded ? classes.expanded : "",
          classes.floatingForm,
          classes.formTheme,
          extraSize && classes.large
        )}
      >
        <header>
         {title}
          <div className={classes.btnOpt}>
            <Tooltip title={expanded ? "Exit Full Screen" : "Full Screen"}>
              <IconButton
                className={classes.expandButton}
                onClick={() => setExpand(!expanded)}
                aria-label="Expand"
              >
                {expanded ? <MinimizeIcon /> : <ExpandIcon />}
              </IconButton>
            </Tooltip>
            <Tooltip title="Close">
                <IconButton className={classes.closeButton} onClick={handleCloseForm} aria-label="Close">
                  <CloseIcon />
                </IconButton>
              </Tooltip>
          </div>
        </header>
        {children}
      </section>
    </div>
  );
};

const FloatingPanelResponsive = withWidth()(FloatingPanel);
export default withStyles(styles)(FloatingPanelResponsive);
