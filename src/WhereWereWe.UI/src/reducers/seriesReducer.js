// @flow
import {List} from 'immutable';
import * as types from './../actions/actionTypes';
import Series from '../models/SeriesRecord';


export default function seriesReducer(state: List<Series> = List(), action: any) {
    switch (action.type) {
    case types.LOAD_SERIES_SUCCESS:
        return action.series;

    case types.ADD_SERIES:
        return state.push(action.series);

    case types.INCREMENT_PROGRESS:
        {
            const index = state.findIndex(i => i.id === action.series.id);
            return state.update(index, () => action.series);
        }

    default:
        return state;
    }
}