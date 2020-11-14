import React, { useState } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import styled from 'styled-components';

const StyledNameSearch = styled.form`
  display: flex;
  width: 100%;
  /* border: 1px solid ${(props) => props.theme.backdrop}; */
  border-radius: 10px;
  margin: 1rem 0;

  input {
    flex: 1;
    border: none;
    border: 1px solid ${(props) => props.theme.backdrop};
    border-radius: 10px 0 0 10px;
    padding: 0.2rem 0.6rem;
    transition: border 0.4s ease;
  }

  input:focus {
    border: 1px solid ${(props) => props.theme.secondary};
    outline: none;
  }

  button {
    background-color: ${(props) => props.theme.backdrop};
    border: none;
    color: ${(props) => props.theme.primary};
    border-radius: 0 8px 8px 0;
    padding: 0 0.6rem;
    box-shadow: none;
  }

  button:focus {
    color: ${(props) => props.theme.secondary};
  }
`;

const NameSearch = () => {
  // TODO: ADD CLEAR BUTTON

  const paramsString = useLocation().search;
  const searchParams = new URLSearchParams(paramsString);
  const nameQuery = searchParams.get('q');
  const [searchValue, setSearchValue] = useState(nameQuery || '');
  const history = useHistory();

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    setSearchValue(e.currentTarget.value);
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    history.push(`/dashboard?q=${searchValue}`);
  }

  return (
    <StyledNameSearch onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Search by name... "
        value={searchValue}
        onChange={handleChange}
      />
      <button type="submit">Search</button>
    </StyledNameSearch>
  );
};

export default NameSearch;
