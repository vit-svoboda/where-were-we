// @flow
import {List} from 'immutable';
import * as types from './../actions/actionTypes';
import Series from '../models/SeriesRecord';

function increment(series: Series) {
    if (!series){
        return;
    }

    if (series.episode < series.episodes) {
        return series.set('episode', series.episode + 1);
    } else if (series.season < series.seasons) {
        return series.merge({
            season: series.season + 1,
            episode: 1
        });
    } else {
        return series;
    }
}

export default function seriesReducer(state: List<Series> = List(), action : any) {
    switch (action.type) {
    case types.LOAD_SERIES_SUCCESS:
        return action.series;

    case types.ADD_SERIES:
        return state.push(action.series);

    case types.INCREMENT_PROGRESS:
        return state.update(
            state.indexOf(action.series),
            i => increment(i));

    default:
        return state;
    }
}