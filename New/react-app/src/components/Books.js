import React, { useState, useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import * as actions from "../actions/book";
import { Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TableSortLabel,
  TextField, Typography, Button, ButtonGroup} from '@mui/material';
//import { toast } from 'react-toastify';

import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import BookForm from './BookForm';
import Pagination from './Pagination';
import NavBar from './NavBar';



const Books = (props) => {

  const [orderBy, setOrderBy] = useState('isbn');
  const [order, setOrder] = useState('asc');
  const [currentIsbn, setCurrentIsbn] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(3);

  const [searchTerm, setSearchTerm] = useState('');
  const [filteredBooks, setFilteredBooks] = useState([]);


  const handleSort = useCallback((property) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrderBy(property);
    setOrder(isAsc ? 'desc' : 'asc');
  }, [orderBy, order]);


  useEffect(() => {
    props.fetchAllBooks()
  }, [])//componentDidMount

  
  //const { addToast } = toast() 
  const handleDelete = isbn => {
    if(window.confirm('Are you sure you want to delete this book?'))
    return props.deleteBook(isbn,()=>{window.alert('Deleted successfully')})
  }

  const [selectedBook, setSelectedBook] = useState(null);





  const handleSearchISBN = (e) => {
    if (e.key === 'Enter') {
      const result = props.bookList.find((book) => book.isbn === searchTerm);
      if (result) {
        setFilteredBooks([result]);
        alert('Book Found.');
      } else {
        setFilteredBooks([]);
        alert('Book not Found.');
      }
    }
  };
  




  //sorting

    props.bookList.sort((a, b) => {
    const isDesc = order === 'desc';
    if (orderBy === 'name') {
      return isDesc ? b.name.localeCompare(a.name) : a.name.localeCompare(b.name);
    }
    if (orderBy === 'author') {
      return isDesc ? b.author.localeCompare(a.author) : a.author.localeCompare(b.author);
    }
    if (orderBy === 'price') {
      return isDesc ? b.price - a.price : a.price - b.price;
    }
    return isDesc ? b.isbn.localeCompare(a.isbn) : a.isbn.localeCompare(b.isbn);
  });

  return ( 

    <Paper>
      <Grid container>
        <Grid item xs={6}>
        <BookForm {...({ currentIsbn, setCurrentIsbn })}/>
        </Grid>
        <Grid item xs={6}>
          <TableContainer>
            <Table>

              <TableHead>
                <TableRow>
                  <TableCell>
                    <TableSortLabel
                      active={orderBy === 'isbn'}
                      direction={order}
                      onClick={() => handleSort('isbn')}
                    >
                      ISBN
                    </TableSortLabel>
                  </TableCell>
                  <TableCell>
                    <TableSortLabel
                      active={orderBy === 'name'}
                      direction={order}
                      onClick={() => handleSort('name')}
                    >
                      Name
                    </TableSortLabel>
                  </TableCell>
                  <TableCell>
                    <TableSortLabel
                      active={orderBy === 'author'}
                      direction={order}
                      onClick={() => handleSort('author')}
                    >
                      Author
                    </TableSortLabel>
                  </TableCell>
                  <TableCell>
                    <TableSortLabel
                      active={orderBy === 'price'}
                      direction={order}
                      onClick={() => handleSort('price')}
                    >
                      Price

                            </TableSortLabel>
                            </TableCell>
                        </TableRow>
                        </TableHead>

                            <TableBody>
                                {
                                    props.bookList.map((record, index) => {
                                        return (<TableRow key={index} hover>
                                            <TableCell>{record.isbn}</TableCell>
                                            <TableCell>{record.name}</TableCell>
                                            <TableCell>{record.author}</TableCell>
                                            <TableCell>{record.price}</TableCell>
                                            <TableCell>
                                              <ButtonGroup variant="text">

                                              <Button><EditIcon color="primary"
                                                       onClick={() => { setCurrentIsbn(record.isbn)}}/>
                                              </Button>

                                              <Button><DeleteIcon color="secondary"
                                                        onClick={() => handleDelete(record.isbn)}/>
                                              </Button>

                                              </ButtonGroup>
                                            </TableCell>
                                        </TableRow>)
                                    })
                                }
                            </TableBody>
                            
                  </Table>

                 
                <Pagination align="left"
                  totalItems={props.bookList.length}
                  rowsPerPage={rowsPerPage}
                />
         

                  
                        <TextField
                            label="Search by ISBN"
                            variant="outlined"
                            size="small"
                            margin="dense"
                            onChange={(e) => setSearchTerm(e.target.value)}
                            onKeyDown={handleSearchISBN}
                          />

                          <TableBody>
                            {filteredBooks.map((record, index) => {
                              return (
                                <TableRow key={index} hover>
                                  <TableCell>{record.isbn}</TableCell>
                                  <TableCell>{record.name}</TableCell>
                                  <TableCell>{record.author}</TableCell>
                                  <TableCell>{record.price}</TableCell>
                                </TableRow>
                              );
                            })}
                          </TableBody>    


                </TableContainer>
                            
            </Grid>
              
        </Grid>
    </Paper>
    );
}

const mapStateToProps = state => ({  
        bookList: state.book.list
    })

const mapActionToProps = {
    fetchAllBooks: actions.fetchAll,
    deleteBook: actions.Delete,
    updateBook: actions.update
}


export default connect(mapStateToProps, mapActionToProps)(Books);