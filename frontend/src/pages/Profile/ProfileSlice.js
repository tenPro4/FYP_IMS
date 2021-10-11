import {
  createSlice,
  createEntityAdapter,
  createAsyncThunk,
} from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import axios from "axios";
import authHeader from 'shared/utils/authHeader';

export const fetchUserDetail = createAsyncThunk(
  "profile/fetchUserDetailStatus",
  async (_, { rejectWithValue}) => {
    try {
      var res = await axios({
          method:"GET",
          url:"/employee/profile",
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

export const fetchUserDetailWithId = createAsyncThunk(
    "profile/fetchUserDetailWithIdStatus",
    async (id, { rejectWithValue}) => {
      try {
        var res = await axios({
            method:"GET",
            url:`/employee/get/${id}`,
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

export const updateUserAvatar = createAsyncThunk(
    "profile/updateAvatarStatus",
async(data,{rejectWithValue}) =>{
        try{
            let token = sessionStorage.getItem("accessJWT");
            var res= await axios({
                method:"POST",
                url:"/employeeImage/postImage",
                headers:{
                    'Authorization':token
                },
                data:data
            })
            return res.data;

        }catch(err){
            return rejectWithValue(err.response.data.errors);
        }
    }
);

export const updateUserBasicInformation = createAsyncThunk(
    "profile/updateEmpBasicInfoStatus",
    async(data,{rejectWithValue}) =>{
        try{
            const id = data.empId;
            let token = sessionStorage.getItem("accessJWT");
            console.log(data);
            await axios({
                method:"PATCH",
                url:`/employee/profile/${id}`,
                headers:authHeader(),
                data:data
            })
            toast.success("Profile Update Success");
        }catch(err){

        }
    }
)

export const changeUserPassword = createAsyncThunk(
    "profile/changePasswordStatus",
    async(data,{rejectWithValue}) =>{
        try{
            await axios({
                method:"POST",
                url:"/account/changePassword",
                headers:authHeader(),
                data:data
            })
            toast.success("Password Update Success");
        }catch(error){

        }
    }
);

export const changeUserAddress = createAsyncThunk(
    "profile/changeAddressStatus",
    async(data,{rejectWithValue}) =>{
        try{
            await axios({
                method:"PATCH",
                url:`/employeeImage/address/${data.empId}`,
                headers:authHeader(),
                data:data
            });
            toast.success("Address Update Success");
        }catch(error){

        }
    }
);

const profileAdapter = createEntityAdapter({
    selectId: (profile) => profile.id,
  });

export const slice = createSlice({
    name:"profile",
    initialState:profileAdapter.getInitialState({
        userDetail:null,
        loading:false,
        errors:null,
        avatarLoading:false,
        userAvatar:null,
        update:false,
    }),
    reducers:{
        clearErrors : (state) =>{
            state.errors = null;
        },
    },
    extraReducers: (builder) =>{
        builder.addCase(fetchUserDetail.pending, (state) =>{
            state.loading = true;
        });
        builder.addCase(fetchUserDetail.fulfilled,(state,action)=>{
            state.userDetail = action.payload;
            state.userAvatar = action.payload.employeeImage;
        });
        builder.addCase(fetchUserDetail.rejected,(state,action)=>{
            state.errors = action.payload;
        });
        builder.addCase(fetchUserDetailWithId.pending, (state) =>{
            state.loading = true;
        });
        builder.addCase(fetchUserDetailWithId.fulfilled,(state,action)=>{
            state.userDetail = action.payload;
            state.userAvatar = action.payload.employeeImage;
        });
        builder.addCase(fetchUserDetailWithId.rejected,(state,action)=>{
            state.errors = action.payload;
        });
        builder.addCase(updateUserAvatar.pending,(state) =>{
            state.avatarLoading = true;
        });
        builder.addCase(updateUserAvatar.fulfilled,(state,action) =>{
            state.avatarLoading = false;
            state.userAvatar = action.payload;
        });
        builder.addCase(updateUserAvatar.rejected,(state,action) =>{
            state.avatarLoading = false;
            state.errors = action.payload;
        });
        builder.addCase(updateUserBasicInformation.pending,(state) =>{
        });
        builder.addCase(updateUserBasicInformation.fulfilled,(state,action) =>{
            state.update = true;
        });
        builder.addCase(updateUserBasicInformation.rejected,(state,action) =>{
            state.update = false;
        });
    },
});

export const profileSelectors = profileAdapter.getSelectors(
    (state) => state.profile
);

export const {clearErrors} = slice.actions;

export default slice.reducer;