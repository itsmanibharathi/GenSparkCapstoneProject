import $ from 'jquery';
import Page404 from '../components/404.html';
import log from '../utility/loglevel.js';
import Footer from '../components/Footer.html';
import headerTemplate from '../components/headerTemplate.js';

import apiService from './apiService.js';
import jwtService from './jwtService.js';
import localStorageService from './localStorageService.js';
import showAlert from './alertService.js';

import loadComponent from './loadComponent.js';

import { HomePage, loadHomeCallback } from '../modules/Home/home.js';
import { AuthPage, loadAuthCallback } from '../modules/Auth/auth.js';

const token = new jwtService('User');
const localStorage = new localStorageService('User');
const api = new apiService(process.env.API_URL, token.get());

const routes = [
    { path: '/', component: HomePage, callback: loadHomeCallback },
    { path: '/auth', component: AuthPage, callback: loadAuthCallback },
    { path: '/logout' }
]

const loadRoutes = () => {
    let path = window.location.pathname.toLowerCase();
    path = path === '/' ? '/' : path.replace(/\/$/, '');
    log.info('path:', path);
    const route = routes.find(r => r.path === path);
    if (path === '/logout') {
        token.remove();
        localStorage.remove();
        showAlert('Logged out successfully', 'success');
        window.location.href = '/';
        loadRoutes();
        return;
    }
    else if (route) {
        $('#404').html("");
        $('#header-placeholder').html(headerTemplate(token));
        loadComponent('#footer-placeholder', Footer);
        loadComponent('#body-placeholder', route.component, route.callback, api, token);
    }
    else {
        loadComponent("#404", Page404);
        $('#header-placeholder').html("");
        $('#body-placeholder').html("");
    }
};

$(document).ready(() => {
    loadRoutes();

    $(window).on('popstate', loadRoutes);
});

$(document).on('click', 'a', function (e) {
    const href = $(this).attr('href');
    if (href && !href.startsWith('http')) {
        e.preventDefault();

        history.pushState(null, '', `${basePath}${href}`);
        loadRoutes();
    }
    else {
        window.href = href;
    }
});

module.exports = {
    loadRoutes,
};

export const basePath = process.env.isProduction ? `/#` : '';

console.log(process.env.BASE_PATH);