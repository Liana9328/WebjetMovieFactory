import React from 'react';
import './movie.styles.css';

export const Movie = props => (
    <div className='movie-container'>
        <img src={props.movie.Poster} alt="movie"></img>
        <h2>{props.movie.Title}</h2>
        <p>{props.movie.Year}</p>
        <p>@{props.movie.Source}</p>
        <p>${props.movie.Price}</p>
    </div>
);

