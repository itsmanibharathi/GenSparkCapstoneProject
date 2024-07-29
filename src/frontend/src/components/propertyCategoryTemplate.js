const PropertyHomeTemplate = (home) => {
    console.log(home)
    return `
        <span >
            <form id="HomeForm">
                <input type="hidden" name="propertyId" value="${home?.propertyId ?? ""}">
                <div class="mb-4">
                    <label for="area" class="block text-gray-700">Area</label>
                    <input type="number" name="area" id="area"  class="w-full px-3 py-2 border rounded" value=${home?.area ?? ""}>
                </div>
                <div class="mb-4">
                    <label for="numberOfBedrooms" class="block text-gray-700">Number of Bedrooms</label>
                    <input type="number" name="numberOfBedrooms" id="numberOfBedrooms" class="w-full px-3 py-2 border rounded" value=${home?.numberOfBedrooms ?? ""}>
                </div>
                <div class="mb-4">
                    <label for="numberOfBathrooms" class="block text-gray-700">Number of Bathrooms</label>
                    <input type="number" name="numberOfBathrooms" id="numberOfBathrooms" class="w-full px-3 py-2 border rounded" value=${home?.numberOfBathrooms ?? ""}>
                </div>
                <div class="mb-4">
                    <label for="yearBuilt" class="block text-gray-700">Year Built</label>
                    <input type="number" name="yearBuilt" id="yearBuilt" class="w-full px-3 py-2 border rounded" value=${home?.yearBuilt ?? ""}>
                </div>
                <div class="mb-4">
                    <label for="furnishingStatus" class="block text-gray-700">Furnishing Status</label>
                    <select name="furnishingStatus" id="furnishingStatus" class="w-full px-3 py-2 border rounded">
                        <option value="" ${home?.furnishingStatus ?? 'selected'} >Select Option</option>
                        <option value="Furnished" ${home?.furnishingStatus == 'Furnished' ? 'selected' : ""} >Furnished</option>
                        <option value="SemiFurnished" ${home?.furnishingStatus == 'Semi-Furnished' ? 'selected' : ""}>Semi-Furnished</option>
                        <option value="Unfurnished" ${home?.furnishingStatus == 'Unfurnished' ? 'selected' : ""}>Unfurnished</option>
                    </select>
                </div>
                <div class="mb-4">
                    <label for="floorNumber" class="block text-gray-700">Floor Number</label>
                    <input type="number" name="floorNumber" id="floorNumber" class="w-full px-3 py-2 border rounded" value=${home?.floorNumber ?? ""}>
                </div>
            </form>
        </span>
    `
}

const PropertyLandTemplate = (land) => {
    console.log(land)
    return `
    <span>
        <form id="LandForm">
            <input type="hidden" name="propertyId" value="${home?.propertyId ?? ""}">
            <div class="mb-4">
                <label for="landArea" class="block text-gray-700" >Land Area</label>
                <input type="number" name="landArea" id="landArea" class="w-full px-3 py-2 border rounded" value=${land?.landArea ?? ""}>
            </div>
            <div class="mb-4">
                <label for="zoningInformation" class="block text-gray-700">Zoning Information</label>
                <input type="text" name="zoningInformation" id="zoningInformation" class="w-full px-3 py-2 border rounded" value=${land?.zoningInformation ?? ""}>
            </div>
            <div class="mb-4">
                <label for="landType" class="block text-gray-700">Land Type</label>
                <select name="landType" id="landType" class="w-full px-3 py-2 border rounded">
                    <option value="" ${land?.landType ?? 'selected'}>Select Option</option>
                    <option value="Residential" ${home?.furnishingStatus == 'Residential' ? 'selected' : ""}>Residential</option>
                    <option value="Commercial" ${home?.furnishingStatus == 'Commercial' ? 'selected' : ""}>Commercial</option>
                    <option value="Agricultural" ${home?.furnishingStatus == 'Agricultural' ? 'selected' : ""} >Agricultural</option>
                </select>
            </div>
        </form>
    </span>
    `
}



export default {
    "PropertyHome": PropertyHomeTemplate,
    "PropertyLand": PropertyLandTemplate
}