import React, { useState, useEffect } from "react";
import ReactDOM from "react-dom";
import MUIDataTable, { debounceSearchRender } from "mui-datatables";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import { useSelector, useDispatch } from "react-redux";
import { 
    addLeaveRequest, 
    fetchLeaves,
    downloadLeaveDocument,
    setDetailDialogOpen,
    leaveSelectors,
    deleteLeaves,
    selectAllLeaves,
} from "pages/leave/LeaveSlice";
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
import LeaveDialog from './LeaveDialog';
import CustomToolbar from 'components/leave/CustomToolbar';
import {TYPE_MAP,TYPE_VALUE} from './const';
import SEO from "components/SEO";

export default () => {
  const dispatch = useDispatch();
  const [responsive, setResponsive] = useState("vertical");
  const [tableBodyHeight, setTableBodyHeight] = useState("400px");
  const [tableBodyMaxHeight, setTableBodyMaxHeight] = useState("");
  const leaves = useSelector(selectAllLeaves);

  useEffect(() =>{
    dispatch(fetchLeaves());
  },[])

  if (leaves === null) {
    return <Preloader show={true} />;
  }

  const columns = [
    {
        name: "name",
        label: "Name",
        options: {
          filter: false,
          customBodyRenderLite: (dataIndex) => {
            let val = leaves[dataIndex].employee.firstName+" "+leaves[dataIndex].employee.lastName;
            return val;
          },
        },
      },
      {
        name: "description",
        label: "Description",
        options: {
          filter: false,
          customBodyRenderLite: (dataIndex) => {
            let val = leaves[dataIndex].description;
            return val;
          },
        },
      },
      {
        name: "leaveType",
        label: "Type",
        options: {
          filter: true,
          customBodyRenderLite: (dataIndex) => {
            let val = leaves[dataIndex].leaveType;

            return TYPE_VALUE[val];
          },
        },
      },
      {
        name: "start",
        label: "Start Date",
        options: {
          filter: false,
          customBodyRenderLite: (dataIndex) => {
            let val = leaves[dataIndex].start;
            var d = new Date(val);
            return format(d,"dd-MM-yyyy")
          },
        },
      },
      {
        name: "end",
        label: "End Date",
        options: {
          filter: false,
          customBodyRenderLite: (dataIndex) => {
            let val = leaves[dataIndex].end;
            var d = new Date(val);
            return format(d,"dd-MM-yyyy")
          },
        },
      },
      {
        name: "period",
        label: "Period",
        options: {
          filter: false,
          customBodyRenderLite: (dataIndex) => {
            let val = 0;
            var start = new Date(leaves[dataIndex].start);
            var end = new Date(leaves[dataIndex].end);
            val = Math.round((end-start)/(1000*60*60*24));

            return val;
          },
        },
      },
      {
        name: "approved",
        label: "Approval",
        options: {
          filter: true,
          customBodyRenderLite: (dataIndex) => {
            return leaves[dataIndex].approved
            ? "Approved"
            :"Unapproved";
          },
        },
      },
      {
        name: "downloadFile",
        label: "Support File",
        options: {
          filter: false,
          onRowClick: false,
          customBodyRenderLite: (dataIndex) => {
            var value = leaves[dataIndex].supportFile;

            if(value !== null){
              return <p
               css={css`
                    &:hover {
                      cursor: pointer;
                    }
                  `}
              onClick={() => dispatch(downloadLeaveDocument(leaves[dataIndex].id))}>
              {value.fileName}</p>
            }

            return null;
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
    confirmFilters: true,
    onRowClick: (rowData,rowMeta) =>{
        dispatch(setDetailDialogOpen(leaves[rowMeta.dataIndex]));
    },
    onRowsDelete: (rowsDeleted, dataRows) => {
      const idsToDelete = rowsDeleted.data.map(d => leaves[d.dataIndex].id); // array of all ids to to be deleted
      dispatch(deleteLeaves({leaves:idsToDelete}));
    },
    customFilterDialogFooter: (currentFilterList, applyNewFilters) => {
      return (
        <div style={{ marginTop: "40px" }}>
          <Button variant="contained" onClick={applyNewFilters}>
            Apply Filters
          </Button>
        </div>
      );
    },
    customToolbar: () => {
        return (
          <CustomToolbar />
        );
    },
  };

  return (
    <React.Fragment>
    <SEO title="Leave"/>
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
      <MUIDataTable
        title={"Leaves List"}
        data={leaves}
        columns={columns}
        options={options}
      />
      <LeaveDialog/>
    </React.Fragment>
  );
};
