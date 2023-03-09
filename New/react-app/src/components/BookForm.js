import React, {useState} from 'react';
import { Grid, TextField, MenuItem, Button, makeStyles } from '@mui/material';
import useForm from './useForm';

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

    const {
        values,
        setValues,
        handleInputChange
    } =  useForm(initalFieldValues)


    return ( 
        <form autoComplete='off' noValidate>
            <Grid container padding={2}>
                <Grid item xs={6}>
                    <TextField margin="normal" required
                    name="isbn"
                    variant='outlined'
                    label="ISBN"
                    value = {values.isbn}
                    onChange = {handleInputChange}
                    />

                    <TextField margin="normal" required
                    name="name"
                    variant='outlined'
                    label="Name"
                    value = {values.name}
                    onChange = {handleInputChange}
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
                        value={selectedAuthor}
                        onChange={(e) => setSelectedAuthor(e.target.value)}
                    >
                        <MenuItem value={null}>Please select an Author</MenuItem>
                        {authors.map((option) => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
                        </MenuItem>
                        ))}
                    </TextField>
                    </div>

                <TextField margin="normal" required
                    name="price"
                    variant='outlined'
                    label="Price"
                    value = {values.price}
                    onChange = {handleInputChange}
                />

            
                <Grid container spacing={0.5}>
                    <Grid item>
                        <Button variant="contained" size="small">
                        Submit Book
                        </Button>
                    </Grid>
                    <Grid item>
                        <Button variant="contained" color="secondary" size="small">
                        Reset Fields
                        </Button>
                    </Grid>
                    </Grid>
                
            </Grid>
        </Grid>
     </form>
    );
}


export default BookForm;