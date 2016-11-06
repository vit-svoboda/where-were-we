// @flow
import * as types from './actionTypes';
import {List} from 'immutable';
import Series from '../models/SeriesRecord';

export function loadSeries() {
    // TODO: Retrieve the series data from some api that would handle the persistance.
    return { type: types.LOAD_SERIES, series: List([
        new Series({ name: 'Archer', season: 3, seasons: 7, episode: 5, episodes: 13 }),
        new Series({ name: 'Game of Thrones', season: 6, seasons: 6, episodes: 10 })
    ]) };
}

export function incrementEpisode(series: Series) {
    return { type: types.INCREMENT_EPISODE, series };
}