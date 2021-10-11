import {
    createSlice,
    createEntityAdapter,
    createAsyncThunk,
  } from '@reduxjs/toolkit'
  import { toast } from 'react-toastify';
  import axios from "axios";


  export const resetPassword = createAsyncThunk(
    'password/resetPassword', async (params,{rejectWithValue}) =>{
        try{
        console.log(params);
        // var response = await axios.post("/account/resetPassword",params);
        const requestOptions ={
            method:"POST",
            headers:{'Content-Type':'application/json'},
            body:JSON.stringify(params)
        };
        const response = await fetch("/account/resetPassword",requestOptions);
        if(response.status === 200){
            toast.success("Password Reset Success");
        }
       return response.data;
        }catch(err) {
            let [first] = Object.values(err.response.data.errors);
            toast.error(first[0])
            return rejectWithValue(err.response.data.errors);
        }
    }
);

const passAdapter = createEntityAdapter({
    selectId: (password) => password.id,
  });


export const slice = createSlice({
    name:"password",
    initialState:passAdapter.getInitialState({
        loading:false,
        errors:null,
        reset:false
    }),
    reducers:{
        clearErrors : (state) =>{
            state.errors = null;
        },
    },
    extraReducers: (builder) =>{
        builder.addCase(resetPassword.pending, (state) =>{
            state.loading = true;
        });
        builder.addCase(resetPassword.fulfilled,(state,action) =>{
            state.loading = false;
            state.errors = null;
            state.reset = true;
        });
        builder.addCase(resetPassword.rejected,(state,action) =>{
            state.errors = action.payload;
            state.loading = false;
            state.reset = false;
        });
    },
});

export const passSelectors = passAdapter.getSelectors(
    (state) => state.password
);

export const {clearErrors} = slice.actions;

export default slice.reducer;
