const propertyCardTemplate = (property) => {
    const imagesHtml = property['mediaFiles'].map((mediaFile, index) => `
        <div class="carousel-item ${index === 0 ? 'carousel-active' : ''}">
            <img src="${mediaFile.url}" alt="${mediaFile.title}" class="w-full h-full object-cover">
            <div class="carousel-caption absolute bottom-0 left-0 w-full bg-black bg-opacity-50 text-white p-2 text-sm">
                ${mediaFile['title']}
            </div>
        </div>
    `).join('');

    var HomeTemplate = (home) => {
        return `
        <p class="text-gray-500"><i class="fas fa-couch"></i> ${home.furnishingStatus}</p>
        <p class="text-gray-500"><i class="fas fa-expand-arrows-alt"></i> ${home.area} sqft</p>
        <p class="text-gray-500"><i class="fas fa-bed"></i> ${home.numberOfBedrooms} Bedrooms</p>
        <p class="text-gray-500"><i class="fas fa-bath"></i> ${home.numberOfBathrooms} Baths</p>
        <p class="text-gray-500"><i class="fas fa-calendar-alt"></i> Built in ${home.yearBuilt}</p>
        <p class="text-gray-500"><i class="fas fa-building"></i> Floor Number: ${home.floorNumber}</p>
        `
    }

    var LandTemplate = (Land) => {
        return `
        <p class="text-gray-500"><i class="fas fa-expand-arrows-alt"></i> ${Land.landArea} sqft</p>
        <p class="text-gray-500"><i class="fas fa-map-marked-alt"></i> ${Land.zoningInformation}</p>
        `
    }

    // add tooltip for every amenities name



    // const highlights = property['amenities'].map(amenity => amenity['name']).join(', ');
    const highlights = property['amenities'].map(amenity => {
        return `<span class="tooltip">${amenity['name']}
                    <span class="tooltiptext">${amenity['isPaid'] == true || 'true' ? 'Paid ' : 'Free '}${amenity['description']}</span>
                </span>`;
    }).join('• ');


    const cardHtml = `
        <div class="bg-white shadow-lg rounded-lg overflow-hidden sm:flex sm:flex-row m-4">
            <div class="carousel relative sm:w-1/2" style="height: 300px;">
                <div class="carousel-inner h-full">
                    ${imagesHtml}
                </div>
                <button class="carousel-button prev absolute left-2 top-1/2 transform -translate-y-1/2 bg-gray-800 text-white px-2 py-1 rounded-full"><i class="fas fa-chevron-left"></i></button>
                <button class="carousel-button next absolute right-2 top-1/2 transform -translate-y-1/2 bg-gray-800 text-white px-2 py-1 rounded-full"><i class="fas fa-chevron-right"></i></button>
            </div>
            <div class="p-4 sm:w-1/2">
                <h2 class="text-xl font-semibold">${property.title} ${property.category}</h2>
                <p class="text-gray-600">${property.description}</p>

                <p class="text-gray-800 font-bold"> For: <span class=" font-thin" >${property.type}</span></p>
                <p class="text-gray-800 font-bold"> Price: <span class=" font-thin" >
                ${property.type == 'Rent' ? `₹${property.price.toLocaleString()}/month + Deposit ₹${(property.price * 3).toLocaleString()} ` : `₹${property.price.toLocaleString()}`} 
                </span>
                </p>
                <p class="text-gray-600"><i class="fas fa-map-marker-alt"></i> ${property.street}, ${property.city}, ${property.state}, ${property.country}, ${property.zipCode}</p>
                <div class=" grid grid-cols-2 ">
                    ${property.category === 'Home' ? HomeTemplate(property.home) : LandTemplate(property.land)}
                </div>
                <div class="mt-2">
                    <span class="text-sm font-semibold">Highlights:</span>
                    <span class="text-sm">${highlights}</span>
                </div>
                <div class="mt-4 flex justify-between items-center">
                    <button property-id="${property.propertyId}" class="viewOwnerInfo bg-blue-500 text-white px-4 py-2 rounded"><i class="fas fa-phone-alt"></i> View Number</button>
                    <button property-id="${property.propertyId}" class="ContactMe bg-green-500 text-white px-4 py-2 rounded"><i class="fas fa-envelope"></i> Contact Me</button>
                </div>
                <div class="ownerInfo hidden">
                    <span class="text-sm font-semibold">Property Owner:</span>
                    <span class="ownerName text-sm"></span>
                    <span class="ownerEmail text-sm"></span>
                    <span class="ownerPhoneNumber text-sm"></span>
                </div>
                <div class="text-sm text-gray-500 mt-2"><i class="fas fa-users"></i> 2 people already contacted this week</div>
            </div>
        </div>
    `;
    return cardHtml;
}

export default propertyCardTemplate;



