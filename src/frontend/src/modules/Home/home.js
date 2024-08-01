import $ from 'jquery';
import homePage from './home.html';
import { properties } from '../../../data/dummy.js';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';

import DataNotFountImg from '../../../public/assets/Image/data-not-found.jpg';

import propertyCardTemplate from '../../components/propertyCardTemplate.js';

const loadHomeCallback = (query, api, token) => {
    log.info('Home page loaded');
    log.info("properties", properties);

    $('.typeCtrl').on('click', function () {
        $('.typeCtrl').removeClass('type-select');
        $(this).addClass('type-select');
    });

    $('#Search-Btn').on('click', async function () {
        var data = {
            "query": $('#query').val(),
            "type": $('.typeCtrl.type-select').attr('value'),
            "Category": $('#Category').val(),
        }
        log.info(data);
        var res = properties.filter(property => property['type'] = data.type || property['category'] == data.Category || property['title'].includes(data.query) || property['description'].includes(data.query) || property['city'].includes(data.query) || property['state'].includes(data.query) || property['country'].includes(data.query));

        // await api.get('/properties/search', data, token)
        loadProperties(res);
    });
    function loadProperties(properties) {
        if (properties == undefined || properties.length == 0) {
            $('#property-card').append(`<img src="${DataNotFountImg}" alt="No data found" class="w-1/2 mx-auto" />`);
            return;
        }
        var propertycard = $('#property-card');
        propertycard.empty();
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
            loadProperties(properties.filter(property => property.userId == token.select('Id')));
        }
        else {
            loadProperties();
        }
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
        await api.get(`property/Interaction/viewOwnerInfo/${$(this).attr('property-id')}`)
            .then((res) => {
                var ownerInfoContainer = $(this).closest('.mt-4').siblings('.ownerInfo');
                ownerInfoContainer.toggleClass('hidden');
                ownerInfoContainer.find('.ownerName').text(res.userName);
                ownerInfoContainer.find('.ownerEmail').text(res.userEmail);
                ownerInfoContainer.find('.ownerPhoneNumber').text(res.userPhoneNumber);
            })
            .catch((err) => {
                showAlert(err.message, 'error');
            });
    });

    $(document).on('click', '.ContactMe', async function () {
        await api.put(`property/Interaction/contact/${$(this).attr('property-id')}`)
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