// @flow
import React, {PropTypes} from 'react';
import {List} from 'immutable';
import SeriesProgress from './SeriesProgress';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as seriesActions from '../actions/seriesActions';

const SeriesList = ({series, actions}) => {
    return (
        <div>
            <h1>Where Were We?</h1>
            {series.map(series =>
                <SeriesProgress
                    key={series.name}
                    series={series}
                    onIncrement={actions.incrementEpisode.bind(this, series)} />)}
        </div>
    );
};

SeriesList.propTypes = {
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

export default connect(mapStateToProps, mapDispatchToProps)(SeriesList);