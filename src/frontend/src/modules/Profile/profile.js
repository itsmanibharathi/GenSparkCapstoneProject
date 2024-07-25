import $ from 'jquery';
import profilePage from './profile.html';

const loadProfileCallback = (api, token) => {
    console.log('Profile Callback');
}

module.exports = {
    profilePage,
    loadProfileCallback
}