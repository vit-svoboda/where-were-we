// @flow
import React, { PropTypes } from 'react';
import { List } from 'immutable';
import Series from '../models/SeriesRecord';
import SeriesProgress from './SeriesProgress';

const SeriesList = ({series, incrementProgress}: {series: List<Series>, incrementProgress: Function}) => {
    return (
        <div>
            {series.map(series =>
                <SeriesProgress
                    key={series.name}
                    series={series}
                    onIncrement={incrementProgress.bind(this, series)} />)}
        </div>
    );
};

SeriesList.propTypes = {
    series: PropTypes.instanceOf(List).isRequired,
    incrementProgress: PropTypes.func.isRequired
};

export default SeriesList;