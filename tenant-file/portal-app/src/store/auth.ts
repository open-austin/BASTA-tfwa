import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState = {
  signedIn: false,
  token: "",
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
      state.signedIn = action.payload;
    },
    setUserInfo: (state, action: PayloadAction<UserData>) => {
      state.user.email = action.payload.email;
      state.user.admin = action.payload.admin;
    },
  },
});

export const { addToken, setSignedIn, setUserInfo } = authSlice.actions;

export default authSlice.reducer;
