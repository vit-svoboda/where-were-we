// @flow
import {combineReducers} from 'redux';
import series from './seriesReducer';
import auth from './authReducer';

// They key names indicate what slice of the state the reducer operates!
const rootReducer = combineReducers({
    series,
    auth
});

export default rootReducer;