import React, {useState} from 'react';
import { Grid, TextField, MenuItem, Button, makeStyles } from '@mui/material';
import useForm from './useForm';
import { connect } from 'react-redux';
import * as actions from "../actions/book"



const initalFieldValues = {
    isbn: '',
    name:'',
    author:'',
    preco:''
}

const authors = [
    {
      value: 'Author1',
      label: 'Author1',
    },
    {
      value: 'Author2',
      label: 'Author2',
    },
    {
      value: 'Author3',
      label: 'Author3',
    },
  ];

  
const BookForm = (props) => {

    const [selectedAuthor, setSelectedAuthor] = useState("");

    const {
        values,
        setValues,
        handleInputChange
    } =  useForm(initalFieldValues)

    const [errors, setErrors] = useState({});

    //validations
    
    const validate = () => {
        let errors = {};
        let isValid = true;
    
        if (!values.isbn) {
          errors.isbn = 'ISBN field cannot be empty';
          isValid = false;
        } else if (!/^\d{13}$/.test(values.isbn)) {
          errors.isbn = 'ISBN must be a 13 digit number';
          isValid = false;
        }
    
        if (!values.name) {
          errors.name = 'Name field cannot be empty';
          isValid = false;
        }
    
        if (!selectedAuthor) {
          errors.author = 'Author field cannot be empty';
          isValid = false;
        }
    
        if (!values.preco) {
          errors.preco = 'Price field cannot be empty';
          isValid = false;
        } else if (values.preco < 0) {
          errors.preco = 'Price cannot be negative';
          isValid = false;
        }
    
        setErrors(errors);
        return isValid;
      };
      
      //submit new book validations

      const handleSubmit = e => {
        e.preventDefault();
    
        if (validate) {
            console.log(values)
          props.createBook(values,()=>{window.alert('Created successfully')})

        } else {
            return window.alert('Submitted failed')
        }
      };

      //reset fields func
      const handleReset = () => {
        setValues(initalFieldValues);
        setSelectedAuthor("");
      };


    return ( 
        <form autoComplete='off' noValidate onSubmit={handleSubmit}>
            <Grid container padding={2}>
                <Grid item xs={6}>
                    <TextField margin="normal" required
                    name="isbn"
                    variant='outlined'
                    label="ISBN"
                    value = {values.isbn}
                    onChange = {handleInputChange}
                    error={errors.isbn}
                    helperText={errors.isbn}
                    />

                    <TextField margin="normal" required
                    name="name"
                    variant='outlined'
                    label="Name"
                    value = {values.name}
                    onChange = {handleInputChange}
                    error={errors.name}
                    helperText={errors.name}
                    />

                </Grid>  
                
                <Grid item xs={6}>
                
                <div>
                    <TextField required
                        margin="normal"
                        fullWidth
                        id="outlined-select-author"
                        label="Author"
                        variant="outlined"
                        SelectProps={{
                        style: { width: "77%" },
                        }}
                        select
                        value={values.author}
                        onChange={(e) => handleInputChange({...values, author: e.target.value})}
                        error={errors.author}
                        helperText={errors.author}
                    >
                        <MenuItem value={""}>Please select an Author</MenuItem>
                        {authors.map((option) => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
                        </MenuItem>
                        ))}
                    </TextField>
                    </div>

                <TextField margin="normal" required
                    name="preco"
                    variant='outlined'
                    label="Price"
                    value = {values.price}
                    onChange = {handleInputChange}
                    error={errors.price}
                    helperText={errors.price}
                />

                <Grid container spacing={0.5}>

                    <Grid item>
                        <Button variant="contained" size="small" type="submit">
                        Submit Book
                        </Button>
                    </Grid>

                    <Grid item>
                        <Button
                        variant="contained"
                        color="secondary"
                        size="small"
                        onClick={handleReset}
                        >
                        Reset Fields
                    </Button>
                    </Grid>

                 </Grid>
                
            </Grid>
        </Grid>
     </form>
    );
}

const mapStateToProps = state => ({  
    bookList: state.book.list
})

const mapActionToProps = {

    //from actions/book.js
    createBook: actions.create,
    updateBook: actions.update
}

export default connect (mapStateToProps, mapActionToProps)(BookForm);