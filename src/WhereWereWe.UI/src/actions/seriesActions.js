// @flow
import * as types from './actionTypes';
import {List} from 'immutable';
import fetch from 'isomorphic-fetch';
import Series from '../models/SeriesRecord';

export function loadSeriesAsync() {
    return function(dispatch: Function) {
        dispatch({ type: types.LOAD_SERIES_ASYNC });
        // TODO: Retrieve the URL from some sort of a configuration.
        return fetch('http://localhost:50475/api/series')
            .then(response => response.json())
            .then(json => dispatch(loadSeriesSuccess(json)))
            .catch(error => dispatch({ type: types.LOAD_SERIES_ERROR, error: error }));
    };
}

export function loadSeriesSuccess(json: Array<Series>) {
    return {
        type: types.LOAD_SERIES_SUCCESS,
        series: List(json.map(item => new Series(item)))
    };
}

export function incrementProgress(series: Series) {
    return { type: types.INCREMENT_PROGRESS, series };
}

export function addSeries(series: Series) {
    return { type: types.ADD_SERIES, series };
}