import { configureStore, combineReducers, getDefaultMiddleware } from "@reduxjs/toolkit";
import { firebaseReducer, actionTypes } from "react-redux-firebase";

import authReducer from "./auth";

const rootReducer = combineReducers({
  auth: authReducer,
  firebase: firebaseReducer,
});

export const store = configureStore({
  reducer: rootReducer,
  // Weird bug and we are removing it by using this for now: https://github.com/prescottprue/react-redux-firebase/issues/761
  // If you want to see if the bug is fixed, remove the belwo + go to the /login page and log in and see if an error occurs
  middleware: getDefaultMiddleware({
    serializableCheck: {
        ignoredActions: [actionTypes.LOGIN, actionTypes.SET, actionTypes.SET_PROFILE]
    }
  }),
});

export type AppDispatch = typeof store.dispatch;

export type RootState = ReturnType<typeof rootReducer>;
