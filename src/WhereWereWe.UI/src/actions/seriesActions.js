// @flow
import * as types from './actionTypes';
import {List} from 'immutable';
import Series from '../models/SeriesRecord';
import {apiRequest} from '../apiClient'; 

export function loadSeriesAsync() {
    return function(dispatch: Function) {
        dispatch({ type: types.LOAD_SERIES_ASYNC });

        apiRequest('GET', '/progress')
            .then(response => response.json())
            .then(json => dispatch(loadSeriesSuccess(json)))
            .catch(error => dispatch({ type: types.LOAD_SERIES_ERROR, error }));
    };
}

export function loadSeriesSuccess(json: Array<Series>) {
    return {
        type: types.LOAD_SERIES_SUCCESS,
        series: List(json.map(item => new Series(item)))
    };
}

function increment(series: Series): Series {
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

export function incrementProgress(series: Series) {
    return function(dispatch: Function) {

        const incremented = increment(series);

        // Optimistic error handling
        dispatch({ type: types.INCREMENT_PROGRESS, series: incremented });

        const data = new FormData();
        data.append('episode', incremented.episode);
        data.append('season', incremented.season);

        apiRequest('PUT', '/progress/' + series.id, data)
            .catch(error => dispatch({ type: types.SERVER_ERROR, error }));
    };
}

export function addSeries(series: Series) {
    return function (dispatch: Function) {
        
        const data = new FormData();
        data.append('name', series.name);
        data.append('episodes', series.episodes);
        data.append('seasons', series.seasons);

        apiRequest('POST', '/series', data)
            .then(response => response.json())
            .then(json => {
                series = new Series(json);

                const data = new FormData();
                data.append('seriesId', series.id);

                return apiRequest('POST', '/progress', data);
            })
            .then(() => dispatch({ type: types.ADD_SERIES, series }))
            .catch(error => dispatch({ type: types.SERVER_ERROR, error }));
    };
}