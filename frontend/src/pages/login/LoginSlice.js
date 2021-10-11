import {
    createSlice,
    createEntityAdapter,
    createAsyncThunk,
  } from '@reduxjs/toolkit'
  import { toast } from 'react-toastify';
  import axios from "axios";

export const login = createAsyncThunk(
    'auth/loginStatus', async (credentials,{rejectWithValue}) =>{
        try{
        var response = await axios.post("/account/login",credentials);
        if(response.status === 200){
            const token = `Bearer ${response.data.token}`;
            sessionStorage.setItem("accessJWT",token);
            localStorage.setItem("ims",response.data.refreshToken);
            axios.defaults.headers.common['Authorization'] = token;
        }
       return response.data;
        }catch(err) {
            let [first] = Object.values(err.response.data.errors);
            toast.error(first[0])
            return rejectWithValue(err.response.data.errors);
        }
    }
);

export const logout = createAsyncThunk(
    'auth/logoutstatus',async ({rejectWithValue}) =>{
        try{
            await axios.delete("/account/logout");
            delete axios.defaults.headers.common['Authorization'];
        }catch(err){
            let [first] = Object.values(err.response.data.errors);
            toast.error(first[0])
            return rejectWithValue(err.response.data.errors);
        }finally{
            toast.info("Logged out");
        }
    }
);

export const fetchNewAccessJWT = createAsyncThunk(
    'auth/refreshToken',async (refreshToken,{rejectWithValue}) => {
        try{
            const res = await axios.get(`/account/refreshToken/${refreshToken}`);
            console.log(res);

            if(res.status === 200){
                const token = `Bearer ${res.data.token}`;
                sessionStorage.setItem("accessJWT", token);
                axios.defaults.headers.common['Authorization'] = token;
            }
            return res.data;
        }catch(error){
            console.log(error)
            return rejectWithValue(error.res.data.errors);
        }   
    }
);

export const emailValidation = createAsyncThunk(
    'auth/emailValidate',async(params,{rejectWithValue}) =>{
        try{
            const requestOptions ={
                method:"POST",
                headers:{'Content-Type':'application/json'},
                body:JSON.stringify(params)
            };
            const response = await fetch("/account/validateEmail",requestOptions);
            if(response.status === 200){
                toast.success("Account Validation Success");
            }
        }catch(error){
            return rejectWithValue(error.response.data.errors)
        }
    }
)

export const register = createAsyncThunk(
    'auth/register',async(params,{rejectWithValue}) =>{
        try{
            const res = await axios.post("/account/register",params)
            if(res.status === 200){
                toast.success("Verification Email have been sended,please check");
            }
        }catch(err){
            let [first] = Object.values(err.response.data.errors);
            toast.error(first[0])
            return rejectWithValue(err.response.data.errors);
        }
    }
);

export const verifyEmail = createAsyncThunk(
    'auth/emailVerifyStatus',async(params,{rejectWithValue}) =>{
        try{
            const res = await axios.post("/account/validateEmail",params)
            if(res.status === 200){
                toast.success("Verification Email Success");
            }
        }catch(err){
            let [first] = Object.values(err.response.data.errors);
            toast.error(first[0])
            return rejectWithValue(err.response.data.errors);
        }
    }
)

const authAdapter = createEntityAdapter({
    selectId: (auth) => auth.id,
  });

export const slice = createSlice({
    name:"auth",
    initialState:authAdapter.getInitialState({
        isAuth:false,
        empId:'',
        loginLoading:false,
        loginErrors:null,
        registerErrors:null,
        refreshLoginError:null,
        loading:false,
        verify:false
    }),
    reducers:{
        clearErrors : (state) =>{
            state.loginErrors = null;
            state.registerErrors = null;
        },
        setLogin : (state) =>{
            state.isAuth = true;
        },
        setVerify:(state,action)=>{
            state.verify =action.payload;
        }
    },
    extraReducers: (builder) =>{
        builder.addCase(login.pending, (state) =>{
            state.loginErrors = true;
        });
        builder.addCase(login.fulfilled,(state,action) =>{
            state.isAuth = true;
            state.empId = action.payload.empId;
            state.loginLoading = false;
            state.loginErrors = null;
        });
        builder.addCase(login.rejected,(state,action) =>{
            state.loginErrors = action.payload;
            state.loginLoading = false;
        });
        builder.addCase(register.pending,(state) =>{
            state.loading = true;
        });
        builder.addCase(register.rejected,(state,action) =>{
            state.registerErrors = action.payload;
            state.loading = false;
        });
        builder.addCase(register.fulfilled,(state,action) =>{
            state.registerErrors = null;
            state.loading = false;
        });
        builder.addCase(fetchNewAccessJWT.fulfilled,(state,action) =>{
            state.empId = action.payload.empId;
            state.isAuth = true;
        });
        builder.addCase(fetchNewAccessJWT.rejected,(state,action) =>{
            state.isAuth = false;
            state.refreshLoginError = action.payload
        });
        builder.addCase(emailValidation.pending,(state) =>{
            state.loginLoading = true;
        });
        builder.addCase(emailValidation.fulfilled,(state) =>{
            state.loginLoading = false;
        });
        builder.addCase(emailValidation.rejected,(state,action) =>{
            state.loginLoading = false;
            state.refreshLoginError = action.payload
        });
        builder.addCase(logout.fulfilled,(state) =>{
            state.isAuth = false;
            state.user = null;
        });
        builder.addCase(logout.rejected, (state) => {
            state.isAuth = false;
            state.user = null;
        });
        builder.addCase(verifyEmail.fulfilled,(state) =>{
            state.verify = true;
            state.loading = false;
        });
        builder.addCase(verifyEmail.pending,(state) =>{
            state.loading = true;
        });
        builder.addCase(verifyEmail.rejected,(state) =>{
            state.loading = false;
        });
    },
});

export const authSelectors = authAdapter.getSelectors(
    (state) => state.auth
);

export const {clearErrors,setLogin,setVerify} = slice.actions;

export default slice.reducer;




