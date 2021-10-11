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
import {setTaskColumn} from "components/task/TaskSlice";

const columnAdapter = createEntityAdapter();

export const initialState = columnAdapter.getInitialState();

export const patchColumnTitle = createAsyncThunk(
  "projectColumn/patchColumnTitleStatus",
  async (data, { rejectWithValue }) => {
    try {
      await axios({
        method: "PATCH",
        url: `/projectColumn/updateTitle/${data.id}`,
        headers: authHeader(),
        data: data,
      });

      return data;
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const updateColumns = createAsyncThunk(
  "projectColumn/updateColumnsStatus",
  async (data, { rejectWithValue,dispatch,getState }) => {
    const previousColumns = selectAllColumns(getState());
    try {
      // dispatch(setColumns(data.columns))
      await axios({
        method: "POST",
        url: `/projectColumn/updateColumn/${data.id}`,
        headers: authHeader(),
        data: {ids:data.columns.map((col) => col.id)},
      });

      return data.columns;
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(previousColumns);
    }
  }
);

export const addColumn = createAsyncThunk(
  "projectColumn/addColumnStatus",
  async (id, { rejectWithValue,dispatch}) => {
    try {
      // dispatch(setColumns(data.columns))
      const res = await axios({
        method: "POST",
        url: `/project/addColumn/${id}`,
        headers: authHeader(),
      });

      dispatch(setTaskColumn(res.data.id));

      return res.data;
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const deleteColumn = createAsyncThunk(
  "projectColumn/deleteColumnStatus",
  async (id, { rejectWithValue}) => {
    try {
      // dispatch(setColumns(data.columns))
      const res = await axios({
        method: "DELETE",
        url: `/project/deleteColumn/${id}`,
        headers: authHeader(),
      });

      return id;
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const slice = createSlice({
  name: "projectColumn",
  initialState,
  reducers: {
    setColumns: columnAdapter.setAll,
  },
  extraReducers: (builder) => {
    builder.addCase(fetchProjectDetail.fulfilled, (state, action) => {
      columnAdapter.setAll(
        state,
        action.payload.column.map((col) => ({
          id: col.id,
          title: col.title,
          project: action.payload.projectId,
        }))
      );
    });
    builder.addCase(patchColumnTitle.fulfilled, (state, action) => {
      columnAdapter.updateOne(state, {
        id: action.payload.id,
        changes: { title: action.payload.title },
      });
    });
    builder.addCase(updateColumns.fulfilled, (state, action) => {
      columnAdapter.setAll(state, action.payload);
    });
    builder.addCase(updateColumns.rejected, (state, action) => {
      columnAdapter.setAll(state, action.payload);
    });
    builder.addCase(addColumn.fulfilled, (state, action) => {
      columnAdapter.addOne(state, action.payload);
    });
    builder.addCase(deleteColumn.fulfilled, (state, action) => {
      columnAdapter.removeOne(state, action.payload);
    });
  },
});

export const { setColumns } = slice.actions;

export const columnSelectors = columnAdapter.getSelectors(
  (state) => state.projectColumn
);

export const {
  selectAll: selectAllColumns,
  selectEntities: selectColumnsEntities,
} = columnSelectors;

export default slice.reducer;
