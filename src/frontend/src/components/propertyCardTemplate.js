const propertyCardTemplate = (property) => {
    const imagesHtml = property.mediaFiles.map((mediaFile, index) => `                        
        <img src="${mediaFile.url}" alt="${mediaFile.title}" class="${index === 0 ? 'active' : ''} w-full h-48 object-cover">
    `).join('');

    const cardHtml = `
                    <div class=" bg-white shadow-lg rounded-lg overflow-hidden sm:flex sm:flex-row">
                        <div class="carousel relative">
                            ${imagesHtml}
                            <button class="carousel-button prev absolute left-2 top-1/2 transform -translate-y-1/2 bg-gray-800 text-white px-2 py-1 rounded-full">❮</button>
                            <button class="carousel-button next absolute right-2 top-1/2 transform -translate-y-1/2 bg-gray-800 text-white px-2 py-1 rounded-full">❯</button>
                        </div>
                        <div class="p-4">
                            <h2 class="text-xl font-semibold">${property.Title} <span class="${property.Category}">${property.Category} </span> </h2> 
                            <p class="text-gray-600">${property.Description}</p>
                            <p class="text-gray-800 font-bold">$${property.Price}</p>
                            <p class="text-gray-600">${property.Street}, ${property.City}, ${property.State}, ${property.Country}</p>
                            <p class="text-gray-500">${property.ZipCode}</p>
                            <p class="text-gray-500">${property.Status}</p>
                        </div>
                    </div>
                `;
    return cardHtml;
}

export default propertyCardTemplate;