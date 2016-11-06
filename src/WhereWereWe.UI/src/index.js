// @flow
import React from 'react';
import {render} from 'react-dom';
import HomePage from './components/HomePage';
import {Provider} from 'react-redux';
import configureStore from './store/configureStore';
import {loadSeries} from './actions/seriesActions';

const store = configureStore();
store.dispatch(loadSeries());

render(
    <Provider store={store}>
        <HomePage />
    </Provider>,
    document.getElementById('app')
);