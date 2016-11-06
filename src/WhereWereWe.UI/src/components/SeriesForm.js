import React, {PropTypes} from 'react';
import Series from '../models/SeriesRecord';

const SeriesForm = ({series, onSave, onChange}) => {
    return (
        <form>
            <label htmlFor="name">Name</label>
            <input
                type="text"
                name="name"
                value={series.name}
                placeholder="Fill in the series name."
                onChange={onChange} />

            <label htmlFor="seasons">Seasons</label>
            <input
                type="number"
                name="seasons"
                value={series.seasons}
                onChange={onChange} />

            <label htmlFor="episodes">Episodes per season</label>
            <input
                type="number"
                name="episodes"
                value={series.episodes}
                onChange={onChange} />

            <input
                type="submit"
                value="Save"
                onClick={onSave} />
        </form>
    );
};

SeriesForm.propTypes = {
    series: PropTypes.instanceOf(Series).isRequired,
    onSave: PropTypes.func.isRequired,
    onChange: PropTypes.func.isRequired
};

export default SeriesForm;