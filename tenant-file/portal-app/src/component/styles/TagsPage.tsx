import styled from 'styled-components';

const StyledTagsDisplay = styled.div`
  width: 95%;
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  border-radius: 10px;

  .display,
  .header,
  .body,
  .flex-row {
    width: 100%;
  }

  .header,
  .flex-row {
    padding: 0.5rem;
    padding: 1rem;
  }

  .header {
    display: flex;
    background-color: rgba(0, 0, 0, 0.08);
    border-radius: 8px;

    & > * {
      flex: 1;
    }

    .title {
      font-weight: 700;
    }

    .add {
      text-align: right;

      button {
        width: 30px;
        height: 30px;
        border: none;
        border-radius: 50%;
        background-color: rgba(0, 0, 0, 0.04);
        text-align: center;
        position: relative;
        transition: background-color ease 0.4s;

        &:hover {
          background-color: rgba(0, 0, 0, 0.4);
        }

        span {
          display: block;
          position: absolute;
          top: 2px;
          left: 10px;
        }
      }
    }
  }

  .flex-row {
    display: flex;
    justify-content: center;
    font-size: 0.8rem;
    border-bottom: 1px solid rgba(0, 0, 0, 0.3);

    & > * {
      flex: 1;
      display: flex;
      align-items: center;
    }
  }

  .body .flex-row:last-child {
    border-bottom: none;
    border-radius: 0 0 8px 8px;
  }

  .buttons {
    justify-content: flex-end;

    button {
      border: none;
      box-shadow: none;
      background-color: ${(props) => props.theme.backdrop};
      color: ${(props) => props.theme.primary};
      border-radius: 8px;
      padding: 0.3rem 0.8rem;
      font-size: 0.8rem;
      transition: color 0.4s ease;
    }

    button:hover {
      color: ${(props) => props.theme.accent};
    }

    button:first-child {
      border-radius: 8px 0 0 8px;
      border-right: 1px solid white;
      width: 64px;
    }

    button:last-child {
      border-radius: 0 8px 8px 0;
    }
  }
`;

export default StyledTagsDisplay;
