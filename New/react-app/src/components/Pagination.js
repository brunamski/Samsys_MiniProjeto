import { useState } from 'react';
import { Pagination as MuiPagination } from '@mui/material';

const Pagination = ({ count, page, onChange }) => {
  const [currentPage, setCurrentPage] = useState(page);

  const handlePageChange = (event, value) => {
    setCurrentPage(value);
    if (onChange) {
      onChange(value);
    }
  };

  return (
    <MuiPagination
      count={count}
      page={currentPage}
      onChange={handlePageChange}
    />
  );
};

export default Pagination;
