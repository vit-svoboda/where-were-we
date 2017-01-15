// @flow
import * as types from './../actions/actionTypes';

export default function authReducer(auth: any = {}, action: any) {
    switch (action.type) {
    case types.LOGIN_SUCCESS:
        return { isValid: true };

    default:
        return auth;
    }
}