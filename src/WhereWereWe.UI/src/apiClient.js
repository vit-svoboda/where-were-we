// @flow
import fetch from 'isomorphic-fetch';

// TODO: Retrieve the URL from some sort of a configuration.
const API_ROOT_URL = 'http://localhost:50475/api';
const TOKEN_STORAGE_KEY = 'apiToken';

export function apiLogin(credentials:FormData): Promise<*> {
    return fetch(API_ROOT_URL + '/token', { method: 'POST', body: credentials })
        .then(response => response.json())
        .then(json => localStorage.setItem(TOKEN_STORAGE_KEY, json.access_token));
}

export function apiRequest(httpMethod: string, relativeUrl: string, body: ?FormData = null): Promise<*> {

    const token = localStorage.getItem(TOKEN_STORAGE_KEY);

    if (!token) {
        return Promise.reject(new Error('Missing API token.'));
    }

    return fetch(API_ROOT_URL + relativeUrl, {
        method: httpMethod,
        mode: 'cors',
        headers: new Headers({ 'Authorization': 'Bearer ' + token }),
        body: body
    });
}