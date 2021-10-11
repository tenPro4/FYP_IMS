import React, { useState } from "react";
import classNames from "classnames";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import Fab from "@material-ui/core/Fab";
import Avatar from "@material-ui/core/Avatar";
import { Alert } from "@material-ui/lab";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import IconButton from "@material-ui/core/IconButton";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import Edit from "@material-ui/icons/Edit";
import Star from "@material-ui/icons/Star";
import StarBorder from "@material-ui/icons/StarBorder";
import MoreVertIcon from "@material-ui/icons/MoreVert";
import LocalPhone from "@material-ui/icons/LocalPhone";
import Email from "@material-ui/icons/Email";
import Smartphone from "@material-ui/icons/Smartphone";
import LocationOn from "@material-ui/icons/LocationOn";
import Group from "@material-ui/icons/Group";
import Work from "@material-ui/icons/Work";
import Language from "@material-ui/icons/Language";
import ListIcon from "@material-ui/icons/List";
import Divider from "@material-ui/core/Divider";
import PermIdentity from "@material-ui/icons/PermIdentity";
import empData from "./dummy";
import { useDispatch, useSelector } from "react-redux";
import { setFormOpen } from "pages/employee/EmployeeSlice";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import { faAngleLeft } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Dialog from "@material-ui/core/Dialog";

const optionsOpt = ["Block", "Delete"];

const ITEM_HEIGHT = 48;

const ConfirmAction = styled.div`
  display: flex;
  justify-content: space-between;
`;

const EmployeeDetail = ({ styles }) => {
  const dispatch = useDispatch();
  const employeeDetail = useSelector((state) => state.employee.showDetail);
  const [anchorElOpt, setAnchorElOpt] = useState(null);
  const [confirmDelete, setConfirmDelete] = useState(false);
  // const filter = empData.filter(x => x.id === '1');
  // const data = filter[0];

  const handleClickOpt = (event) => {
    setAnchorElOpt(event.currentTarget);
  };

  const handleCloseOpt = () => {
    setAnchorElOpt(null);
  };

  const handleOpenEditForm = () => {
    dispatch(setFormOpen(true));
  };

  const handleRemoveEmployee = () => {
    console.log(employeeDetail.employeeId);
  };

  if (employeeDetail === null) {
    return null;
  }

  const { employeeImage } = employeeDetail;
  const imageUrl =
    employeeImage !== null
      ? employeeImage.imageHeader + employeeImage.imageBinary
      : "";
  const imageName = employeeImage !== null ? employeeImage.imageName : "";

  var permissionList = "";
  if (employeeDetail.permission !== null) {
    employeeDetail.permission.map((permission) => {
      permissionList = permissionList !== "" ? permissionList + "," : "";
      var name = permission.masterPermission.permissionName.split(".");
      permissionList = permissionList + name[1];
    });
  }

  return (
    <main className={classNames(styles.content, styles.detailPopup)}>
      <section className={styles.cover}>
        <div className={styles.opt}>
          <IconButton aria-label="Edit" onClick={handleOpenEditForm}>
            <Edit />
          </IconButton>
          <IconButton
            aria-label="More"
            aria-owns={anchorElOpt ? "long-menu" : null}
            aria-haspopup="true"
            className={styles.button}
            onClick={handleClickOpt}
          >
            <MoreVertIcon />
          </IconButton>
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
            {optionsOpt.map((option) => {
              if (option === "Delete") {
                return (
                  <MenuItem
                    key={option}
                    selected={option === "Edit Profile"}
                    onClick={() => setConfirmDelete(true)}
                  >
                    {option}
                  </MenuItem>
                );
              }
              return (
                <MenuItem
                  key={option}
                  selected={option === "Edit Profile"}
                  onClick={handleCloseOpt}
                >
                  {option}
                </MenuItem>
              );
            })}
          </Menu>
        </div>
        <Avatar alt={imageName} src={imageUrl} className={styles.avatar}>
          {employeeDetail.firstName.charAt(0)}
        </Avatar>
        <Typography className={styles.userName} variant="display1">
          {employeeDetail.firstName + " " + employeeDetail.lastName}
        </Typography>
        <br />
        <Typography variant="caption">
          {employeeDetail.role !== null ? " (" + employeeDetail.role + ")" : ""}
        </Typography>
      </section>
      <div>
        <Dialog open={confirmDelete} maxWidth="xs" fullWidth>
          <div>
            <Alert
              severity="error"
              css={css`
                margin-bottom: 2rem;
              `}
            >
              Are you sure you want to remove this employee?
            </Alert>
            <ConfirmAction>
              <Fab
                size="small"
                onClick={() => setConfirmDelete(false)}
                css={css`
                  box-shadow: none;
                  &.MuiFab-sizeSmall {
                    width: 32px;
                    height: 32px;
                  }
                `}
              >
                <FontAwesomeIcon icon={faAngleLeft} color="#555" />
              </Fab>
              <Button
                size="small"
                color="secondary"
                variant="contained"
                onClick={handleRemoveEmployee}
                css={css`
                  font-size: 0.625rem;
                `}
              >
                Remove member
              </Button>
            </ConfirmAction>
          </div>
        </Dialog>

        <List>
          <ListItem>
            <Avatar className={styles.blueIcon}>
              <Group />
            </Avatar>
            <ListItemText
              primary={
                employeeDetail.department.masterDepartment.departmentName
              }
              secondary="Department"
            />
          </ListItem>
          <Divider inset />
          <ListItem>
            <Avatar className={styles.amberIcon}>
              <ListIcon />
            </Avatar>
            <ListItemText primary={permissionList} secondary="Permission" />
          </ListItem>
          <Divider inset />
          <ListItem>
            <Avatar className={styles.tealIcon}>
              <PermIdentity />
            </Avatar>
            <ListItemText
              primary={employeeDetail.role !== null ? employeeDetail.role : ""}
              secondary="Role"
            />
          </ListItem>
          <Divider inset />
          {/* <ListItem>
              <Avatar className={styles.brownIcon}>
                <Work />
              </Avatar>
              <ListItemText primary={data.companyEmail} secondary="Company Email" />
            </ListItem>
            <Divider inset />
            <ListItem>
              <Avatar className={styles.redIcon}>
                <LocationOn />
              </Avatar>
              <ListItemText primary={data.address} secondary="Address" />
            </ListItem>
            <Divider inset />
            <ListItem>
              <Avatar className={styles.purpleIcon}>
                <Language />
              </Avatar>
              <ListItemText primary={data.website} secondary="Website" />
            </ListItem> */}
        </List>
      </div>
    </main>
  );
};

export default EmployeeDetail;
