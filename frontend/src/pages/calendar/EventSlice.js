import {
    createSlice,
    PayloadAction,
    createEntityAdapter,
    createAsyncThunk,
  } from "@reduxjs/toolkit";
  import { toast } from "react-toastify";
import axios from "axios";
import authHeader from "shared/utils/authHeader";

const eventAdapter = createEntityAdapter();

export const initialState = eventAdapter.getInitialState({
    formOpen:false,
    showDetail:null,
});

export const fetchEvents = createAsyncThunk(
    "event/fetchEventsStatus",
    async (_, { rejectWithValue }) => {
      try {
        var res = await axios({
          method: "GET",
          url:"/event",
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

  export const deleteEvent = createAsyncThunk(
    "event/deleteEventStatus",
    async (id, { rejectWithValue }) => {
      try {
        var res = await axios({
          method: "DELETE",
          url:`/event/${id}`,
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

  export const addEvent = createAsyncThunk(
    "event/addEventStatus",
    async (data, { rejectWithValue }) => {
      try {
        var res = await axios({
          method: "POST",
          url:"/event",
          headers: authHeader(),
          data:data
        });
        
        return res.data;
      } catch (err) {
        let [first] = Object.values(err.response.data.errors);
        toast.error(first[0]);
        return rejectWithValue(err.response.data.errors);
      }
    }
  );

export const slice = createSlice({
    name: "event",
    initialState,
    reducers: {
        setFormOpen:(state,action) =>{
            state.formOpen = action.payload;
        },
        setShowDetail:(state,action) =>{
            state.showDetail = action.payload;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchEvents.fulfilled,(state,action) =>{
            eventAdapter.setAll(
                state,
                action.payload.map((event) => ({
                    id: event.id,
                    title: event.title,
                    description:event.description,
                    start: new Date(event.start),
                    end: new Date(event.end),
                    color:event.color
                  }))
                );
        });
        builder.addCase(deleteEvent.fulfilled,(state,action) =>{
          eventAdapter.removeOne(state,action.payload);
      });
        builder.addCase(addEvent.fulfilled,(state,action) =>{
          eventAdapter.addOne(
            state,
            {
              id:action.payload.id,
              title:action.payload.title,
              description:action.payload.description,
              start:new Date(action.payload.start),
              end:new Date(action.payload.end),
              color:action.payload.color
            });
        });
    },
  });

export const eventSelectors = eventAdapter.getSelectors(
(state) => state.event
);

export const {
    selectAll : selectAllEvents,
    selectEntities:selectEventsEntities,
} =eventSelectors

export const {
    setFormOpen,
    setShowDetail,
  } = slice.actions;


export default slice.reducer;