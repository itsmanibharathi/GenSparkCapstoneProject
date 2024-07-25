import $ from 'jquery';
import authPage from './auth.html';
import { signInPage, signUpPage } from '../../Services/authLayoutService.js';
import showAlert from '../../Services/alertService.js';
import loadRoutes from '../../Services/routerService.js';
const loadAuthCallback = (api, token) => {
    console.log('Loading Auth Callback');
    $('#signUpPage').on('click', signUpPage);
    $('#signInPage').on('click', signInPage);

    $('#signIn').on('blur', 'input', function (e) {
        const input = e.target;
        const value = input.value;
        if (value === '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name === 'userEmail' && !/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid email address';
        }

        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (value !== '') {
                input.classList.add('border-green-500');
            }
        }
    });

    $('#signUp').on('blur', 'input', function (e) {
        const input = e.target;
        const value = input.value;
        if (value === '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name === 'userEmail' && !/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid email address';
        }
        else if (input.name === 'userPhone' && !/^[0-9]{10}$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid Phone number';
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (value !== '') {
                input.classList.add('border-green-500');
            }
        }
    });

    $('#signIn').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        console.log(data);
        api.post('user/auth/login', data)
            .then((res) => {
                token.set(res.data.token);
                showAlert(res.message, 'success');
                loadRoutes('/');
            })
            .catch((err) => {
                console.log(err);
                showAlert(err.message, 'error');
            });
    });


    $('#signUp').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serializeArray();
        const data = {};
        formData.forEach((item) => {
            data[item.name] = item.value;
        });
        console.log(data);
        api.post('user/auth/register', data)
            .then((res) => {
                showAlert(res.message, 'success');
                signInPage();
            })
            .catch((err) => {
                console.log(err);
                showAlert(res.message, 'error');
            });
    });
}


module.exports = {
    authPage,
    loadAuthCallback
}
