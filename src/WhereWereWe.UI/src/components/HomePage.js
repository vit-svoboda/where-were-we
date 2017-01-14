// @flow
import React, {PropTypes} from 'react';
import {List} from 'immutable';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import SeriesList from './SeriesList';
import SeriesForm from './SeriesForm';
import LoginForm from './LoginForm';
import Series from '../models/SeriesRecord';
import * as seriesActions from '../actions/seriesActions';

class HomePage extends React.Component {
    state: {
        newSeries: Series
    };
    constructor (props: any, context: any) {
        super(props, context);

        this.state = {
            newSeries: null
        };

        (this:any).createNewSeries = this.createNewSeries.bind(this);
        (this:any).updateNewSeries = this.updateNewSeries.bind(this);
        (this:any).saveNewSeries = this.saveNewSeries.bind(this);
        (this:any).hideNewSeries = this.hideNewSeries.bind(this);
    }

    updateNewSeries (event: any) {
        const field = event.target.name;
        let newSeries = this.state.newSeries;
        newSeries = newSeries.set(field, event.target.value);
        return this.setState({newSeries: newSeries});
    }

    createNewSeries () {
        return this.setState({newSeries: Series()});
    }

    saveNewSeries (event: any) {
        event.preventDefault();

        this.props.actions.addSeries(this.state.newSeries);
    }

    hideNewSeries () {
        this.setState({newSeries: null});
    }

    render() {
        const {series} = this.props;
        const {newSeries} = this.state;
        const {incrementProgress} = this.props.actions;
        const {auth} = this.props;

        return (
            <div>
                <h1>Where Were We?</h1>
                {!auth.isValid && <LoginForm />}
                {newSeries
                    ? <SeriesForm series={newSeries} onSave={this.saveNewSeries} onChange={this.updateNewSeries} onCancel={this.hideNewSeries} />
                    : <input type="button" value="+" onClick={this.createNewSeries} />}
                <SeriesList series={series} incrementProgress={incrementProgress} />
            </div>
        );
    }
}

HomePage.propTypes = {
    series: PropTypes.instanceOf(List).isRequired,
    auth: PropTypes.object.isRequired,
    actions: PropTypes.object.isRequired
};

function mapStateToProps(state) {
    return {
        series: state.series,
        auth: state.auth
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(seriesActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(HomePage);