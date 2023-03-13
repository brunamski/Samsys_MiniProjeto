import React, { useState } from 'react';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';

function PaginationControlled({ totalItems, rowsPerPage }) {
  const [page, setPage] = useState(1);
  const [rows, setRowsPerPage] = useState(3);

  const handleChangePage = (event, value) => {
    setPage(value);
  };

  const handleChangeRowsPerPage = (event) => {
    setPage(1);
    setRowsPerPage(3);
  };

  const pageCount = Math.ceil(totalItems / rowsPerPage);

  return (
    <Stack spacing={2}>
      <Pagination
        count={pageCount}
        page={page}
        rowsPerPage={3}
        onChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </Stack>
  );
}

export default PaginationControlled;
