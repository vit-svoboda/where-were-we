// @flow
import {createStore} from 'redux';
import rootReducer from '../reducers';

export default function configureStore(preloadedState: any) {
    return createStore(rootReducer, preloadedState);
}