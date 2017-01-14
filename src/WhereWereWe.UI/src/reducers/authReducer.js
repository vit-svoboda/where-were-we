// @flow
import * as types from './../actions/actionTypes';

export default function authReducer(auth: any = {}, action: any) {
    switch (action.type) {
    case types.LOGIN_SUCCESS:
        localStorage.setItem('apiToken', action.json.access_token);

        return { isValid: true };

    default:
        return auth;
    }
}