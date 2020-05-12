import {
  combineReducers,
  configureStore,
  createAction,
  createReducer,
  createSlice,
} from "@reduxjs/toolkit";

import authReducer from "./auth";

const rootReducer = combineReducers(authReducer);

export const store = configureStore({
  reducer: {
    auth: authReducer,
  },
});

export type AppDispatch = typeof store.dispatch;

export type RootState = ReturnType<typeof store.getState>;
