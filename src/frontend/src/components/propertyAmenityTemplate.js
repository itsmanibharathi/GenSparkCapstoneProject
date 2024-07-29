const PropertyAmenityTemplate = (amenity) => {
    return `
            <div class="flex space-x-4 mb-2">
                <form>
                    <input type="number" class="hidden" name="amenityId" value="${amenity?.amenityId ?? ""}">
                    <input type="text" name="Name" placeholder="Amenity Name" class="px-3 py-2 border rounded flex-grow" value="${amenity?.name ?? ""}" >
                    <input type="text" name="Description" placeholder="Amenity Description" class="px-3 py-2 border rounded flex-grow"  value="${amenity?.description ?? ""}">
                    <label class="inline-flex items-center space-x-2">
                        <input type="checkbox" name="isPaid" ${amenity?.isPaid ? 'checked' : ""} class="form-checkbox">
                        <span>Is Paid</span>
                    </label>
                    <button type="button" class="remove-amenity bg-red-500 text-white px-3 py-2 rounded">Remove</button>
                </form>
            </div>
    `
}
export default PropertyAmenityTemplate;