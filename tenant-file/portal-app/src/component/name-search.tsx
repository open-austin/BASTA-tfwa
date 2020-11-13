import React, { useState } from 'react';

const NameSearch = () => {
  const [searchValue, setSearchValue] = useState('');

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    setSearchValue(e.currentTarget.value);
  }

  return (
    <form>
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
