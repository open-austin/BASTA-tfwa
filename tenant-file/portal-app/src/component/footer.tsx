import React from "react";
import { useSelector } from "react-redux";
import styled from "styled-components";
import { RootState } from "../store/store";

const StyledFooter = styled.footer`
  box-shadow: 0 -1px 10px rgba(0, 0, 0, 0.3);
  background-color: ${(props) => props.theme.backdrop};
  padding: 0.5rem 1rem;
  color: ${(props) => props.theme.secondary};
`;

const Footer: React.FC = () => {
  const profile = useSelector((state: RootState) => state.firebase.profile);

  return (
    <StyledFooter>
      Footer
      <p>
        {!profile.isEmpty
          ? `You are signed in as: ${profile.token.claims.email}`
          : "You need to sign in"}
      </p>
    </StyledFooter>
  );
};

export default Footer;
