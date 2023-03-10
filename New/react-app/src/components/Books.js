import React, { useState, useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import * as actions from "../actions/book";
import { Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TableSortLabel, Button, ButtonGroup} from '@mui/material';
//import { toast } from 'react-toastify';

import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import BookForm from './BookForm';
//import Pagination from './Pagination';

/*const styles = (theme) => ({
  paper: {
    margin: theme.spacing(2),
    padding: theme.spacing(2),
  }
  // define other styles here
});*/

const Books = (props) => {

  const [orderBy, setOrderBy] = useState('isbn');
  const [order, setOrder] = useState('asc');

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

  const handleUpdate = (isbn) => {
    
  }




    props.bookList.sort((a, b) => {
    const isDesc = order === 'desc';
    if (orderBy === 'name') {
      return isDesc ? b.name.localeCompare(a.name) : a.name.localeCompare(b.name);
    }
    if (orderBy === 'author') {
      return isDesc ? b.author.localeCompare(a.author) : a.author.localeCompare(b.author);
    }
    if (orderBy === 'preco') {
      return isDesc ? b.preco - a.preco : a.preco - b.preco;
    }
    return isDesc ? b.isbn.localeCompare(a.isbn) : a.isbn.localeCompare(b.isbn);
  });

  return ( 

    <Paper>
      <Grid container>
        <Grid item xs={6}>
          <BookForm/>
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
                      active={orderBy === 'preco'}
                      direction={order}
                      onClick={() => handleSort('preco')}
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
                                            <TableCell>{record.preco}</TableCell>
                                            <TableCell>
                                              <ButtonGroup variant="text">

                                              <Button><EditIcon color="primary"
                                                       onClick={() => handleUpdate(record.isbn)}/>
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