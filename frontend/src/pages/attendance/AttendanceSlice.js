import { createSlice, 
    PayloadAction, 
    createAsyncThunk,
    createEntityAdapter
} from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import axios from "axios";
import authHeader from 'shared/utils/authHeader';

const attendanceAdapter = createEntityAdapter();

export const fetchAttendanceStatus = createAsyncThunk(
    "attendance/fetchAttendanceStatus",
    async (_, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"GET",
            url:"/attendance/getStatus",
            headers:authHeader()
        });
        
        return res.data;

      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const fetchActive = createAsyncThunk(
    "attendance/fetchActiveStatus",
    async (filter, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"POST",
            url:"/attendance/getActive",
            headers:authHeader(),
            data:filter
        });
        
        return res.data;

      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const fetchAbsent = createAsyncThunk(
    "attendance/fetchAbsentStatus",
    async (filter, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"POST",
            url:"/attendance/getAbsent",
            headers:authHeader(),
            data:filter

        });
        
        return res.data;

      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const checkIn = createAsyncThunk(
    "attendance/checkInStatus",
    async (_, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"GET",
            url:"/attendance/getCheckIn",
            headers:authHeader()
        });
        
        return res.data;

      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const checkOut = createAsyncThunk(
    "attendance/checkOutStatus",
    async (_, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"GET",
            url:"/attendance/getCheckOut",
            headers:authHeader()
        });
        
        return res.data;

      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

export const initialState = {
    loading:false,
    list:null,
    status:null,
    checkInDialog:false,
  };

  export const slice = createSlice({
    name: "attendance",
    initialState,
    reducers: {
      setCheckInDialogOpen: (state, action) => {
        state.checkInDialog = action.payload;
      },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchAttendanceStatus.fulfilled,(state,action)=>{
            state.status = action.payload;
        });
        builder.addCase(checkIn.pending, (state) =>{
            state.loading = true;
        });
        builder.addCase(checkIn.fulfilled,(state,action)=>{
            state.status = action.payload;
            state.loading = false;
        });
        builder.addCase(checkOut.pending, (state) =>{
            state.loading = true;
        });
        builder.addCase(checkOut.fulfilled,(state,action)=>{
            state.status = action.payload;
            state.loading = false;
        });
        builder.addCase(fetchActive.fulfilled,(state,action)=>{
            state.list = action.payload;
            state.loading = false;
        });
        builder.addCase(fetchActive.pending,(state,action)=>{
            state.loading = true;
        });
        builder.addCase(fetchAbsent.fulfilled,(state,action)=>{
            state.list = action.payload;
            state.loading = false;
        });
        builder.addCase(fetchAbsent.pending,(state,action)=>{
            state.loading = true;
        });
    },
  });

  export const {
    setCheckInDialogOpen
  } = slice.actions;

export const attendanceSelectors = attendanceAdapter.getSelectors(
    (state) => state.attendance
);

export default slice.reducer;