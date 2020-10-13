import { configureStore, combineReducers } from "@reduxjs/toolkit";
import { firebaseReducer } from "react-redux-firebase";

import authReducer from "./auth";

const rootReducer = combineReducers({
  auth: authReducer,
  firebase: firebaseReducer,
});

export const store = configureStore({
  reducer: rootReducer,
});

export type AppDispatch = typeof store.dispatch;

export type RootState = ReturnType<typeof rootReducer>;
