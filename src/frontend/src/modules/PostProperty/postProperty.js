import $ from 'jquery';
import postPropertyPage from './postProperty.html';
import postPropertyImage from '../../../public/assets/Image/PostPropertyPage.webp';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';


const loadPostPropertyCallback = (query, api, token, localStorage) => {
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
            var oldData = localStorage.get('propertyForm');
            if (oldData === null) {
                oldData = {};
            }
            oldData[input.name] = input.value;
            localStorage.set('propertyForm', oldData);
        }

    });

    $('#propertyForm').on('change', 'select', (e) => {
        e.preventDefault();
        let select = e.target;
        var oldData = localStorage.get('propertyForm');
        if (oldData === null) {
            oldData = {};
        }
        oldData[select.name] = select.value;
        localStorage.set('propertyForm', oldData);
    });

    $('#propertyForm').on('submit', (e) => {
        e.preventDefault();
        if (token.get() === null) {
            showAlert('Please login to post a property', 'error');
            return;
        }
        if ($('#propertyForm')[0].checkValidity() === false) {
            showAlert('Please fill the required fields', 'error');
        }
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
                    loadRoutes(`/property/edit?PropertyId=${response.data.PropertyId}`);
                })
                .catch(error => {
                    showAlert(error.message, 'error');
                    log.error(error);
                });
        }
    });

    $('#refreshForm').on('click', (e) => {
        e.preventDefault();
        $('#propertyForm').trigger('reset');
        $('#propertyForm input').removeClass('border-green-500');
        $('#propertyForm select').removeClass('border-green-500');
        localStorage.remove('propertyForm');
    });
    function loadOldData() {
        var oldData = localStorage.get('propertyForm');
        if (oldData !== null) {
            Object.keys(oldData).forEach(key => {
                if (key === 'Type' || key === 'Category') {
                    $(`select[name=${key}]`).val(oldData[key]);
                } else {
                    $(`input[name=${key}]`).val(oldData[key]);
                }
            });
        }
    }
    loadOldData();

}

module.exports = {
    postPropertyPage,
    loadPostPropertyCallback
}