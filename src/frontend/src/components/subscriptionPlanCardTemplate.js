const subscriptionPlanTemplate = (plan) => {
    return `
    <div class=" transition ease-in-out delay-150 hover:-translate-y-1 hover:scale-110 duration-300 bg-white p-6 rounded-lg shadow-md ">
        <h2 class="text-xl font-semibold mb-2">${plan.subscriptionPlanName}</h2>
        <p class="text-gray-700 mb-4">${plan.subscriptionPlanDescription}</p>
        <p class="text-gray-900 font-bold mb-2">Price: ${plan.subscriptionPlanPrice == 0 ? 'Free' : '₹' + plan.subscriptionPlanPrice}</p>
        <p class="text-gray-600">Duration: ${plan.subscriptionPlanDuration} ${plan.subscriptionPlanDurationType == 'Count' ? 'Post View' : plan.subscriptionPlanDurationType}</p>
        <button value=${plan.subscriptionPlanId} class="subBtn  bg-primary text-white px-4 py-2 rounded mt-4">Subscribe</button>
    </div>
    `;
}

export default subscriptionPlanTemplate;