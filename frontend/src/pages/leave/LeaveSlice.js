import {
  createSlice,
  PayloadAction,
  createAsyncThunk,
  createEntityAdapter,
} from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import axios from "axios";
import authHeader from "shared/utils/authHeader";

export const fetchLeaves = createAsyncThunk(
  "leave/fetchLeavesStatus",
  async (_, { rejectWithValue }) => {
    try {
      var res = await axios({
        method: "GET",
        url: "/leave",
        headers: authHeader(),
      });

      return res.data;
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const addLeaveRequest = createAsyncThunk(
  "leave/addLeaveRequestStatus",
  async (data, { rejectWithValue }) => {
    try {
      var file = data.upload;
      data.upload = null;

      var response = await axios({
        method: "POST",
        url: "/leave/leaveRequest",
        headers: authHeader(),
        data: data,
      });

      if(file !== null){
        const formData = new FormData();
        formData.append('upload', file);
  
        var newResponse =await axios.post(`/leave/uploadFile/${response.data.id}`,formData);

        return newResponse.data;
      }else{
        return response.data;
      }

    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const updateLeaveRequest = createAsyncThunk(
  "leave/updateLeaveRequestStatus",
  async (data, { rejectWithValue }) => {
    try {
      var file = data.upload;
      data.upload = null;

      var response = await axios({
        method: "PATCH",
        url: `/leave/updateRequest/${data.id}`,
        headers: authHeader(),
        data: data,
      });

      if(file !== null){
        const formData = new FormData();
        formData.append('upload', file);
  
        var newResponse =await axios.post(`/leave/uploadFile/${response.data.id}`,formData);

        return newResponse.data;
      }else{
        return response.data;
      }

    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const deleteLeaves = createAsyncThunk(
  "leave/deleteLeavesStatus",
  async(data,{rejectWithValue}) =>{
    try{

      await axios({
        method:"DELETE",
        url:"/leave/removeLeaves",
        data:data
      });

      return data.leaves;

    }catch(err){

    }
  }
)

export const downloadLeaveDocument = createAsyncThunk(
  "leave/downloadDocumentStatus",
  async (id, { rejectWithValue }) => {
    axios
      .get(`/leave/downloadSupportFile/${id}`, { 
          responseType: "blob"
        })
      .then(response => {
        let fileName = response.headers["content-disposition"].split("filename=")[1];
        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
          // IE variant
          window.navigator.msSaveOrOpenBlob(
            new Blob([response.data], {
              type: "application/pdf",
            }),
            fileName
          );
        } else {
          const url = window.URL.createObjectURL(
            new Blob([response.data], {
              type: "application/pdf",
            })
          );
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute(
            "download",
            response.headers["content-disposition"].split("filename=")[1]
          );
          document.body.appendChild(link);
          link.click();
        }
      });
  }
);

const leaveAdapter = createEntityAdapter();

export const initialState = leaveAdapter.getInitialState({
    detailDialog: null,
  });

export const slice = createSlice({
  name: "leave",
  initialState,
  reducers: {
      removeLeave: leaveAdapter.removeOne,
      addLeave: leaveAdapter.addOne,
    setDetailDialogOpen: (state, action) => {
      state.detailDialog = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchLeaves.fulfilled, (state, action) => {
        leaveAdapter.setAll(state,action.payload);
    });
    builder.addCase(addLeaveRequest.fulfilled, (state, action) => {
      leaveAdapter.addOne(state,action.payload);
    });
    builder.addCase(updateLeaveRequest.fulfilled, (state, action) => {
      leaveAdapter.updateOne(state,{
        id:action.payload.id,
        changes:action.payload,
      });
    });
    builder.addCase(deleteLeaves.fulfilled, (state, action) => {
      leaveAdapter.removeMany(state,action.payload);
    });
  },
});


export const { setDetailDialogOpen,removeLeave,addLeave } = slice.actions;

export const leaveSelectors = leaveAdapter.getSelectors(
    (state) => state.leave
  );

export const{
    selectAll : selectAllLeaves,
    selectEntities:selectLeavesEntities
} = leaveSelectors;

export default slice.reducer;
