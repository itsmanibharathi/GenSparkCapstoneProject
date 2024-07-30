import $ from 'jquery';
import homePage from './home.html';
import { property } from '../../../data/dummy.js';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';

import propertyCardTemplate from '../../components/propertyCardTemplate.js';

const loadHomeCallback = (query, api, token) => {
    log.info('Property data:', property);
    const LoadPropert = (property) => {
        var propertyCard = $('#property-cards');
        propertyCard.empty();
        property.forEach((property) => {
            propertyCard.append(propertyCardTemplate(property));
        });
    }

    document.querySelectorAll('.carousel-button').forEach(button => {
        button.addEventListener('click', event => {
            const carousel = event.target.closest('.carousel');
            const activeImg = carousel.querySelector('img.active');
            let nextImg;

            if (event.target.classList.contains('next')) {
                nextImg = activeImg.nextElementSibling;
                if (!nextImg || nextImg.tagName !== 'IMG') {
                    nextImg = carousel.querySelector('img:first-child');
                }
            } else {
                nextImg = activeImg.previousElementSibling;
                if (!nextImg || nextImg.tagName !== 'IMG') {
                    nextImg = carousel.querySelector('img:last-child');
                }
            }

            activeImg.classList.remove('active');
            nextImg.classList.add('active');
        });
    });

    LoadPropert(property);


}

module.exports = {
    homePage,
    loadHomeCallback
}