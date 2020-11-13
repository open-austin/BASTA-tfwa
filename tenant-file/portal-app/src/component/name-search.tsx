import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';

const NameSearch = () => {
  const [searchValue, setSearchValue] = useState('');
  const history = useHistory();

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    setSearchValue(e.currentTarget.value);
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    console.log(searchValue);
    history.push(`/${searchValue}`);
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
