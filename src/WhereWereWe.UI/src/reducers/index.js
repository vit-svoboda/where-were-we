// @flow
import {combineReducers} from 'redux';
import series from './seriesReducer';

// They key names indicate what slice of the state the reducer operates!
const rootReducer = combineReducers({
    series
});

export default rootReducer;