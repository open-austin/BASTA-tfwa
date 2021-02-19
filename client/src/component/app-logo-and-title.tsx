import React from 'react';
import { ReactComponent as ToolLogo } from '../images/tools-logo.svg';
import styled from 'styled-components';
import { NavLink } from 'react-router-dom';

const StyledLogo = styled.div`
  font-size: 1.5rem;
  color: ${(props) => props.theme.primary};
  font-weight: 700;

  span,
  a {
    margin: 0 0.25rem;
  }

  a {
    color: inherit;
    &:hover {
      color: inherit;
      text-decoration: none;
    }
  }

  .tool_logo {
    position: relative;
    bottom: 3px;
  }
`;

const AppLogoAndTitle = () => {
  return (
    <StyledLogo>
      <NavLink exact to="/" activeClassName="active">
        <span>
          <ToolLogo />
        </span>
        <span>Tenant File</span>
      </NavLink>
    </StyledLogo>
  );
};

export default AppLogoAndTitle;
