import React, { useState, useEffect } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import styled from 'styled-components';

const StyledNameSearch = styled.form`
  display: flex;
  width: 100%;
  /* border: 1px solid ${(props) => props.theme.backdrop}; */
  border-radius: 10px;
  margin: 1rem 0;
  position: relative;

  input {
    flex: 1;
    border: none;
    border: 1px solid ${(props) => props.theme.backdrop};
    border-right: none;
    border-radius: 10px 0 0 10px;
    padding: 0.2rem 0.6rem;
    transition: border 0.2s ease;
  }

  button {
    border: none;
    box-shadow: none;
    background-color: rgba(0, 0, 0, 0);
  }

  .clear {
    position: absolute;
    top: -0.1rem;
    right: 5rem;
    font-size: 1.3rem;
    cursor: pointer;
  }

  input:focus {
    border: 1px solid ${(props) => props.theme.secondary};
    border-radius: 10px 0 0 10px;
    border-right: none;
    outline: none;
  }

  button.submit {
    border: none;
    box-shadow: none;
    background-color: ${(props) => props.theme.backdrop};
    color: ${(props) => props.theme.primary};
    border-radius: 0 8px 8px 0;
    padding: 0 0.6rem;
  }

  .hide {
    display: none;
  }

  button:focus {
    color: ${(props) => props.theme.secondary};
  }
`;

const NameSearch = () => {
  const paramsString = useLocation().search;
  const searchParams = new URLSearchParams(paramsString);
  const nameQuery = searchParams.get('q');
  const [searchValue, setSearchValue] = useState(nameQuery || '');
  const history = useHistory();

  // If field is empty, clear filter
  useEffect(() => {
    if (searchValue.length === 0) {
      history.push(`/dashboard`);
    }
  }, [searchValue, history]);

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    setSearchValue(e.currentTarget.value);
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    history.push(`/dashboard?q=${searchValue}`);
  }

  function clearSearch(e: React.FormEvent) {
    e.preventDefault();
    setSearchValue('');
  }

  return (
    <StyledNameSearch onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Search by name... "
        value={searchValue}
        onChange={handleChange}
      />
      <button className="clear" onClick={clearSearch}>
        &times;
      </button>
      <button className="submit" type="submit">
        Search
      </button>
    </StyledNameSearch>
  );
};

export default NameSearch;
