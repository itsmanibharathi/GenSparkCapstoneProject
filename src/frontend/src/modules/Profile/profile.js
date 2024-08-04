import $ from 'jquery';
import profilePage from './profile.html';
import UploadProfile from '../../../public/assets/Image/uploadprofile.jpg';
import log from '../../utility/loglevel.js';
import showAlert from '../../Services/alertService.js';

const loadProfileCallback = async (query, api, token, localStorage) => {
    var profileImgIsDirty = false;
    var profileDataIsDirty = false;

    const loadUserData = (user) => {
        $('#userName').val(user.userName);
        $('#userEmail').val(user.userEmail);
        $('#userPhoneNumber').val(user.userPhoneNumber);
        $('#profile-image').attr('src', user.userProfileImageUrl || UploadProfile);
    };

    $('#profileimage').change((event) => {
        profileImgIsDirty = true;
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                $('#profile-image').attr('src', e.target.result);
            };
            reader.readAsDataURL(file);
        }
    });

    $('#profileData-form').change(() => {
        profileDataIsDirty = true;
    });

    $(window).on('beforeunload', function (event) {
        if (profileImgIsDirty || profileDataIsDirty) {
            const message = 'You have unsaved changes. Are you sure you want to leave?';
            event.returnValue = message;
            return message;
        }
    });


    $('#save-button').on('click', async (e) => {
        e.preventDefault();
        log.debug('Form submitted');
        if (!profileImgIsDirty && !profileDataIsDirty) {
            showAlert('No changes to save', 'info');
            return;
        }
        if (profileImgIsDirty) {
            const formData = new FormData($('#imageForm')[0]);
            var url = $('#profile-image').attr('src');
            if (url.includes('http')) {
                const containerName = url.split('/')[3];
                const blobName = url.split('/')[4];
                await fetch(`${process.env.API_URL}/media/${containerName}/${blobName}`, {
                    method: 'PUT',
                    body: formData
                })
                    .then(response => response.json())
                    .then(res => {
                        log.info(res);
                        showAlert(res.message, 'success');
                        $('#profile-image').attr('src', res.data);
                        profileImgIsDirty = false;
                    })
                    .catch((error) => {
                        log.error(error.message, error);
                        showAlert(error.message, 'error');
                    });
            }
            else {
                await fetch(`${process.env.API_URL}/media/profile`, {
                    method: 'POST',
                    body: formData
                })
                    .then(response => response.json())
                    .then(res => {
                        log.info(res);
                        showAlert(res.message, 'success');
                        $('#profile-image').attr('src', res.data);
                        profileImgIsDirty = false;
                    })
                    .catch((error) => {
                        log.error(error.message, error);
                        showAlert(error.message, 'error');
                        $(this).remove();
                    });
            }
        }
        const formData = $('#profileData-form').serializeArray();
        var data = {};
        formData.forEach((element) => {
            data[element.name] = element.value;
        });
        data.userProfileImageUrl = $('#profile-image').attr('src');
        log.debug('User data:', data);
        await api.put('user', data)
            .then((response) => {
                log.debug('User data updated:', response.data);
                showAlert(response.message, 'success');
                localStorage.set('user', response.data);
                profileDataIsDirty = false;
            })
            .catch((error) => {
                log.error('User data update error:', error);
                showAlert(error.message, 'error');
            });

        $('#edit-button').removeClass('hidden');
        $('#save-button').addClass('hidden');
        $('#upload-button').toggleClass('hidden');
        $('#userName, #userPhoneNumber').prop('readonly', true);
    });

    $('#edit-button').click(() => {
        $('#edit-button').addClass('hidden');
        $('#save-button').removeClass('hidden');
        $('#upload-button').toggleClass('hidden');
        $('#userName, #userPhoneNumber').prop('readonly', false);
    });

    $('#upload-button').click(() => {
        $('#profileimage').click();
    });

    var user = await api.get('user')
        .then((response) => {
            log.debug('User data:', response.data);
            return response.data;
        })
        .catch((error) => {
            log.error('User data error:', error);
            showAlert(error.message, 'error');
        }
        );
    loadUserData(user);
}

module.exports = {
    profilePage,
    loadProfileCallback
}
