import React from "react";
import { useSelector } from "react-redux";
import styled from "styled-components";
import { RootState } from "../store/store";
import { SignedInStatus } from "../store/auth";

const StyledFooter = styled.footer`
  border-top: 1px black solid;
  background-color: ${(props) => props.theme.backdrop};
  padding: 0.5rem 1rem;
  color: ${(props) => props.theme.secondary};
`;

const Footer: React.FC = () => {
  const signedInStatus = useSelector(
    (state: RootState) => state.auth.signedInStatus
  );

  const userEmail = useSelector((state: RootState) => state.auth.user.email);

  return (
    <StyledFooter>
      Footer
      <p>
        {signedInStatus === SignedInStatus.LoggedIn
          ? `You are signed in as: ${userEmail}`
          : "You need to sign in"}
      </p>
    </StyledFooter>
  );
};

export default Footer;
