import {
  createSlice,
  PayloadAction,
  createEntityAdapter,
  createAsyncThunk,
} from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import axios from "axios";
import authHeader from "shared/utils/authHeader";
import { fetchProjectDetail } from "pages/board/ProjectSlice";

export const createComment = createAsyncThunk(
  "task/createCommentStatus",
  async (data, { rejectWithValue }) => {
    try {
      var res = await axios({
        method: "POST",
        url: `/task/addComment/${data.id}`,
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

export const deleteComment = createAsyncThunk(
  "task/deleteCommentStatus",
  async (id, { rejectWithValue }) => {
    try {
      await axios({
        method: "DELETE",
        url: `/task/deleteComment/${id}`,
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

const commentAdapter = createEntityAdapter({
    selectId: (comment) => comment.commentId,

    sortComparer: (a, b) => b.dateCreated.localeCompare(a.dateCreated),
});

export const initialState = commentAdapter.getInitialState({
  createCommentStatus: "idle",
});

export const slice = createSlice({
  name: "comment",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchProjectDetail.fulfilled, (state, action) => {
      action.payload.column.map((col) => {
        col.masterTask.map((task) => {
            commentAdapter.addMany(state,task.taskComment);
        });
      });
    });
    builder.addCase(createComment.pending, (state, action) => {
      state.createCommentStatus = "loading";
    });
    builder.addCase(createComment.fulfilled, (state, action) => {
      commentAdapter.addOne(state, action.payload);
      state.createCommentStatus = "succeeded";
    });
    builder.addCase(deleteComment.fulfilled, (state, action) => {
      commentAdapter.removeOne(state,action.payload);
    });
  },
});

export const { selectAll: selectAllComments } = commentAdapter.getSelectors(
    (state) => state.comment
  );

export const selectCreateCommentStatus = (state) =>
  state.comment.createCommentStatus;

  export default slice.reducer;