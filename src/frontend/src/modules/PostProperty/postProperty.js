import $ from 'jquery';
import postPropertyPage from './postProperty.html';

const loadPostPropertyCallback = (api, token) => {
    console.log('load PostProperty Callback');
}

module.exports = {
    postPropertyPage,
    loadPostPropertyCallback
}