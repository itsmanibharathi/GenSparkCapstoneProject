import $ from 'jquery';
import profilePage from './profile.html';
import UploadProfile from '../../../public/assets/Image/uploadprofile.jpg';
import log from '../../utility/loglevel.js';
import showAlert from '../../Services/alertService.js';

const loadProfileCallback = (api, token) => {
    console.log('Profile Callback');
    $(document).ready(() => {
        var users = {
            userName: "Manibharathi",
            userEmail: "manibharathidct@gmail.com",
            userPhoneNumber: "6385687966",
            userProfileImageUrl: null,
            isActive: true,
            createAt: "0001-01-01T00:00:00",
            updateAt: null
        }

        const loadUserData = (user) => {
            console.log('User:', user.userName);
            $('#userName').val(user.userName);
            $('#userEmail').val(user.userEmail);
            $('#userPhoneNumber').val(user.userPhoneNumber);
            $('#profile-image').attr('src', user.userProfileImageUrl || UploadProfile);
        };

        $('#profileimage').change((event) => {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    $('#profile-image').attr('src', e.target.result);
                };
                reader.readAsDataURL(file);
            }
        });

        $('#profile-form').submit((e) => {
            e.preventDefault();

            // Create a FormData object to hold the form data
            const formData = new FormData($('#profile-form')[0]);

            // Include the profile image in the FormData object
            const fileInput = $('#profileimage')[0];
            if (fileInput.files.length > 0) {
                formData.append('profileimage', fileInput.files[0]);
            }

            var data = {};
            for (const [key, value] of formData.entries()) {
                log.debug(`${key}: ${value}`);
                data[key] = value;
            }

            // form format data
            const formDatas = new FormData("#profile-form");


            api.put('user', formData)
                .then((response) => {
                    log.debug('Profile update response:', response);
                    showAlert(res.message, 'success');
                    loadUserData(response.data);
                })
                .catch((error) => {
                    log.error('Profile update error:', error);
                    showAlert(error.message, 'error');
                });

            $('#edit-button').removeClass('hidden');
            $('#save-button').addClass('hidden');
            $('#userName, #userEmail, #userPhoneNumber').prop('readonly', true);
        });

        $('#edit-button').click(() => {
            $('#edit-button').addClass('hidden');
            $('#save-button').removeClass('hidden');
            $('#userName, #userEmail, #userPhoneNumber').prop('readonly', false);
        });

        var user = api.get('user')
            .then((response) => {
                log.debug('User data:', response.data);
                loadUserData(response.data);
            })
            .catch((error) => {
                log.error('User data error:', error);
                showAlert(error.message, 'error');
            }
            );
        loadUserData(user);
    });
}

module.exports = {
    profilePage,
    loadProfileCallback
}
