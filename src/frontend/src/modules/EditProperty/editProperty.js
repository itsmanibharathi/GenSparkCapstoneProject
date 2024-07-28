import $ from 'jquery';
import editPropertyPage from './editProperty.html';
import showAlert from '../../Services/alertService.js';
import log from '../../utility/loglevel.js';

const loadEditPropertyCallback = (query, api, token) => {
    log.info('Loading Edit Property Callback');
    let currentStep = 1;
    const totalSteps = 4;
    function showStep(step) {
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

    // $('#nextBtn').click(function () {
    //     if (currentStep < totalSteps) {
    //         currentStep++;
    //         showStep(currentStep);
    //     }
    //     toggleButtons();
    // });

    // $('#prevBtn').click(function () {
    //     if (currentStep > 1) {
    //         currentStep--;
    //         showStep(currentStep);
    //     }
    //     toggleButtons();
    // });

    //     <div class="flex flex-row justify-between flex-grow md:justify-end gap-2">
    //     <button type="button" class="prevBtn hidden bg-gray-500 text-white px-4 py-2 rounded "
    //         onclick="prevStep()">
    //         Previous
    //     </button>
    //     <button type="button" class="nextBtn bg-blue-500 text-white px-4 py-2 rounded self-end ml-auto"
    //         onclick="nextStep()">
    //         Next
    //     </button>
    //     <button type="submit" class="submitBtn hidden bg-green-500 text-white px-6 py-2 rounded mt-4"
    //         id="submitBtn">Submit</button>
    // </div>
    function toggleButtons() {
        if (currentStep === 1) {
            $('.prevBtn').addClass('hidden');
        } else {
            $('.prevBtn').removeClass('hidden');
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

    function loadFormData(data) {
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
            $('#area').val(data.home.area);
            $('#numberOfBedrooms').val(data.home.numberOfBedrooms);
            $('#numberOfBathrooms').val(data.home.numberOfBathrooms);
            $('#yearBuilt').val(data.home.yearBuilt);
            $('#furnishingStatus').val(data.home.furnishingStatus);
            $('#floorNumber').val(data.home.floorNumber);
        } else if (data.category === 'Land') {
            $('#category').change();
            $('#landArea').val(data.land.landArea);
            $('#zoningInformation').val(data.land.zoningInformation);
            $('#landType').val(data.land.landType);
        }

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

        data.mediaFiles.forEach(media => {
            const mediaHtml = `
                        <div class="flex space-x-4 mb-2">
                            <input type="text" name="mediaTitle" value="${media.title}" placeholder="Media Title" class="px-3 py-2 border rounded flex-grow">
                            <select name="mediaType" class="px-3 py-2 border rounded flex-grow">
                                <option value="Image" ${media.type === 'Image' ? 'selected' : ''}>Image</option>
                                <option value="Video" ${media.type === 'Video' ? 'selected' : ''}>Video</option>
                            </select>
                            <input type="file" name="file" class="px-3 py-2 border rounded flex-grow">
                            <button type="button" class="remove-media bg-red-500 text-white px-3 py-2 rounded">Remove</button>
                        </div>
                    `;
            $('#mediaFiles').append(mediaHtml);
        });
    }

    const propertyData = {
        "propertyId": 0,
        "title": "Sample Property",
        "description": "This is a sample property description.",
        "price": 500000,
        "landmark": "Near Central Park",
        "street": "123 Main St",
        "city": "Sample City",
        "state": "Sample State",
        "country": "Sample Country",
        "zipCode": "12345",
        "latitude": "40.7128",
        "longitude": "-74.0060",
        "category": "Home",
        "type": "Rent",
        "userId": 1,
        "status": "Active",
        "createAt": "2024-07-28T15:29:48.193Z",
        "updateAt": "2024-07-28T15:29:48.193Z",
        "amenities": [
            {
                "amenityId": 0,
                "name": "Gym",
                "description": "Well-equipped gym",
                "isPaid": true,
                "propertyId": 0,
                "createAt": "2024-07-28T15:29:48.193Z",
                "updateAt": "2024-07-28T15:29:48.193Z"
            }
        ],
        "mediaFiles": [
            {
                "mediaFileId": 0,
                "title": "Property Front View",
                "type": "Image",
                "url": "https://example.com/front-view.jpg",
                "propertyId": 0,
                "createAt": "2024-07-28T15:29:48.193Z",
                "updateAt": "2024-07-28T15:29:48.193Z"
            }
        ],
        "home": {
            "propertyId": 0,
            "area": 1200,
            "numberOfBedrooms": 3,
            "numberOfBathrooms": 2,
            "yearBuilt": 2015,
            "furnishingStatus": "Furnished",
            "floorNumber": 1,
            "createAt": "2024-07-28T15:29:48.193Z",
            "updateAt": "2024-07-28T15:29:48.193Z"
        },
        "land": null
    };

    loadFormData(propertyData);

    $('#propertyForm').submit(function (event) {
        event.preventDefault();
        const formData = $(this).serializeArray();
        console.log(formData);

        // Simulate form submission
        alert('Form submitted successfully!');
    });
}

module.exports = {
    editPropertyPage,
    loadEditPropertyCallback
}