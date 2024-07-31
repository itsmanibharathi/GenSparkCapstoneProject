import $ from 'jquery';
import homePage from './home.html';
import { properties } from '../../../data/dummy.js';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';

import propertyCardTemplate from '../../components/propertyCardTemplate.js';

const loadHomeCallback = (query, api, token) => {
    log.info('Home page loaded');
    log.info("properties", properties);
    function loadProperties(properties) {
        var propertycard = $('#property-card');
        propertycard.empty();
        properties.forEach(property => {
            propertycard.append(propertyCardTemplate(property));
        });

    }

    loadProperties(properties);

}

module.exports = {
    homePage,
    loadHomeCallback
}