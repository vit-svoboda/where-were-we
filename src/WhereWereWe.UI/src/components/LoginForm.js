// @flow
import React from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as authActions from '../actions/authActions';

class LoginForm extends React.Component {
    constructor (props: any, context: any) {
        super(props, context);

        this.requestLogin = this.requestLogin.bind(this);
    }

    requestLogin(event) {
        event.preventDefault();

        var form = new FormData(event.target.form);
        this.props.actions.loginAsync(form);
    }

    render() {
        return (
            <form>
                <label htmlFor="username">Name</label>
                <input name="username" type="text" />
                <label htmlFor="password">Password</label>
                <input name="password" type="password" />
                <input type="submit" onClick={this.requestLogin} />
            </form>
        );
    }
}

function mapStateToProps(state) {
    return {
        series: state.series,
        auth: state.auth
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(authActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginForm);