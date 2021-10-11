import {
    createSlice,
    createEntityAdapter,
    createAsyncThunk,
  } from "@reduxjs/toolkit";
  import { fetchProjectDetail } from "pages/board/ProjectSlice";
  import { toast } from "react-toastify";
  import axios from "axios";
  import authHeader from "shared/utils/authHeader";
  import update from 'immutability-helper';

  const employeeAdapter = createEntityAdapter({
      selectId: (employee) => employee.employeeId,
  });

  export const fetchEmployees = createAsyncThunk(
    "employee/fetchEmployeeListStatus",
    async (_, { rejectWithValue }) => {
      try {
        var res = await axios({
          method: "GET",
          url:"/employee/getAll",
          headers: authHeader(),
        });
        if (res.status === 200) {
          return res.data;
        }
      } catch (err) {
        let [first] = Object.values(err.response.data.errors);
        toast.error(first[0]);
        return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const UpdateEmployee = createAsyncThunk(
    "employee/UpdateEmployeeStatus",
    async (data, { rejectWithValue }) => {
      try {
        var res = await axios({
          method: "POST",
          url:"/employee/updatePermission",
          headers: authHeader(),
          data:data
        });
        if (res.status === 200) {
          return res.data;
        }
      } catch (err) {
        let [first] = Object.values(err.response.data.errors);
        toast.error(first[0]);
        return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const slice = createSlice({
    name:"employee",
    initialState:employeeAdapter.getInitialState({
        initialList:null,
        searchList:null,
        formOpen:false,
        itemSelected:null,
        showDetail:null,
    }),
    reducers:{
        setFormOpen:(state,action) =>{
            state.formOpen = action.payload;
        },
        searchEmployees:(state,action) =>{
            state.searchList = state.initialList.filter((row) =>{
                if(!action.payload) return row;

                return row.firstName.toLowerCase().includes(action.payload.toLowerCase());
            });
        },
        showDetail:(state,action) =>{
            state.showDetail = action.payload;
            state.itemSelected = action.payload.employeeId;
        },
    },
    extraReducers: (builder) =>{
        builder.addCase(fetchEmployees.fulfilled,(state,action) =>{
            state.initialList = action.payload;
            state.searchList = action.payload;
        });
        builder.addCase(UpdateEmployee.fulfilled,(state,action) =>{
          const index = state.initialList.findIndex((emp) => emp.employeeId === action.payload.employeeId);
          const newData = update(state.initialList,{
            $splice:[[index,1,action.payload]]
          });
          state.initialList = newData;
          state.searchList = newData;
          state.showDetail = state.searchList.filter(x => x.employeeId === action.payload.employeeId)[0];
      });
    },
});

export const {
    setFormOpen,
    searchEmployees,
    showDetail,
  } = slice.actions;
  
  export default slice.reducer;