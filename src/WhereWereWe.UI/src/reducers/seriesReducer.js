// @flow
import * as types from './../actions/actionTypes';

export default function seriesReducer(state : Array<any> = [], action : any) {
    switch (action.type) {
    case types.LOAD_SERIES:
        return action.series;

    default:
        return state;
    }
}