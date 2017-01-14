// @flow
import * as types from './actionTypes';
import fetch from 'isomorphic-fetch';

export function loginAsync(form: FormData) {
    return function(dispatch: Function) {
        dispatch({ type: types.LOGIN_ASYNC });
        // TODO: Retrieve the URL from some sort of a configuration.
        return fetch('http://localhost:50475/api/token', {
            method: 'POST',
            body: form
        })
            .then(response => response.json())
            .then(json => dispatch(loginSuccess(json)))
            .catch(error => dispatch({ type: types.LOGIN_ERROR, error: error }));
    };
}

export function loginSuccess(json: { access_token: string, expires_in: number }) {   
    return { type: types.LOGIN_SUCCESS, json };
}