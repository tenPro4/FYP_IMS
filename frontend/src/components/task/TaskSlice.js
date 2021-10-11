import {
  createSlice,
  PayloadAction,
  createEntityAdapter,
  createAsyncThunk,
} from "@reduxjs/toolkit";
import { fetchProjectDetail } from "pages/board/ProjectSlice";
import { toast } from "react-toastify";
import axios from "axios";
import authHeader from "shared/utils/authHeader";

export const createTask = createAsyncThunk(
  "project/createTaskStatus",
  async (data, { rejectWithValue }) => {
    try {
      var res = await axios({
        method: "POST",
        url: `/task/addTask/${data.id}`,
        headers: authHeader(),
        data: data,
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

export const patchTaskAssignee = createAsyncThunk(
  "project/patchTaskAssigneeStatus",
  async (data, { rejectWithValue }) => {
    try {
      var res = await axios({
        method: "PATCH",
        url: `/task/updateTaskAssignee/${data.id}`,
        headers: authHeader(),
        data: data,
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

export const patchTask = createAsyncThunk(
  "project/patchTaskStatus",
  async (data, { rejectWithValue }) => {
    try {
      var res = await axios({
        method: "PATCH",
        url: `/task/updateTask/${data.id}`,
        headers: authHeader(),
        data: data,
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



export const deleteTask = createAsyncThunk(
  "project/deleteTaskStatus",
  async (id, { rejectWithValue,getState }) => {
    const columns = getState().projectColumn.ids;
    try {
      const data ={
        columns:columns,
        id:id
      };
      await axios({
        method: "DELETE",
        url: `/task/removeTask/${id}`,
        headers: authHeader(),
      });
      
      return data;

    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const sortTask = createAsyncThunk(
  "project/sortTaskStatus",
  async (data, { rejectWithValue, getState }) => {
    const state = getState();
    const previousTasksByColumn = state.projectTask.byColumn;
    try {
      var res = await axios({
        method: "POST",
        url: "/task/sortTask",
        headers: authHeader(),
        data: data,
      });
      if (res.status === 200) {
        return data.updateOrder;
      }
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(previousTasksByColumn);
    }
  }
);

export const initialState = {
  byColumn: {},
  byId: {},
  createLoading: false,
  createDialogOpen: false,
  createDialogColumn: null,
  editDialogOpen: null,
};

export const slice = createSlice({
  name: "projectTask",
  initialState,
  reducers: {
    setTasksByColumn: (state, action) => {
      state.byColumn = action.payload;
    },
    setTaskColumn:(state,action) =>{
        state.byColumn[action.payload] = [];
    },
    setCreateDialogOpen: (state, action) => {
      state.createDialogOpen = action.payload;
    },
    setCreateDialogColumn: (state, action) => {
      state.createDialogColumn = action.payload;
    },
    setEditDialogOpen: (state, action) => {
      state.editDialogOpen = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchProjectDetail.fulfilled, (state, action) => {
      const byColumn = {};
      const byId = {};
      action.payload.column.map((col) => {
        col.masterTask.map((task) => {
          byId[task.taskId] = task;
        });
        byColumn[col.id] = col.masterTask.map((t) => t.taskId);
      });
      state.byColumn = byColumn;
      state.byId = byId;
    });
    builder.addCase(createTask.pending, (state) => {
      state.createLoading = true;
    });
    builder.addCase(createTask.fulfilled, (state, action) => {
      console.log(action.payload);
      state.byId[action.payload.taskId] = action.payload;
      state.byColumn[action.payload.columnId].push(action.payload.taskId);
      state.createDialogOpen = false;
      state.createLoading = false;
    });
    builder.addCase(createTask.rejected, (state) => {
      state.createLoading = false;
    });
    builder.addCase(sortTask.fulfilled, (state, action) => {
      state.byColumn = action.payload;
    });
    builder.addCase(sortTask.rejected, (state, action) => {
      state.byColumn = action.payload;
    });
    builder.addCase(patchTaskAssignee.fulfilled, (state, action) => {
      state.byId[action.payload.taskId] = action.payload;
    });
    builder.addCase(patchTask.fulfilled, (state, action) => {
      state.byId[action.payload.taskId] = action.payload;
    });
    builder.addCase(deleteTask.fulfilled, (state, action) => {
      action.payload.columns.map((col) =>{
        state.byColumn[col].map((taskId) =>{
          if(taskId === action.payload.id){
            const index = state.byColumn[col].indexOf(taskId);
            state.byColumn[col].splice(index,1);
          }
        });
      });
      delete state.byId[action.payload.id];
    });
  },
});

export const {
  setTasksByColumn,
  setTaskColumn,
  setCreateDialogOpen,
  setCreateDialogColumn,
  setEditDialogOpen,
} = slice.actions;

export default slice.reducer;
