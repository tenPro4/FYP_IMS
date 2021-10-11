import React, { useState, useEffect } from "react";
import ReactDOM from "react-dom";
import MUIDataTable, { debounceSearchRender } from "mui-datatables";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import { useSelector, useDispatch } from "react-redux";
import { fetchActive, fetchAbsent } from "pages/attendance/AttendanceSlice";
import { Button } from "@material-ui/core";
import Preloader from "components/Preloader";
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
} from "@material-ui/pickers";
import MomentUtils from "@date-io/moment";
import DateFnsUtils from '@date-io/date-fns';
import Moment from 'moment';
import { format } from "date-fns";
/** @jsx jsx */
import { css, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import SEO from "components/SEO";

export default () => {
  const dispatch = useDispatch();
  const [responsive, setResponsive] = useState("vertical");
  const [tableBodyHeight, setTableBodyHeight] = useState("400px");
  const [selectedDate, setDate] = useState(new Date());
  const [selectedType, setType] = useState(1);
  const [tableBodyMaxHeight, setTableBodyMaxHeight] = useState("");
  const list = useSelector((state) => state.attendance.list);

  const PickerContainer = styled.div`
  width: "200px";
  marginBottom: "10px";
  marginRight: 10;
`;

  useEffect(() => {
    const formatDate = format(selectedDate,"yyyy/MM/dd");
    if(selectedType ===1){
      dispatch(fetchActive({ attendanceDate: formatDate }));
    }else{
      dispatch(fetchAbsent({ attendanceDate: formatDate }));
    }
  }, [selectedDate,selectedType]);

  if (list === null) {
    return <Preloader show={true} />;
  }

  const columns = [
    {
      name: "cardNo",
      label: "Card No",
      options: {
        filter: false,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].cardNo;
          return val;
        },
      },
    },
    {
      name: "firstName",
      label: "First Name",
      options: {
        filter: true,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].firstName;
          return val;
        },
      },
    },
    {
      name: "lastName",
      label: "Last Name",
      options: {
        filter: true,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].lastName;
          return val;
        },
      },
    },
    {
      name: "email",
      label: "Email",
      options: {
        filter: true,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].email;
          return val;
        },
      },
    },
    {
      name: "departmentName",
      label: "Department",
      options: {
        filter: true,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].departmentName;
          return val;
        },
      },
    },
    {
      name: "scanInTime",
      label: "Check In",
      options: {
        filter: false,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].scanInTime;
          return val;
        },
      },
    },
    {
      name: "scanOutTime",
      label: "Check Out",
      options: {
        filter: false,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].scanOutTime;
          return val;
        },
      },
    },
    {
      name: "lateHour",
      label: "Late(hour)",
      options: {
        filter: false,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].lateHour;
          return val;
        },
      },
    },
    {
      name: "workHours",
      label: "Work Hours",
      options: {
        filter: false,
        customBodyRenderLite: (dataIndex) => {
          let val = list[dataIndex].workHours;
          return val;
        },
      },
    },
  ];

  const options = {
    rowsPerPage: 10,
    rowsPerPageOptions: [10, 100, 250, 500, 1000],
    filter: true,
    filterType: "dropdown",
    responsive,
    tableBodyHeight,
    customSearchRender: debounceSearchRender(500),
    jumpToPage: true,
    tableBodyMaxHeight,
    selectableRows: false,
    confirmFilters: true,
    customFilterDialogFooter: (currentFilterList, applyNewFilters) => {
      return (
        <div style={{ marginTop: "40px" }}>
          <Button variant="contained" onClick={applyNewFilters}>
            Apply Filters
          </Button>
        </div>
      );
    },
  };

  return (
    <React.Fragment>
    <SEO title="Attendance"/>
      <FormControl>
        <InputLabel id="demo-simple-select-label">Responsive Option</InputLabel>
        <Select
          labelId="demo-simple-select-label"
          id="demo-simple-select"
          value={responsive}
          style={{ width: "200px", marginBottom: "10px", marginRight: 10 }}
          onChange={(e) => setResponsive(e.target.value)}
        >
          <MenuItem value={"vertical"}>vertical</MenuItem>
          <MenuItem value={"standard"}>standard</MenuItem>
          <MenuItem value={"simple"}>simple</MenuItem>
        </Select>
      </FormControl>
      <FormControl>
        <InputLabel id="demo-simple-select-label">Table Body Height</InputLabel>
        <Select
          labelId="demo-simple-select-label"
          id="demo-simple-select"
          value={tableBodyHeight}
          style={{ width: "200px", marginBottom: "10px", marginRight: 10 }}
          onChange={(e) => setTableBodyHeight(e.target.value)}
        >
          <MenuItem value={""}>[blank]</MenuItem>
          <MenuItem value={"400px"}>400px</MenuItem>
          <MenuItem value={"800px"}>800px</MenuItem>
          <MenuItem value={"100%"}>100%</MenuItem>
        </Select>
      </FormControl>
      <FormControl>
      <PickerContainer> 
          <MuiPickersUtilsProvider utils={DateFnsUtils}>
            <KeyboardDatePicker
              label="Date"
              format="dd/MM/yyyy"
              placeholder="10/10/2018"
              mask={[/\d/, /\d/, '/', /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/]}
              value={selectedDate}
              onChange={(e) => setDate(e)}
              animateYearScrolling={false}
            />
          </MuiPickersUtilsProvider>
        </PickerContainer>
      </FormControl>
      <FormControl>
        <InputLabel id="demo-simple-select-label">Attendance</InputLabel>
        <Select
          labelId="demo-simple-select-label"
          id="demo-simple-select"
          value={selectedType}
          style={{ width: "200px", marginBottom: "10px", marginLeft: 10 }}
          onChange={(e) => setType(e.target.value)}
        >
          <MenuItem value={1}>Active</MenuItem>
          <MenuItem value={0}>Absent</MenuItem>
        </Select>
      </FormControl>
      <MUIDataTable
        title={"Attendance List"}
        data={list}
        columns={columns}
        options={options}
      />
    </React.Fragment>
  );
};
