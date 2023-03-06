import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import * as actions from "../actions/book";
import { Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow,} from '@mui/material';
import { styled } from '@mui/styles';

import BookForm from './BookForm';

const styles = (theme) => ({
    paper: {
      margin: theme.spacing(2),
      padding: theme.spacing(2),
    }
    // define other styles here
  });

const Books = (props) => {
//const [isbn, setIsbn] = useState(0000000000001)

        useEffect(() => {
        props.fetchAllBooks()
    }, [])//componentDidMount

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
                                <TableCell>ISBN</TableCell>
                                <TableCell>Name</TableCell>
                                <TableCell>Author</TableCell>
                                <TableCell>Price</TableCell>
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
    fetchAllBooks: actions.fetchAll
}


export default connect(mapStateToProps, mapActionToProps)(Books);