import $ from 'jquery';
import postPropertyPage from './postProperty.html';
import postPropertyImage from '../../../public/assets/Image/PostPropertyPage.webp';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';


const loadPostPropertyCallback = (query, api, token) => {
    $('#PostPropertyImage').attr('src', postPropertyImage);
    // validate the form
    var isFormDirty = false;
    $('#propertyForm').on('input', (e) => {
        isFormDirty = true;
    });

    $('#propertyForm').on('blur', 'input', (e) => {
        e.preventDefault();
        let input = e.target;
        if (input.value == '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name == 'price' && input.value < 0) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Price must be greater than 0';
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (input.value !== '') {
                input.classList.add('border-green-500');
            }
        }

    });

    $('#propertyForm').on('change', 'select', (e) => {
        e.preventDefault();
        let select = e.target;
        if (select.value == '') {
            select.classList.add('border-red-500');
            select.nextElementSibling.innerText = 'This field is required';
        }
        else {
            select.classList.remove('border-red-500');
            select.nextElementSibling.innerText = '';
            select.classList.add('border-green-500');
        }
    });

    $('#propertyForm').on('submit', (e) => {
        e.preventDefault();
        const formdata = $('#propertyForm').serializeArray();
        const data = {};
        formdata.forEach(item => {
            data[item.name] = item.value;
        });
        log.info("data: ", data);
        if (isFormDirty) {
            api.post('Property', data)
                .then(response => {
                    if (response.status === 201) {
                        alert('Property posted successfully');
                        $('#propertyForm').trigger('reset');
                        $('#propertyForm input').removeClass('border-green-500');
                        $('#propertyForm select').removeClass('border-green-500');
                        isFormDirty = false;
                    }
                    showAlert(response.message, 'success');
                    loadRoutes(`/property/edit/${response.data.id}`);
                })
                .catch(error => {
                    showAlert(error.message, 'error');
                    log.error(error);
                });
        }
    });
}

module.exports = {
    postPropertyPage,
    loadPostPropertyCallback
}