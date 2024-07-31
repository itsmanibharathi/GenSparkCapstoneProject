import $ from 'jquery';
import userVerifyPage from './userVerify.html';
import showAlert from '../../Services/alertService';
import loadRoutes from '../../Services/routerService.js';

const loaduserVerifyCallback = (query, api, token) => {
    const userVerify = async () => {
        await api.put(`user/verify/${query.id}/${query.token}`)
            .then(response => {
                $('#user-verify').html(response.message);
                showAlert(response.message, 'success');
                loadRoutes('/auth');
            })
            .catch(error => {
                $('#user-verify').html(error.message);
                showAlert(error.message, 'error');
            });
    }

    if (query.id && query.token) {
        userVerify();
    }
}

module.exports = {
    userVerifyPage,
    loaduserVerifyCallback
}