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
import { userVerifyPage, loaduserVerifyCallback } from '../modules/UserVerify/userVerify.js';
import { subscriptionPlanPage, loadSubscriptionPlanCallback } from '../modules/SubscriptionPlan/subscriptionPlan.js';

const token = new jwtService('User');
const localStorage = new localStorageService('User');
const api = new apiService(process.env.API_URL, token.get());

const routes = [
    { path: '/', component: homePage, callback: loadHomeCallback, title: 'Home' },
    { path: '/auth', component: authPage, callback: loadAuthCallback, title: 'Authenticate' },
    { path: '/property/post', component: postPropertyPage, callback: loadPostPropertyCallback, title: 'Post Property' },
    { path: '/property/edit', component: editPropertyPage, callback: loadEditPropertyCallback, title: 'Edit Property' },
    { path: '/profile', component: profilePage, callback: loadProfileCallback, title: 'Profile' },
    { path: '/user/verify', component: userVerifyPage, callback: loaduserVerifyCallback, title: 'Verify User' },
    { path: '/subscription/plan', component: subscriptionPlanPage, callback: loadSubscriptionPlanCallback, title: 'Subscription Plan' }
];

let isNavigating = false;

const loadRoutes = (path, query) => {
    if (isNavigating) return;
    isNavigating = true;
    setTimeout(() => { isNavigating = false; }, 100); 

    if (path) {
        log.info('Navigating to path:', path);
        const newUrl = new URL(window.location.href);
        newUrl.pathname = path;
        newUrl.search = '';
        window.history.pushState({}, '', newUrl.href);
    } else {
        log.info('Navigating twwo path:', path);
        path = window.location.pathname.toLowerCase();
        path = path === '/' ? '/' : path.replace(/\/$/, '');

        query = window.location.search;
        query = query === '' ? '' : query.replace(/^\?/, '');
        query = query === '' ? '' : query.split('&').reduce((acc, item) => {
            const [key, value] = item.split('=');
            acc[key] = value;
            return acc;
        }, {});
    }
    log.info('path:', path);
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
        loadComponent('#body-placeholder', route.component, route.callback, query, api, token, localStorage);
        document.title = route.title + ' | 360area.tech';
    } else {
        loadComponent("#404", Page404);
        $('#header-placeholder').html("");
        $('#body-placeholder').html("");
    }
};

const initializeRouter = () => {
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
};

initializeRouter();

export default loadRoutes;
