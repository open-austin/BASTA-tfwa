import React from 'react';
import { useSelector } from 'react-redux';
import { NavLink } from 'react-router-dom';
import StyledFooter from './styles/FooterStyles';
import { RootState } from '../store/store';

// const StyledFooter = styled.footer`
//   box-shadow: 0 -1px 10px rgba(0, 0, 0, 0.3);
//   background-color: ${(props) => props.theme.backdrop};
//   padding: 0.5rem 1rem;
//   color: ${(props) => props.theme.secondary};
// `;

const Footer: React.FC = () => {
  const profile = useSelector((state: RootState) => state.firebase.profile);

  return (
    <>
      <StyledFooter>
        <NavLink to="#" tabIndex={0}>
          <i className="las la-file-alt"></i>
          <span>Create Account</span>
        </NavLink>
        <NavLink to="/" tabIndex={0}>
          <i className="las la-home"></i>
          <span>Home</span>
        </NavLink>
        <NavLink to="#" tabIndex={0}>
          <i className="las la-cog"></i>
          <span>Settings</span>
        </NavLink>
        <a href="#">
          <span>
            {!profile.isEmpty
              ? `You are signed in as: ${profile.token.claims.email}`
              : 'You need to sign in'}
          </span>
        </a>
      </StyledFooter>
    </>
  );
};

export default Footer;
