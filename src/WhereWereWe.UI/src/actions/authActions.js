// @flow
import * as types from './actionTypes';
import {apiLogin} from '../apiClient';

export function loginAsync(form: FormData) {
    return function(dispatch: Function) {
        dispatch({ type: types.LOGIN_ASYNC });
        
        apiLogin(form)
            .then(() => dispatch({ type: types.LOGIN_SUCCESS }))
            .catch(error => dispatch({ type: types.LOGIN_ERROR, error: error }));
    };
}