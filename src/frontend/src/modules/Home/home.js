import $ from 'jquery';
import homePage from './home.html';

const loadHomeCallback = (query, api, token) => {
    console.log('Home Callback');
}

module.exports = {
    homePage,
    loadHomeCallback
}