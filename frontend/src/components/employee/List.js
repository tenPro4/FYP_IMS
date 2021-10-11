import React, { Fragment, useState,useEffect } from "react";
import { withStyles } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import classNames from "classnames";
import Divider from "@material-ui/core/Divider";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import Avatar from "@material-ui/core/Avatar";
import BottomNavigation from "@material-ui/core/BottomNavigation";
import BottomNavigationAction from "@material-ui/core/BottomNavigationAction";
import SearchIcon from "@material-ui/icons/Search";
import PermContactCalendar from "@material-ui/icons/PermContactCalendar";
import Star from "@material-ui/icons/Star";
import empData from "./dummy";
import Useravatar from 'assets/img/avatars/pp_boy4.svg'
import { useDispatch,useSelector } from "react-redux";
import {fetchEmployees,searchEmployees,showDetail} from 'pages/employee/EmployeeSlice';

const EmployeeList = ({ clippedRight,styles}) => {
  const dispatch = useDispatch();
  const employeeList = useSelector((state) => state.employee.searchList);
  const itemSelected = useSelector((state) => state.employee.itemSelected);
  const [search, setSearch] = useState("");

  useEffect(() =>{
    dispatch(fetchEmployees());
  },[])

  if(employeeList === null){
    return null;
  }

  const handleOnChange = (e) =>{
    dispatch(searchEmployees(e.target.value));
  }

  const handleShowDetail = (data) =>{
    dispatch(showDetail(data));
  }

  return (
    <Fragment>
      <Drawer
        variant="permanent"
        anchor="left"
        open
        classes={{
          paper: styles.drawerPaper,
        }}
      >
        <div>
          <div
            className={classNames(
              styles.toolbar,
              clippedRight && styles.clippedRight
            )}
          >
            <div className={styles.flex}>
              <div className={styles.searchWrapper}>
                <div className={styles.search}>
                  <SearchIcon />
                </div>
                <input
                  className={styles.input}
                  onChange={handleOnChange}
                  placeholder="Search Employees"
                />
              </div>
            </div>
          </div>
          <Divider />
          <List>
            {employeeList !== null && employeeList.map((data, index) => {
              const {employeeImage} = data;
              const imageUrl = employeeImage !== null 
              ? employeeImage.imageHeader+employeeImage.imageBinary
              : '';
              const imageName = employeeImage !== null 
              ? employeeImage.imageName
              : '';
              return (
                <ListItem
                  button
                  key={data.employeeId}
                  className={itemSelected === data.employeeId ? styles.selected : ""}
                  onClick={() => handleShowDetail(data)}
                >
                  <Avatar
                    alt={imageName}
                    src={imageUrl}
                    className={styles.avatar}
                  >
                  {data.firstName.charAt(0)}
                  </Avatar>
                  <ListItemText
                    primary={data.firstName+" "+data.lastName}
                    secondary={data.role !== null ? data.role : ''}
                  />
                </ListItem>
              );
            })}
          </List>
        </div>
      </Drawer>
    </Fragment>
  );
};

export default EmployeeList;
