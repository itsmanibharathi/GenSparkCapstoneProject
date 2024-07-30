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
        toggleButtons();
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
    }

    function validateForm() {
        if (currentStep < 3 && !$(`#${pages[currentStep - 1]} form`)[0].checkValidity()) {
            showAlert('Please fill all the required fields', 'error');
            return false;
        }

        const hasVisibleSaveBtn = $(document).find('.saveBtn').filter(function () {
            return !$(this).hasClass('hidden');
        }).length > 0;

        if (hasVisibleSaveBtn) {
            showAlert('Please save the amenity', 'error');
            return false;
        }
        return true;
    }


    window.nextStep = function () {
        if (!validateForm())
            return;
        if (currentStep < totalSteps) {
            currentStep++;
            showStep(pages[currentStep - 1]);
        }
    }

    window.prevStep = function () {
        if (!validateForm())
            return;
        if (currentStep > 1) {
            currentStep--;
            showStep(pages[currentStep - 1]);
        }
    }

    function toggleButtons() {
        log.info('Toggle Buttons');
        log.info('Current Step', currentStep);

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

    $('.container').on('change', function (e) {
        isdirty = true;
        log.info('Form is dirty', isdirty);
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
    $('#specificinfo').on('blur', 'input', function (e) {
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

    $('#amenities').on('blur', 'input', function (e) {
        const input = e.target;
        const value = input.value;
        if (value === '' && input.required) {
            input.classList.add('border-red-500');
            input.nextElementSibling.innerText = 'This field is required';
        }
        else {
            input.classList.remove('border-red-500');
            input.nextElementSibling.innerText = '';
            if (value !== '') {
                input.classList.add('border-green-500');
            }
        }
    });

    // Add amenity template
    $('#add-amenity').click(function () {
        var index = $('#amenities tr').length;
        $('#amenities').append(propertyAmenityTemplate(null, index, query.propertyId));
        $('#amenities tr').last().find('.amenityForm').toggleClass('hidden');
        $('#amenities tr').last().find('.amenityTable').toggleClass('hidden');

    });

    // Delete amenity row
    $('#amenities').on('click', '.deleteBtn', function () {
        api.delete(`property/amenity/${$(this).closest('tr').find('input[name="amenityId"]').val()}`)
            .then((res) => {
                log.info(res);
                $(this).closest('tr').remove();
                showAlert(res.message, 'success');
            })
            .catch((err) => {
                log.error(err);
                showAlert(err.message, 'error');
            });
    });

    // Edit amenity row
    $('#amenities').on('click', '.editBtn', function () {
        var tr = $(this).closest('tr');
        tr.find('.amenityTable').toggleClass('hidden');
        tr.find('.amenityForm').toggleClass('hidden');
    });

    // Save amenity row
    $('#amenities').on('click', '.saveBtn', function () {
        var tr = $(this).closest('tr');

        var name = tr.find('input[name="name"]').val();
        var description = tr.find('input[name="description"]').val();
        if (name === '' || description === '') {
            showAlert('Please fill all the required fields', 'error');
            return;
        }
        var isPaid = tr.find('input[name="isPaid"]').is(':checked');
        tr.find('td[name="name"]').text(name);
        tr.find('td').eq(2).text(description);
        tr.find('td').eq(4).text(isPaid ? "Paid" : "Free");

        tr.find('.amenityTable').toggleClass('hidden');
        tr.find('.amenityForm').toggleClass('hidden');

        // Send save request to the server here
    });


    $('#add-media').on('click', function () {
        var index = $('#mediaFiles tr').length;
        $('#mediaFiles').append(propertyMediFileTemplate(null, index, query.propertyId));
        $('#mediaFiles tr').last().find('.mediaForm').toggleClass('hidden');
        $('#mediaFiles tr').last().find('.mediaTable').toggleClass('hidden');

    });

    $('#mediaFiles').on('click', '.deleteBtn', function () {
        var mediaId = $(this).closest('tr').find('input[name="mediaFileId"]').val();

        if (mediaId > 0) {
            api.delete(`Property/MediaFile/${mediaId}`)
                .then((res) => {
                    log.info(res);
                    $(this).closest('tr').remove();
                    showAlert(res.message, 'success');
                })
                .catch((err) => {
                    log.error(err);
                    showAlert(err.message, 'error');
                });
        }
        var url = $(this).closest('tr').find('input[name="url"]').val();
        if (!url) {
            $(this).closest('tr').remove();
            return;
        }
        var contanerName = url.split('/')[3];
        var blobName = url.split('/')[4];
        log.info('Delete file', contanerName, blobName);
        api.delete(`media/${contanerName}/${blobName}`)
            .then((res) => {
                log.info(res);
                $(this).closest('tr').remove();
                showAlert(res.message, 'success');
            })
            .catch((err) => {
                log.error(err);
                showAlert(err.message, 'error');
            });

    });

    // Edit amenity row
    $('#mediaFiles').on('click', '.editBtn', function () {
        var tr = $(this).closest('tr');

        tr.find('.mediaTable').toggleClass('hidden');
        tr.find('.mediaForm').toggleClass('hidden');
    });



    $('#mediaFiles').on('click', '.saveBtn', async function () {
        alert('save button clicked');
        var tr = $(this).closest('tr');
        var mediaId = $(this).closest('tr').find('input[name="mediaFileId"]').val();
        log.info('Media Id', mediaId);
        var title = tr.find('input[name="title"]').val();
        if (title === '') {
            showAlert('Please fill all the required fields', 'error');
            return;
        }
        var fileInput = tr.find('input[type="file"]')[0];
        var url = tr.find('input[name="url"]').val();
        log.info('File Input', fileInput);
        log.info('File', fileInput.files);
        if (fileInput && fileInput.files.length > 0) {
            if (mediaId > 0) {
                log.info('Update file');
                var containerName = url.split('/')[3];
                var blobName = url.split('/')[4];
                var formData = new FormData(tr.find('.uploadForm')[0]);
                await fetch(`${process.env.API_URL}/media/${containerName}/${blobName}`, {
                    method: 'PUT',
                    body: formData
                })
                    .then(response => response.json())
                    .then(res => {
                        log.info(res);
                        tr.find('input[name="url"]').val(res.data);
                        tr.find('td[name="url"]').html(`<img src="${res.data}">`);
                        tr.find('td[name="title"]').text(title);
                        showAlert(res.message, 'success');
                    })
                    .catch((error) => {
                        log.error(error.message, error);
                        showAlert(error.message, 'error');
                        $(this).remove();
                    });
            }
            else {
                log.info('Create file');
                var formData = new FormData(tr.find('.uploadForm')[0]);
                await fetch(`${process.env.API_URL}/media/mediafile`, {
                    method: 'POST',
                    body: formData
                })
                    .then(response => response.json())
                    .then(res => {
                        log.info(res);
                        tr.find('input[name="url"]').val(res.data);
                        tr.find('td[name="url"]').html(`<img src="${res.data}">`);
                        tr.find('td[name="title"]').text(title);
                        showAlert(res.message, 'success');
                    })
                    .catch((error) => {
                        log.error(error.message, error);
                        showAlert(error.message, 'error');
                        $(this).remove();
                    });

            }
        }
        tr.find('.mediaTable').toggleClass('hidden');
        tr.find('.mediaForm').toggleClass('hidden');

        // Send save request to the server here
    });

    $('#mediaFiles').on('click', '.refreshBtn', function () {
        var tr = $(this).closest('tr');
        var url = tr.find('input[name="url"]').val();
        tr.find('td[name="url"]').html(`<img src="${url}">`);
    });

    // create a on change event for file input


    const getProperty = async (query) => {
        if (!query.propertyId) {
            return null;
        }
        return await api.get(`property/${query.propertyId}`)
            .then(res => {
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

        data.amenities.forEach((amenity, index) => {
            $('#amenities').append(propertyAmenityTemplate(amenity, index, query.propertyId));
        });

        data.mediaFiles.forEach((mediaFile, index) => {
            $('#mediaFiles').append(propertyMediFileTemplate(mediaFile, index, query.propertyId));
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

        $('#amenities tr').each(function () {
            var data = {
                propertyId: query.propertyId,
                amenityId: $(this).find('input[name="amenityId"]').val(),
                name: $(this).find('input[name="name"]').val(),
                description: $(this).find('input[name="description"]').val(),
                isPaid: $(this).find('input[name="isPaid"]').val() === 'on' ? true : false,
            }
            formData.amenities.push(data);
        });


        $('#mediaFiles tr').each(function () {
            var data = {
                mediaFileId: $(this).find('input[name="mediaFileId"]').val(),
                url: $(this).find('input[name="url"]').val(),
                propertyId: $(this).find('input[name="propertyId"]').val(),
                type: $(this).find('input[name="type"]').val(),
                title: $(this).find('input[name="title"]').val()
            }
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

    $(window).on('beforeunload', function (event) {
        if (isdirty) {
            const message = 'You have unsaved changes. Are you sure you want to leave?';
            event.returnValue = message;
            return message;
        }
    });


    toggleButtons();
    var page = query?.page?.toLowerCase() ?? pages[currentStep - 1];
    if (!pages.includes(page)) {
        page = pages[0];
    }
    currentStep = pages.indexOf(page) + 1;
    showStep(page);
    const propertyData = await getProperty(query);
    populateForm(propertyData);

};

module.exports = {
    editPropertyPage,
    loadEditPropertyCallback
}