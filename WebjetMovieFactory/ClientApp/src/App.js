import React, { Component } from 'react';
import _ from 'lodash';
import './App.css';
import { MovieList } from './components/MovieList/movie-list.component';

class App extends Component {
    constructor() {
        super();

        this.state = {
            moviesList: []
        };
    }

    componentDidMount() {
        this.getAllMovies()
    }

    getAllMovies() {
        const _this = this;
   
        var urls = [
            '/api/cinemaworld/movies',
            '/api/filmworld/movies'
        ];

        var requests = urls.map(function (url) {
            return fetch(url)
                .then(function (response) {
                    // throw "uh oh!";  - test a failure
                    return response.json();
                })
                .then((data) => {
                    _this.setState(state => state.moviesList.push(data));
                })
        });

        Promise.allSettled(requests)
            .then((results) => {
                console.log(JSON.stringify(results, null, 2));
            }).catch(function (err) {
                console.log(err);
            })
    }


    render() {

        const { moviesList } = this.state; 

        const cheapMovies = _(moviesList.flat())
            .groupBy('Name')
            .map((group) => _.minBy(group, 'Price'))
            .sortBy('Year')
            .value();

        console.log(cheapMovies);

        return (
            <div className='App'>
                <h1>Cheap Movies</h1>  
                <MovieList movies={cheapMovies} />  
            </div>
        );
    }

}

export default App;
