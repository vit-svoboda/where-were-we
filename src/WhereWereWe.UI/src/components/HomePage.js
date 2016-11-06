import React, {PropTypes} from 'react';
import {List} from 'immutable';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import SeriesList from './SeriesList';
import SeriesForm from './SeriesForm';
import Series from '../models/SeriesRecord';
import * as seriesActions from '../actions/seriesActions';

export class HomePage extends React.Component {
    constructor (props, context) {
        super(props, context);

        this.state = {
            newSeries: null
        };

        this.createNewSeries = this.createNewSeries.bind(this);
        this.updateNewSeries = this.updateNewSeries.bind(this);
        this.saveNewSeries = this.saveNewSeries.bind(this);
    }

    updateNewSeries(event) {
        const field = event.target.name;
        let newSeries = this.state.newSeries;
        newSeries = newSeries.set(field, event.target.value);
        return this.setState({newSeries: newSeries});
    }

    createNewSeries() {
        return this.setState({newSeries: Series()});
    }

    saveNewSeries(event) {
        event.preventDefault();

        this.props.actions.addSeries(this.state.newSeries);
    }    

    render() {
        const {series} = this.props;
        const {newSeries} = this.state;
        const {incrementProgress} = this.props.actions;

        return (
            <div>
                <h1>Where Were We?</h1>
                {newSeries
                    ? <SeriesForm series={newSeries} onSave={this.saveNewSeries} onChange={this.updateNewSeries} />
                    : <input type="button" value="+" onClick={this.createNewSeries} />}
                <SeriesList series={series} incrementProgress={incrementProgress} />
            </div>
        );
    }
}

HomePage.propTypes = {
    series: PropTypes.instanceOf(List).isRequired,
    actions: PropTypes.object.isRequired
};

function mapStateToProps(state) {
    return {
        series: state.series
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(seriesActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(HomePage);