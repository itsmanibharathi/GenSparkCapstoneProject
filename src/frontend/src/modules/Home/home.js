import $ from 'jquery';
import homePage from './home.html';

const loadHomeCallback = (api, token) => {
    console.log('Home Callback');
}

module.exports = {
    homePage,
    loadHomeCallback
}