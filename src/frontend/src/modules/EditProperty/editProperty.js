import $ from 'jquery';
import editPropertyPage from './editProperty.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';
import propertyCategoryTemplate from '../../components/propertyCategoryTemplate.js';
import propertyAmenityTemplate from '../../components/propertyamenityTemplate.js';
import propertyMediFileTemplate from '../../components/propertyMediFileTemplate.js';

const loadEditPropertyCallback = async (query, api, token, localStorage) => {
    log.info('Loading Edit Property Callback');
    const pages = ['basicinfo', 'specificinfo', 'amenitiesinfo', 'mediafileinfo'];
    let currentStep = 1;
    const totalSteps = 4;
    var isdirty = false;

    function showStep(page) {
        log.info('Showing Step', page);
        log.info('Formdata', gatherFormData());
        pages.forEach((page) => {
            $(`#${page}`).hide();
        });
        if (!pages.includes(page))
            page = pages[currentStep - 1];
        $(`#${page}`).show();

        var step = pages.indexOf(page) + 1;
        $('#currentStep').text(step);
        $('#progressBar').css('width', `${(step / totalSteps) * 100}%`);
        window.history.pushState({}, '', `?page=${pages[step - 1]}&propertyId=${query.propertyId}`);
        toggleButtons();
    }

    window.nextStep = function () {
        if ($(`#${pages[currentStep - 1]} form`)[0].checkValidity() === false) {
            showAlert('Please fill all the required fields', 'error');
            return;
        }
        if (currentStep < totalSteps) {
            currentStep++;
            showStep(pages[currentStep - 1]);
        }
    }

    window.prevStep = function () {
        if (currentStep > 1) {
            currentStep--;
            showStep(pages[currentStep - 1]);
        }
    }

    function toggleButtons() {
        if (currentStep === 1) {
            $('.prevBtn').addClass('hidden');
            $('.nextBtn').addClass('ml-auto');
        } else {
            $('.prevBtn').removeClass('hidden');
            $('.nextBtn').removeClass('ml-auto');
        }

        if (currentStep === totalSteps) {
            $('.nextBtn').addClass('hidden');
            $('.submitBtn').removeClass('hidden');
        } else {
            $('.nextBtn').removeClass('hidden');
            $('.submitBtn').addClass('hidden');
        }
    }

    $('form').on('change', function (e) {
        isdirty = true;
    });



    $('#basicinfoForm').on('blur', 'input', function (e) {
        const input = e.target;
        const value = input.value;
        if (value === '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name === 'price' && !/^[0-9]+$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid price';
        }
        else if (input.name === 'zipCode' && !/^[0-9]{6}$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid Zip Code';
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (value !== '') {
                input.classList.add('border-green-500');
            }
        }
    });
    console.log('specificinfo form :', $('#specificinfo form'));
    $('#specificinfo form').on('blur', 'input', function (e) {
        alert('home');
        const input = e.target;
        const value = input.value;
        if (value === '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else if (input.name === 'area' && !/^[0-9]+$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid price';
        }
        else if (input.name === 'numberOfBedrooms' && !/^[0-9]+$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid number of bedrooms';
        }
        else if (input.name === 'numberOfBathrooms' && !/^[0-9]+$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid number of bathrooms';
        }
        else if (input.name === 'yearBuilt' && !/^[0-9]+$/.test(value)) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid year built';
        }
        else if (input.name === 'floorNumber' && value > 0) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'Invalid floor number';
        } else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (value !== '') {
                input.classList.add('border-green-500');
            }
        }
    });



    $(document).on('click', '#add-amenity', function () {
        $('#amenities').append(propertyAmenityTemplate());
    });

    $(document).on('click', '.remove-amenity', function () {
        $(this).parent().remove();
    });

    $(document).on('click', '#add-media', function () {
        $('#mediaFiles').append(propertyMediFileTemplate());
    });

    $(document).on('click', '.remove-media', function () {
        $(this).parent().remove();
    });

    // create a on change event for file input
    $(document).on('change', '.fileInput', async function () {
        var formData = new FormData($(this).parent('.uploadForm')[0]);
        log.info(formData);
        fetch(`${process.env.API_URL}/media`, {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                log.info(data);
                $(this).siblings('.file-url').text(data.data);
                $(this).siblings('input[name="mediaUrl"]').val(data.data);
            })
            .catch((error) => {
                log.error(error.message, error);
                $(this).parent().remove();
            });

    });

    const getProperty = async (query) => {
        if (!query.propertyId) {
            return null;
        }
        return await api.get(`property/${query.propertyId}`)
            .then(res => {
                console.log("dataaa", res.data);
                localStorage.set('property', res.data);
                return res.data;
            }).catch(err => {
                log.error(err);
                showAlert(err.message, 'error');
                return null;
            });
    }

    const populateForm = (data) => {

        $('#basicinfoForm').find('input, textarea, select').each(function () {
            $(this).val(data[$(this).attr('name')]);
        });
        if (data.category === 'Home') {
            $('#categoryDetails').html(propertyCategoryTemplate.PropertyHome(data.home));
        }
        else if (data.category === 'Land') {
            $('#categoryDetails').html(propertyCategoryTemplate.PropertyLand(data.land));
        }


        data.amenities.forEach(amenity => {
            $('#amenities').append(propertyAmenityTemplate(amenity));
        });

        data.mediaFiles.forEach(mediaFile => {
            $('#mediaFiles').append(propertyMediFileTemplate(mediaFile));
        });

        // showStep(currentStep);
    }

    const gatherFormData = () => {
        const formData = {}

        var tempFormData = $('#basicinfoForm').serializeArray();
        tempFormData.forEach((item) => {
            formData[item.name] = item.value;
        });
        formData.category = $('#category').val();
        formData.type = $('#type').val();

        formData.amenities = [];
        formData.mediaFiles = [];

        $('#amenities form').each(function () {
            var data = {}
            var tempFormData = $(this).serializeArray();
            tempFormData.forEach((item) => {
                data[item.name] = item.value;
                if (item.name === 'isPaid') {
                    data[item.name] = item.value === 'on' ? true : false;
                }
            });
            formData.amenities.push(data);
        });


        $('#mediaFiles form').each(function () {
            var data = {}
            var tempFormData = $(this).serializeArray();
            tempFormData.forEach((item) => {
                data[item.name] = item.value;
            });
            formData.mediaFiles.push(data);
        });

        // Gather category specific details
        if (formData.category === 'Home') {
            var data = {};
            var tempFormData = $('#HomeForm').serializeArray();
            tempFormData.forEach((item) => {
                data[item.name] = item.value;
            });
            formData.Home = data;
        } else if (formData.category === 'Land') {
            var data = {};
            var tempFormData = $('#LandForm').serializeArray();
            tempFormData.forEach((item) => {
                data[item.name] = item.value;
            });
            formData.Land = data
        }

        return formData;
    }

    $('.submitBtn').on('click', async function (event) {
        event.preventDefault();
        var data = gatherFormData();
        console.log(data);
        await api.put('property', data)
            .then((res) => {
                log.info(res);
                showAlert(res.message, 'success');
                loadRoutes('/');
            })
            .catch((err) => {
                log.error(err);
                showAlert(err.message, 'error');
            });
    });

    toggleButtons();
    var page = query.page.toLowerCase() || pages[currentStep - 1];
    showStep(page || pages[currentStep - 1]);
    const propertyData = await getProperty(query);
    populateForm(propertyData);

};

module.exports = {
    editPropertyPage,
    loadEditPropertyCallback
}