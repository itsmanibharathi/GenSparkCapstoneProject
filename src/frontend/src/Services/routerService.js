import $, { queue } from 'jquery';
import Page404 from '../components/404.html';
import log from '../utility/loglevel.js';
import Footer from '../components/footer.html';
import headerTemplate from '../components/headerTemplate.js';

import apiService from './apiService.js';
import jwtService from './jwtService.js';
import localStorageService from './localStorageService.js';
import showAlert from './alertService.js';

import loadComponent from './loadComponent.js';

import { homePage, loadHomeCallback } from '../modules/Home/home.js';
import { authPage, loadAuthCallback } from '../modules/Auth/auth.js';
import { postPropertyPage, loadPostPropertyCallback } from '../modules/PostProperty/postProperty.js';
import { editPropertyPage, loadEditPropertyCallback } from '../modules/EditProperty/editProperty.js';
import { profilePage, loadProfileCallback } from '../modules/Profile/profile.js';
import { verificationPage, loadVerificationCallback } from '../modules/Verification/verification.js';


const token = new jwtService('User');
const localStorage = new localStorageService('User');
const api = new apiService(process.env.API_URL, token.get());

const routes = [
    { path: '/', component: homePage, callback: loadHomeCallback },
    { path: '/auth', component: authPage, callback: loadAuthCallback },
    { path: '/property/post', component: postPropertyPage, callback: loadPostPropertyCallback },
    { path: '/property/edit', component: editPropertyPage, callback: loadEditPropertyCallback },
    { path: '/profile', component: profilePage, callback: loadProfileCallback },
    { path: '/verification', component: verificationPage, callback: loadVerificationCallback }

];



const loadRoutes = (path, query) => {
    path = path || window.location.pathname.toLowerCase();
    path = path === '/' ? '/' : path.replace(/\/$/, '');
    log.info('path:', path);

    query = query || window.location.search;
    query = query === '' ? '' : query.replace(/^\?/, '');
    query = query === '' ? '' : query.split('&').reduce((acc, item) => {
        const [key, value] = item.split('=');
        acc[key] = value;
        return acc;
    }, {});

    log.info('query:', query);


    if (path === '/logout') {
        token.remove();
        localStorage.remove();
        showAlert('Logged out successfully', 'success');
        history.replaceState(null, '', '/');
        path = '/';
    }
    const route = routes.find(r => r.path === path);

    if (route) {
        $('#404').html("");
        $('#header-placeholder').html(headerTemplate(token));
        loadComponent('#footer-placeholder', Footer);
        loadComponent('#body-placeholder', route.component, route.callback, query, api, token);
    } else {
        loadComponent("#404", Page404);
        $('#header-placeholder').html("");
        $('#body-placeholder').html("");
    }
};


$(window).on('popstate', () => {
    loadRoutes();
});

$(document).on('click', 'a', function (e) {
    const href = $(this).attr('href');
    if (href && !href.startsWith('http')) {
        e.preventDefault();

        if (window.location.pathname !== href) {
            history.pushState(null, '', href);
            loadRoutes(href);
        }
    } else {
        window.location.href = href;
    }
});

export default loadRoutes;
