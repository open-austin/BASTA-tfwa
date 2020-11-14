import React, { useState } from 'react';
import { useHistory, useLocation } from 'react-router-dom';

const NameSearch = () => {
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
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Search by name... "
        value={searchValue}
        onChange={handleChange}
      />
      <button type="submit">Search</button>
    </form>
  );
};

export default NameSearch;
