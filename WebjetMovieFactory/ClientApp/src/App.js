import React, { Component } from 'react';
import _ from 'lodash';
import './App.css';
import { MovieList } from './components/MovieList/movie-list.component';

class App extends Component {
    constructor() {
        super();

        this.state = {
            dataLoading: false,
            moviesList: []
        };
    }

    async componentDidMount() {
        let dataList = [];

        const movieSource = [
            'cinemaworld', 'filmworld'
        ];

        Promise.allSettled(movieSource.map(source =>
            fetch(`/api/${source}/movies`)
                .then(checkStatus)
                .then(parseJSON)
                .then(data => {

                    Promise.allSettled(data.map(item =>
                        fetch(`/api/${source}/movie/${item.ID}`)
                            .then(checkStatus)
                            .then(parseJSON)
                            .then(data => {
                                dataList.push(data);

                                this.setState({
                                    moviesList: dataList,
                                    dataLoading: true
                                })
                            })
                            .catch(error => console.log(error))
                    ));
                })
                .catch(error => console.log(error))
        ))
       

        function checkStatus(response) {
            if (response.ok) {
                return Promise.resolve(response);
            } else {
                
                return Promise.reject(new Error(response.statusText));
            }
        }

        function parseJSON(response) {
            return response.json();
        }
    }


    render() {

        const { dataLoading, moviesList } = this.state; 

        const cheapMovies = _(moviesList.flat())
            .groupBy('Title')
            .map((group) => _.minBy(group, 'Price'))
            .sortBy('Year')
            .value();

        cheapMovies.filter(x => x !== undefined);
        console.log(cheapMovies);

        if (dataLoading === false) {
            return (
                <div className='App'>
                    <h1>Loading......</h1>
                </div>
            );
        }

        if ((cheapMovies) && (cheapMovies.length > 0) && (cheapMovies[0])) {
            return (
                <div className='App'>
                    <h1>Cheap Movies</h1>
                    <MovieList movies={cheapMovies} />
                </div>
            );
        } else {
            return (
                <div className='App'>
                    <h1>No Movies Available</h1>
                </div>
            );
        }                
    }

}

export default App;
