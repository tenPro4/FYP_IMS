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

export const removeMemberFromProject = createAsyncThunk(
  "project/removeMemberStatus",
  async (data, { rejectWithValue }) => {
    console.log(data);
    try {
      await axios({
        method: "DELETE",
        url: "/project/leaveProject",
        headers: authHeader(),
        data: data,
      });

      return data.employeeId;
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

export const addMembersToProject = createAsyncThunk(
  "project/addMembersStatus",
  async (data, { rejectWithValue }) => {
    console.log(data);
    try {
      var res = await axios({
        method: "POST",
        url: `/project/${data.projectId}/addMembers`,
        headers: authHeader(),
        data: data,
      });

      return res.data;
    } catch (err) {
      let [first] = Object.values(err.response.data.errors);
      toast.error(first[0]);
      return rejectWithValue(err.response.data.errors);
    }
  }
);

const memberAdapter = createEntityAdapter({
  selectId: (projectMember) => projectMember.id,

  sortComparer: (a, b) => a.firstName.localeCompare(b.firstName),
});

export const initialState = memberAdapter.getInitialState({
  memberList: null,
  dialogMember: null,
  memberListOpen: false,
});

export const slice = createSlice({
  name: "projectMember",
  initialState,
  reducers: {
    setAllProjectMembers: memberAdapter.setAll,
    addProjectMembers: memberAdapter.addMany,
    removeProjectMember: memberAdapter.removeOne,
    setDialogMember: (state, action) => {
      state.dialogMember = action.payload;
    },
    setMemberListOpen: (state, action) => {
      state.memberListOpen = action.payload;
    },
    removeFromMember: (state, action) => {
      state.memberList = state.memberList.filter(
        (x) => x.employeeId !== action.payload
      );
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchProjectDetail.fulfilled, (state, action) => {
      state.memberList = action.payload.projectUser;
    });
    builder.addCase(removeMemberFromProject.fulfilled, (state, action) => {
      state.memberList = state.memberList.filter(
        (x) => x.employeeId !== action.payload
      );
    });
    builder.addCase(addMembersToProject.fulfilled, (state, action) => {
        console.log(action.payload);
      state.memberList = state.memberList.concat(action.payload);
    });
  },
});

export const {
  setAllProjectMembers,
  addProjectMembers,
  removeProjectMember,
  setDialogMember,
  setMemberListOpen,
  removeFromMember,
} = slice.actions;

const memberSelectors = memberAdapter.getSelectors(
  (state) => state.projectMember
);

export const {
  selectAll: selectAllMembers,
  selectEntities: selectMemberEntities,
  selectTotal: selectMembersTotal,
} = memberSelectors;

export default slice.reducer;
