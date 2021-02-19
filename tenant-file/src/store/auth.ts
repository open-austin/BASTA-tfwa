import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export enum SignedInStatus {
  Unknown,
  LoggedOut,
  LoggedIn,
}

const initialState = {
  token: "",
  signedInStatus: SignedInStatus.Unknown,
  user: {
    admin: false,
    email: "",
  },
};

interface UserData {
  email: string;
  admin: boolean;
}

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    addToken: (state, action: PayloadAction<string>) => {
      state.token = action.payload;
    },
    setSignedIn: (state, action: PayloadAction<boolean>) => {
      state.signedInStatus = action.payload
        ? SignedInStatus.LoggedIn
        : SignedInStatus.LoggedOut;
    },
    setUserInfo: (state, action: PayloadAction<UserData>) => {
      state.user.email = action.payload.email;
      state.user.admin = action.payload.admin;
    },
  },
});

export const { addToken, setSignedIn, setUserInfo } = authSlice.actions;

export default authSlice.reducer;
