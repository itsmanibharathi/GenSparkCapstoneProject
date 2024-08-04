import $ from 'jquery';
import homePage from './home.html';
// import { properties } from '../../../data/dummy.js';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';

import DataNotFountImg from '../../../public/assets/Image/data-not-found.jpg';

import propertyCardTemplate from '../../components/propertyCardTemplate.js';

const loadHomeCallback = (query, api, token) => {
    log.info('Home page loaded');
    $('.typeCtrl').on('click', function () {
        $('.typeCtrl').removeClass('type-select');
        $(this).addClass('type-select');
    });

    $('#Search-Btn').on('click', async function () {
        var data = {
            "query": $('#query').val() || '',
            "type": $('.typeCtrl.type-select').attr('value') || '',
            "category": $('#Category').val() || '',
            "getMyProperty": false
        }
        if (data.query == 'Mypost') {
            data.query = '';
            data.getMyProperty = true;
        }

        function serializeQueryParams(obj) {
            // add key only if value is not empty
            return Object.keys(obj)
                .filter(key => obj[key] !== '')
                .map(key => `${key}=${obj[key]}`)
                .join('&');
        }
        log.info("data", data);
        var urlQuery = serializeQueryParams(data);
        log.info("url", `Property?${urlQuery}`);
        await api.get(`Property?${urlQuery} `)
            .then((res) => {
                loadProperties(res.data);
            })
            .catch((err) => {
                showAlert(err.message, 'error');
            });
        // await api.get('/properties/search', data, token)
        // var res = properties.filter(property => property['type'] = data.type || property['category'] == data.Category || property['title'].includes(data.query) || property['description'].includes(data.query) || property['city'].includes(data.query) || property['state'].includes(data.query) || property['country'].includes(data.query));
    });
    function loadProperties(properties) {
        var propertycard = $('#property-card');
        propertycard.empty();
        if (properties == undefined || properties.length == 0) {
            propertycard.append(`<img src = "${DataNotFountImg}" alt = "No data found" class= "w-1/2 mx-auto rounded-lg" /> `);
            return;
        }
        properties.forEach(property => {
            propertycard.append(propertyCardTemplate(property));
        });

    }
    log.info("token", token);
    log.info("isowner", token.select('isOwner'));
    function onLoadHome() {
        if (token && token.select('isOwner') == 'True') {
            $('.typeCtrl.hidden').removeClass('hidden');
            $('.typeCtrl.type-select').removeClass('type-select');
            $('.typeCtrl[value="Mypost"]').addClass('type-select');
        }
        $('#Search-Btn').trigger('click');
    }
    onLoadHome();

    $(document).on('click', '.carousel-button.next', function () {
        const $carousel = $(this).closest('.carousel');
        const $carouselInner = $carousel.find('.carousel-inner');
        const $items = $carouselInner.children();
        let currentIndex = $items.index($carouselInner.find('.carousel-active'));

        $items.eq(currentIndex).removeClass('carousel-active');
        currentIndex = (currentIndex + 1) % $items.length;
        $items.eq(currentIndex).addClass('carousel-active');
    });

    $(document).on('click', '.carousel-button.prev', function () {
        const $carousel = $(this).closest('.carousel');
        const $carouselInner = $carousel.find('.carousel-inner');
        const $items = $carouselInner.children();
        let currentIndex = $items.index($carouselInner.find('.carousel-active'));

        $items.eq(currentIndex).removeClass('carousel-active');
        currentIndex = (currentIndex - 1 + $items.length) % $items.length;
        $items.eq(currentIndex).addClass('carousel-active');
    });

    $(document).on('click', '.viewOwnerInfo', async function () {
        await api.get(`property/Interaction/viewOwnerInfo/${$(this).attr('property-id')} `)
            .then((res) => {
                var ownerInfoContainer = $(this).closest('.mt-4').siblings('.ownerInfo');
                ownerInfoContainer.toggleClass('hidden');
                ownerInfoContainer.find('.ownerName').text(res.data.userName);
                ownerInfoContainer.find('.ownerEmail').text(res.data.userEmail);
                ownerInfoContainer.find('.ownerPhoneNumber').text(res.data.userPhoneNumber);
            })
            .catch((err) => {
                showAlert(err.message, 'error');
            });
    });

    $(document).on('click', '.ContactMe', async function () {
        await api.put(`property/Interaction/contact/${$(this).attr('property-id')} `)
            .then((res) => {
                showAlert(res.message, 'success');
            })
            .catch((err) => {
                showAlert(err.message, 'error');
            });
    });


}

module.exports = {
    homePage,
    loadHomeCallback
}