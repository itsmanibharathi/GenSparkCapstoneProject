import $ from 'jquery';
import verificationPage from './verification.html';

const loadVerificationCallback = (api, token) => {
    console.log('Verification Callback');
}

module.exports = {
    verificationPage,
    loadVerificationCallback
}