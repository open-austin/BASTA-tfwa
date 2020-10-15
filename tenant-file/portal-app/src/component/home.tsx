import React from "react";
import { ReactComponent as Logo } from "../images/logo.svg";
import styled from 'styled-components';

const StyledHeader = styled.div`
  font-size: 100px;
  color: ${(props) => props.theme.darkSecondary};
  font-weight: 700;
  text-align: center;

  span {
    align-text: center;
  }  
`;

const Home: React.FC = () => (
    // App logo and title
    // <AppLogoAndTitle />
    <StyledHeader>
      <span>Home</span>
      <Logo />
    </StyledHeader>
    // Home
  );
  
  export default Home;
  