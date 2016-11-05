import * as types from './actionTypes';

export function loadSeries() {
    return { type: types.LOAD_SERIES, series: [] };
}