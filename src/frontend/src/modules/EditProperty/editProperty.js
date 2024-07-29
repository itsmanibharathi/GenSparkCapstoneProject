import $, { get } from 'jquery';
import editPropertyPage from './editProperty.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';
import loadRoutes from '../../Services/routerService.js';

const loadEditPropertyCallback = async (query, api, token) => {
    log.info('Loading Edit Property Callback');
    let currentStep = 1;
    const totalSteps = 4;
    function showStep(step) {
        log.info('form data', gatherFormData());
        $('.step').removeClass('active');
        $(`.step:nth-of-type(${step})`).addClass('active');
        $('#currentStep').text(step);
        $('#progressBar').css('width', `${(step / totalSteps) * 100}%`);
    }

    window.nextStep = function () {
        if (currentStep < totalSteps) {
            currentStep++;
            showStep(currentStep);
            toggleButtons();
        }
    }

    window.prevStep = function () {
        if (currentStep > 1) {
            currentStep--;
            showStep(currentStep);
            toggleButtons();
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

    $('#category').change(function () {

        const selectedCategory = $(this).val();
        let detailsHtml = '';

        if (selectedCategory === 'Home') {
            detailsHtml = `
                        <div class="mb-4">
                            <label for="area" class="block text-gray-700">Area</label>
                            <input type="number" name="area" id="area" class="w-full px-3 py-2 border rounded">
                        </div>
                        <div class="mb-4">
                            <label for="numberOfBedrooms" class="block text-gray-700">Number of Bedrooms</label>
                            <input type="number" name="numberOfBedrooms" id="numberOfBedrooms" class="w-full px-3 py-2 border rounded">
                        </div>
                        <div class="mb-4">
                            <label for="numberOfBathrooms" class="block text-gray-700">Number of Bathrooms</label>
                            <input type="number" name="numberOfBathrooms" id="numberOfBathrooms" class="w-full px-3 py-2 border rounded">
                        </div>
                        <div class="mb-4">
                            <label for="yearBuilt" class="block text-gray-700">Year Built</label>
                            <input type="number" name="yearBuilt" id="yearBuilt" class="w-full px-3 py-2 border rounded">
                        </div>
                        <div class="mb-4">
                            <label for="furnishingStatus" class="block text-gray-700">Furnishing Status</label>
                            <select name="furnishingStatus" id="furnishingStatus" class="w-full px-3 py-2 border rounded">
                                <option value="Furnished">Furnished</option>
                                <option value="SemiFurnished">Semi-Furnished</option>
                                <option value="Unfurnished">Unfurnished</option>
                            </select>
                        </div>
                        <div class="mb-4">
                            <label for="floorNumber" class="block text-gray-700">Floor Number</label>
                            <input type="number" name="floorNumber" id="floorNumber" class="w-full px-3 py-2 border rounded">
                        </div>
                    `;
        } else if (selectedCategory === 'Land') {
            detailsHtml = `
                        <div class="mb-4">
                            <label for="landArea" class="block text-gray-700">Land Area</label>
                            <input type="number" name="landArea" id="landArea" class="w-full px-3 py-2 border rounded">
                        </div>
                        <div class="mb-4">
                            <label for="zoningInformation" class="block text-gray-700">Zoning Information</label>
                            <input type="text" name="zoningInformation" id="zoningInformation" class="w-full px-3 py-2 border rounded">
                        </div>
                        <div class="mb-4">
                            <label for="landType" class="block text-gray-700">Land Type</label>
                            <select name="landType" id="landType" class="w-full px-3 py-2 border rounded">
                                <option value="Residential">Residential</option>
                                <option value="Commercial">Commercial</option>
                                <option value="Agricultural">Agricultural</option>
                            </select>
                        </div>
                    `;
        }

        $('#categoryDetails').html(detailsHtml);
    });

    $(document).on('click', '#add-amenity', function () {
        const amenityHtml = `
            <div class="flex space-x-4 mb-2">
                <input type="text" name="amenityName" placeholder="Amenity Name" class="px-3 py-2 border rounded flex-grow">
                <input type="text" name="amenityDescription" placeholder="Amenity Description" class="px-3 py-2 border rounded flex-grow">
                <label class="inline-flex items-center space-x-2">
                    <input type="checkbox" name="isPaid" class="form-checkbox">
                    <span>Is Paid</span>
                </label>
                <button type="button" class="remove-amenity bg-red-500 text-white px-3 py-2 rounded">Remove</button>
            </div>
        `;
        $('#amenities').append(amenityHtml);
    });

    $(document).on('click', '.remove-amenity', function () {
        $(this).parent().remove();
    });

    $(document).on('click', '#add-media', function () {
        const mediaHtml = `
        <div class="flex space-x-4 mb-2">
            <input type="text" name="mediaTitle" placeholder="Media Title" class="px-3 py-2 border rounded flex-grow">
            <select name="mediaType" class="px-3 py-2 border rounded flex-grow">
                <option value="Image">Image</option>
                <option value="Video">Video</option>
            </select>
            <form class="image-form" action="${process.env.API_URL}/media" method="post" enctype="multipart/form-data">
                <input type="file" name="file" class="px-3 py-2 border rounded flex-grow">
            </form>
            <input type="hidden" name="mediaUrl" class="px-3 py-2 border rounded flex-grow">
            <span class="file-url"></span>
            <button type="button" class="remove-media bg-red-500 text-white px-3 py-2 rounded">Remove</button>
        </div>
    `;
        $('#mediaFiles').append(mediaHtml);
    });

    $(document).on('click', '.remove-media', function () {
        $(this).parent().remove();
    });

    $(".image-form").on('change', 'input[type="file"]', async function (event) {
        event.preventDefault();
        const formdata = new FormData($(this)[0]);
        console.log(formdata);

        var response = await fetch(process.env.API_URL + '/media', {
            method: 'POST',
            body: formdata
        });
        console.log(response);
    });



    function loadFormData(data) {
        if (!data) {
            loadRoutes('/');
        }
        $('#propertyId').val(data.propertyId);
        $('#title').val(data.title);
        $('#description').val(data.description);
        $('#price').val(data.price);
        $('#landmark').val(data.landmark);
        $('#street').val(data.street);
        $('#city').val(data.city);
        $('#state').val(data.state);
        $('#country').val(data.country);
        $('#zipCode').val(data.zipCode);
        $('#latitude').val(data.latitude);
        $('#longitude').val(data.longitude);
        $('#category').val(data.category);
        $('#type').val(data.type);
        $('#userId').val(data.userId);
        $('#status').val(data.status);

        if (data.category === 'Home') {
            $('#category').change();
            $('#area').val(data.home?.area ?? '');
            $('#numberOfBedrooms').val(data.home?.numberOfBedrooms ?? '');
            $('#numberOfBathrooms').val(data.home?.numberOfBathrooms ?? '');
            $('#yearBuilt').val(data.home?.yearBuilt ?? '');
            $('#furnishingStatus').val(data.home?.furnishingStatus ?? '');
            $('#floorNumber').val(data.home?.floorNumber ?? '');
        } else if (data.category === 'Land') {
            $('#category').change();
            $('#landArea').val(data.land?.landArea ?? '');
            $('#zoningInformation').val(data.land?.zoningInformation ?? '');
            $('#landType').val(data.land?.landType ?? '');
        }

        $('#amenities').empty();
        try {
            data.amenities.forEach(amenity => {
                const amenityHtml = `
                    <div class="flex space-x-4 mb-2">
                        <input type="text" name="amenityName" value="${amenity.name}" placeholder="Amenity Name" class="px-3 py-2 border rounded flex-grow">
                        <input type="text" name="amenityDescription" value="${amenity.description}" placeholder="Amenity Description" class="px-3 py-2 border rounded flex-grow">
                        <label class="inline-flex items-center space-x-2">
                            <input type="checkbox" name="isPaid" class="form-checkbox" ${amenity.isPaid ? 'checked' : ''}>
                            <span>Is Paid</span>
                        </label>
                        <button type="button" class="remove-amenity bg-red-500 text-white px-3 py-2 rounded">Remove</button>
                    </div>
                `;
                $('#amenities').append(amenityHtml);
            });
        }
        catch (e) {
            log.error(e);
        }

        $('#mediaFiles').empty();
        try {



            data.mediaFiles.forEach(media => {
                const mediaHtml = `
                <div class="flex space-x-4 mb-2">
                <input type="text" name="mediaTitle" value="${media.title}" placeholder="Media Title" class="px-3 py-2 border rounded flex-grow">
                <select name="mediaType" class="px-3 py-2 border rounded flex-grow">
                <option value="Image" ${media.type === 'Image' ? 'selected' : ''}>Image</option>
                <option value="Video" ${media.type === 'Video' ? 'selected' : ''}>Video</option>
                </select>
                <form class="image-form" action="${process.env.API_URL}/media" method="post" enctype="multipart/form-data">
                <input type="file" name="file" class="px-3 py-2 border rounded flex-grow">
                </form>
                <input type="hidden" name="mediaUrl" value="${media.url}" class="px-3 py-2 border rounded flex-grow">
                <span class="file-url">${media.url}</span>
                <button type="button" class="remove-media bg-red-500 text-white px-3 py-2 rounded">Remove</button>
                </div>
                `;
                $('#mediaFiles').append(mediaHtml);
            });
        }
        catch (e) {
            log.error(e);
        }
    }



    function gatherFormData() {
        const propertyData = {
            propertyId: $('#propertyId').val(),
            title: $('#title').val(),
            description: $('#description').val(),
            price: $('#price').val(),
            landmark: $('#landmark').val(),
            street: $('#street').val(),
            city: $('#city').val(),
            state: $('#state').val(),
            country: $('#country').val(),
            zipCode: $('#zipCode').val(),
            latitude: $('#latitude').val(),
            longitude: $('#longitude').val(),
            category: $('#category').val(),
            type: $('#type').val(),
            userId: $('#userId').val(),
            status: $('#status').val(),
            amenities: [],
            mediaFiles: []
        };

        // Gather amenities data
        $('#amenities > div').each(function () {
            const amenity = {
                name: $(this).find('input[name="amenityName"]').val(),
                description: $(this).find('input[name="amenityDescription"]').val(),
                isPaid: $(this).find('input[name="isPaid"]').is(':checked')

            };
            propertyData.amenities.push(amenity);
        });

        // Gather media files data
        $('#mediaFiles > div').each(function () {
            const media = {
                title: $(this).find('input[name="mediaTitle"]').val(),
                type: $(this).find('select[name="mediaType"]').val(),
                url: $(this).find('input[name="mediaUrl"]').val()
                // Assuming you handle file uploads separately
            };
            propertyData.mediaFiles.push(media);
        });

        return propertyData;
    }

    const getFormData = async (query) => {
        if (!query.propertyId) {
            return null;
        }
        return await api.get(`property/${query.propertyId}`)
            .then(res => {
                console.log("dataaa", res.data);
                return res.data;
            }).catch(err => {
                log.error(err);
                showAlert(err.message, 'error');
                return null;
            });
    }
    loadFormData(await getFormData(query));


    $('#propertyForm').submit(async function (event) {
        event.preventDefault();
        var data = gatherFormData();
        console.log(data);
        await api.put('property', data)
            .then((res) => {
                log.info(res);
                showAlert(res.message, 'success');
            })
            .catch((err) => {
                log.error(err);
                showAlert(err.message, 'error');
            });

        // Simulate form submission
        alert('Form submitted successfully!');
    });
}

module.exports = {
    editPropertyPage,
    loadEditPropertyCallback
}