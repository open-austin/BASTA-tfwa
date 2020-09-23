import styled from 'styled-components';

const StyledExportToolbar = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0.5rem 1rem;
  background-color: ${(props) => props.theme.backdrop};
  color: ${(props) => props.theme.primary};
  box-shadow: 0 1px 10px rgba(0, 0, 0, 0.3);
  z-index: 2;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 1.2rem;
  padding: 0.5rem 1rem;
  font-weight: 700;

  span {
    flex: 1;
    text-align: center;
    transition: color 0.4s ease;

    &:hover {
      color: ${({ theme }) => theme.accent};
      cursor: pointer;
    }
  }
`;

export default StyledExportToolbar;

// import styled from 'styled-components';

// const StyledFooter = styled.footer`
//   background-color: ${(props) => props.theme.backdrop};
//   color: #fefad4;
//   width: 100vw;
//   display: flex;
//   justify-content: center;
//   align-items: center;
//   font-size: 1.5rem;
//   padding: 0.5rem 1rem;

//   a {
//     flex: 1;
//     text-align: center;
//     text-decoration: none;
//     color: ${(props) => props.theme.primary};
//     display: flex;
//     flex-direction: column;
//     transition: color 0.4s ease;

//     span {
//       font-size: 0.5em;
//     }

//     &:hover {
//       color: ${(props) => props.theme.accent};
//     }
//   }
// `;

// export default StyledFooter;
