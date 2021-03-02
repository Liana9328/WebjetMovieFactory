import React from 'react';
import { Movie } from '../Movie/movie.component';

import './movie-list.styles.css';

export const MovieList = props => (
    <div className='movie-list'>
        {props.movies.map(movie => (
            <Movie key={movie.ID} movie={movie} />
        ))}
    </div>
);