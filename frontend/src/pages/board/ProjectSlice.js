import { createSlice, 
    PayloadAction, 
    createAsyncThunk,
    createEntityAdapter
} from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import axios from "axios";
import authHeader from 'shared/utils/authHeader';

export const fetchAllProjects = createAsyncThunk(
    "project/fetchAllProjectStatus",
    async (_, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"GET",
            url:"/project/all",
            headers:authHeader()
        });

        if (res.status === 200) {
            return res.data;
        }
      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const fetchProjectOverview = createAsyncThunk(
    "project/fetchProjectOverviewStatus",
    async (_, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"GET",
            url:"/project/overview",
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

  export const fetchProjectDetail = createAsyncThunk(
    "project/fetchProjectDetailStatus",
    async (id, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"GET",
            url:`/project/get/${id}`,
            headers:authHeader()
        });
        if (res.status === 200) {
            return res.data;
        }
      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

export const createNewProject = createAsyncThunk(
    "project/createProjectStatus",
    async (data, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"POST",
            url:"/project/addone",
            headers:authHeader(),
            data: data
        });
        if (res.status === 200) {
            return res.data;
        }
      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

  export const updateProject = createAsyncThunk(
    "project/updateProjectStatus",
    async (data, { rejectWithValue}) => {
      try {
        await axios({
            method:"PATCH",
            url:`/project/update/${data.projectId}`,
            headers:authHeader(),
            data: data
        });

        return data;
      } catch (err) {
          let [first] = Object.values(err.response.data.errors);
          toast.error(first[0])
          return rejectWithValue(err.response.data.errors);
      }
    }
  );

const projectAdapter = createEntityAdapter({
    selectId: (project) => project.projectId,
  });


  export const slice = createSlice({
    name:"project",
    initialState:projectAdapter.getInitialState({
        all:null,
        loading:false,
        errors:null,
        detail:null,
        createDialogOpen:false,
        editDialogOpen:false,
        overview: null,
    }),
    reducers:{
        setCreateDialogOpen : (state,action) =>{
            state.createDialogOpen = action.payload;
        },
        setEditDialogOpen : (state,action) =>{
            state.editDialogOpen = action.payload;
        },
    },
    extraReducers: (builder) =>{
        builder.addCase(fetchAllProjects.pending, (state) =>{
            state.loading = true;
        });
        builder.addCase(fetchAllProjects.fulfilled,(state,action)=>{
            state.loading = false;
            state.all = action.payload;
        });
        builder.addCase(fetchAllProjects.rejected,(state,action)=>{
            state.errors = action.payload;
            state.loading = false;
        });
        builder.addCase(createNewProject.pending, (state) =>{
            state.createLoading = true;
            state.loading = true;
        });
        builder.addCase(createNewProject.fulfilled,(state,action)=>{
            state.loading = false;
            state.createDialogOpen = false;
            state.all.push(action.payload);
            state.errors = null;
        });
        builder.addCase(createNewProject.rejected,(state,action)=>{
            state.errors = action.payload;
            state.loading = false;
        });
        builder.addCase(fetchProjectDetail.pending, (state) =>{
            state.loading = true;
        });
        builder.addCase(fetchProjectDetail.fulfilled,(state,action)=>{
            const {
                projectId,
                name,
                description,
                employeeLeaderId,
                projectUser,
                dateUpdated,
                dateCreated,
                status,
            } = action.payload;
            state.detail = {projectId,name,description,employeeLeaderId,projectUser,dateUpdated,dateCreated,status};
            state.loading = false;
            state.errors = null;
        });
        builder.addCase(fetchProjectDetail.rejected,(state,action)=>{
            state.errors = action.payload;
            state.loading = false;
        });
        builder.addCase(updateProject.fulfilled,(state,action)=>{
            state.detail = action.payload;
        });
        builder.addCase(fetchProjectOverview.fulfilled,(state,action)=>{
            state.overview = action.payload;
        });
    },
});

export const {setCreateDialogOpen,setEditDialogOpen} = slice.actions;

export const projectSelectors = projectAdapter.getSelectors(
    (state) => state.projectAdapter
);

export const projectOwner = (state) =>{

    if(state.profile.userDetail !== null){
        return(
            state.project?.employeeLeaderId === state.profile.userDetail?.employeeId
        );
    }

    return false;
};

export default slice.reducer;