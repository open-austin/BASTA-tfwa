import React from "react";
import FirebaseAuth from "./firebase";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";



const Login: React.FC = () => {
    // Only show the log in component if you are not already logged in
    const profile = useSelector((state: RootState) => state.firebase.profile);

    return (
        <div className="log-in-or-out">
        {profile.isLoaded && !profile.isEmpty ? (
            <span>You are already logged in.</span>
        ) : (<FirebaseAuth />)}
      </div>

    );
;
}


export default Login;
