import React from "react";
import bastaaustin from "../images/bastaaustin.png";
import styled from "styled-components";

const StyledHeader = styled.div`
  font-size: 100px;
  color: ${(props) => props.theme.darkSecondary};
  font-weight: 700;
  text-align: left;
  

  .main-header {
    text-align: center;
    vertical-align: top;
    
  }

  .main-image {
    width: 50%;
  }
`;

const Home: React.FC = () => (
  <StyledHeader>
    <img className="main-image" src={bastaaustin} alt="basta logo" />
    <span className="main-header">Home</span>
  </StyledHeader>
);

export default Home;
