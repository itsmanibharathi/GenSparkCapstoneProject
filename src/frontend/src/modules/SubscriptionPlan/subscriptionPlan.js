import $ from 'jquery';
import subscriptionPlanPage from './subscriptionPlan.html';
import loadRoutes from '../../Services/routerService.js';
import log from '../../utility/loglevel.js';
import subscriptionPlanTemplate from '../../components/subscriptionPlanCardTemplate.js';
import showAlert from '../../Services/alertService.js';

const loadSubscriptionPlanCallback = (query, api, token, localStorage) => {
    // loadRoutes('/subscription-plan');
    log.info('Subscription Plan Page Loaded');
    $('#subscriptionPlans').on('click', '.subBtn', function () {
        if (!token) {
            alert('Please login to subscribe');
            return;
        }
        const planId = $(this).val();
        api.post('subscription/subscribe/' + planId)
            .then(response => {
                log.info(response);
                showAlert('Subscribed successfully', 'success');
                loadRoutes('/');
            })
            .catch(error => {
                log.error(error);
                showAlert(error.message, 'error');
            });
    });

    const subscriptionPlanContainer = $('#subscriptionPlans');
    subscriptionPlanContainer.empty();
    api.get('subscription/plan')
        .then(response => {
            response.data.forEach(plan => {
                subscriptionPlanContainer.append(subscriptionPlanTemplate(plan));
            });
        })
        .catch(error => {
            log.error(error);
        });

}

module.exports = {
    subscriptionPlanPage,
    loadSubscriptionPlanCallback
}
