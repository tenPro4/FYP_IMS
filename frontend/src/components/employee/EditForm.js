import React, { useState,useEffect } from "react";
import PropTypes from "prop-types";
import Tooltip from "@material-ui/core/Tooltip";
import Button from "@material-ui/core/Button";
import Add from "@material-ui/icons/Add";
import FloatingPanel from "./FloatingPanel";
import { useSelector,useDispatch } from "react-redux";
import Avatar from "@material-ui/core/Avatar";
import Typography from "@material-ui/core/Typography";
import {
  Input,
  InputLabel,
  MenuItem,
  FormControl,
  FormHelperText,
  Select,
  Grid,
} from "@material-ui/core";
import {UpdateEmployee} from 'pages/employee/EmployeeSlice';
import { setFormOpen } from "pages/employee/EmployeeSlice";

const EditEmployee = ({ styles }) => {
  const dispatch = useDispatch();
  const details = useSelector(state => state.employee.showDetail);
  const formOpen = useSelector((state) => state.employee.formOpen);
  const [pendingPermission, setPermission] = useState([]);
  const [pendingDepartment, setDepartment] = useState("");
  const [pendingRole,setRole] = useState("");

  useEffect(() =>{
    var permission =[];
    if(details !== null){
      if(details.permission != null){
        details.permission.map(data =>{
          permission.push(data.masterPermission.permissionName.split('.')[1]);
        })
      }
      setPermission(permission);
      setDepartment(details.department.masterDepartment.departmentName);
      setRole(details.role);
    }
  },[details]);

  const handleOnReset =()=>{
    var permission =[];
    if(details.permission != null){
      details.permission.map(data =>{
        permission.push(data.masterPermission.permissionName.split('.')[1]);
      })
    }
    setPermission(permission);
    setDepartment(details.department.masterDepartment.departmentName);
    setRole(details.role);
  }

  const handleSave =()=>{
    const param ={
      employeeId:details.employeeId,
      role:pendingRole,
      permission:pendingPermission.map(permission => pendingDepartment+"."+permission),
      department:pendingDepartment,
      oldRole:details.role
    };

    dispatch(UpdateEmployee(param));
  }

  if (details === null) {
    return null;
  }

  const crudOptions = ["CREATE", "READ", "UPDATE", "DELETE"];

  const { employeeImage } = details;
  const imageUrl =
    employeeImage !== null
      ? employeeImage.imageHeader + employeeImage.imageBinary
      : "";
  const imageName = employeeImage !== null ? employeeImage.imageName : "";

  return (
    <div>
      <FloatingPanel title="Edit Employee" open={formOpen} setClose={setFormOpen}>
        <section className={styles.bodyForm}>
          <div className={styles.avatarWrap}>
            <Typography className={styles.formTitle} variant="display1">
              {details.firstName + " " + details.lastName}
            </Typography>
            <Avatar
              alt={imageName}
              src={imageUrl}
              className={styles.uploadAvatar}
            >
              {details.firstName.charAt(0)}
            </Avatar>
          </div>
          <Grid container className={styles.paper}>
              <Grid item xs={6}>
                <FormControl className={styles.formControl}>
                  <InputLabel>Department</InputLabel>
                  <Select
                    value={pendingDepartment}
                    onChange={(e) => setDepartment(e.target.value)}
                    inputProps={{
                      name: "department",
                      id: "department",
                    }}
                  >
                    <MenuItem value={"UNCATEGORY"}>UNCATEGORY</MenuItem>
                    <MenuItem value={"PROJECT"}>PROJECT</MenuItem>
                    <MenuItem value={"HR"}>HR</MenuItem>
                  </Select>
                </FormControl>
              </Grid>
              <Grid item xs={6}>
                <FormControl className={styles.formControl}>
                  <InputLabel>Role</InputLabel>
                  <Select
                    value={pendingRole}
                    onChange={(e) => setRole(e.target.value)}
                    disabled={true}
                    inputProps={{
                      name: "role",
                      id: "role",
                    }}
                  >
                    <MenuItem value={"Admin"}>Admin</MenuItem>
                    <MenuItem value={"Executive"}>Executive</MenuItem>
                  </Select>
                </FormControl>
              </Grid>
              <Grid item xs={12}>
                <FormControl className={styles.formControl}>
                  <InputLabel>Permission</InputLabel>
                  <Select
                    multiple
                    value={pendingPermission}
                    onChange={(e) => setPermission(e.target.value)}
                    input={<Input id="select-multiple" />}
                  >
                    {crudOptions.map((option) => (
                      <MenuItem key={option} value={option}>
                        {option}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>
            </Grid>
        </section>
        <div className={styles.buttonArea}>
            <Button color="secondary" variant="contained" className={styles.button} onClick={handleSave}>
              Save
            </Button>
            <Button
              type="button"
              onClick={handleOnReset}
            >
              Reset
            </Button>
          </div>
      </FloatingPanel>
    </div>
  );
};

export default EditEmployee;
