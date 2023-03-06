import React from 'react';
import logo from './logo.svg';
import './App.css';
import { store } from './actions/store';
import { Provider } from 'react-redux';
import Books from './components/Books';
import { Container } from '@mui/material';

function App() {
  return (
    <Provider store={store}> 
    <Container maxWidth="lg">
          <Books />
        </Container>
    </Provider>
  );
}

export default App;
