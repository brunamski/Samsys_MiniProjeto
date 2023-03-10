import React, {useEffect, useState} from 'react';
import { Grid, TextField, MenuItem, Button, FormControl, InputLabel, Select, FormHelperText } from '@mui/material';
import useForm from './useForm';
import { connect } from 'react-redux';
import * as actions from "../actions/book"
import Pagination from './Pagination';




const initalFieldValues = {
    isbn: '',
    name:'',
    author:'',
    price:''
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
    const [currentIsbn, setCurrentIsbn] = useState(0);


    const {
        values,
        setValues,
        handleInputChange
    } =  useForm(initalFieldValues, setCurrentIsbn)

    const [errors, setErrors] = useState({});

    //validations
    
    const validate = (fieldValues = values) => {
      let errors = {};
      let isValid = true;
    
      if (!fieldValues.isbn) {
        errors.isbn = 'ISBN field cannot be empty';
        isValid = false;
      } else if (!/^\d{13}$/.test(fieldValues.isbn)) {
        errors.isbn = 'ISBN must be a 13 digit number';
        isValid = false;
      }
    
      if (!fieldValues.name) {
        errors.name = 'Name field cannot be empty';
        isValid = false;
      }
    
      if (!fieldValues.author) {
        errors.author = 'Author field cannot be empty';
        isValid = false;
      }
    
      if (!fieldValues.price && fieldValues.price !== 0) {
        errors.price = 'Price field cannot be empty';
        isValid = false;
      } else if (fieldValues.price < 0) {
        errors.price = 'Price field cannot be negative';
        isValid = false;
      } else if (!/^\d+(\.\d{1,2})?$/.test(fieldValues.price)) {
        errors.price = 'Price must be numeric with up to two decimal places';
        isValid = false;
      }

      setErrors(errors);
      return isValid;
    }
    
    

      
      //submit new book validations

      const handleSubmit = (e) => {
        e.preventDefault(); // Prevent page reload on form submission
        if (validate()) {
          if (props.currentIsbn !== 0 && props.currentIsbn !== null) { 
            props.updateBook(props.currentIsbn, values, () => { 
              window.alert('Updated successfully');
            });
          } else if (props.currentIsbn === 0 || props.currentIsbn === null) {
            props.createBook(values, () => { 
              window.alert('Created successfully');
            });
          }
        } else {
          window.alert('Submit failed');
        }
      };
      
      
      
      
      

      //reset fields func
      const handleReset = () => {
        setValues(initalFieldValues);
        setSelectedAuthor("");
      };

      useEffect(() =>{
        if(props.currentIsbn !== 0)
        setValues({
          ...props.bookList.find(x => x.isbn === props.currentIsbn)
        })
      },[props.currentIsbn])


  //material-ui select -> dropDown
  const inputLabel = React.useRef(null);
  const [labelWidth, setLabelWidth] = React.useState(0);
  React.useEffect(() => {
      setLabelWidth(inputLabel.current.offsetWidth);
  }, []);

    return ( 
        <form autoComplete='off' noValidate onSubmit={handleSubmit}>
            <Grid container padding={2}>
                <Grid item xs={6}>
                    <TextField margin="normal" required
                    style={{ width: '80%' }}
                    name="isbn"
                    variant='outlined'
                    label="ISBN"
                    value = {values.isbn}
                    onChange = {handleInputChange}
                    error={errors.isbn}
                    helperText={errors.isbn}
                    />

                    <TextField margin="normal" required
                    style={{ width: '80%' }}
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
                <FormControl variant="outlined"
                margin="normal"
                style={{ width: '80%' }}
                {...(errors.author && { error: true })}>

                    <InputLabel ref={inputLabel}>Author</InputLabel>
                        <Select
                            name="author"
                            value={values.author}
                            onChange={handleInputChange}
                        >
                        <MenuItem value={""}>Please select an Author</MenuItem>
                        {authors.map((option) => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
                        </MenuItem>
                        ))}
                        </Select>
                        {errors.author && <FormHelperText>{errors.author}</FormHelperText>}
                    </FormControl>
                    </div>

                <TextField margin="normal" required
                    style={{ width: '80%' }}
                    name="price"
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