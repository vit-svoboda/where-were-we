// @flow
import React, {PropTypes} from 'react';
import Series from '../models/SeriesRecord';

const SeriesProgress = ({series, onIncrement}: {series: Series, onIncrement: Function}) => (
    <div>
        <h2>{series.name}</h2>
        Season {series.season} Episode {series.episode}
        <input type="button" value="+" onClick={onIncrement} />
    </div>
);

SeriesProgress.propTypes = {
    series: PropTypes.instanceOf(Series).isRequired,
    onIncrement: PropTypes.func.isRequired
};

export default SeriesProgress;