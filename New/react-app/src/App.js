import React from 'react';
import './App.css';
import { store } from './actions/store';
import { Provider } from 'react-redux';
import Books from './components/Books';
import { Container } from '@mui/material';
import NavBar from './components/NavBar';

function App() {
  return (
    <Provider store={store}> 
    <Container maxWidth="lg">
          <NavBar/>
          <Books />
        </Container>
    </Provider>
  );
}

export default App;
