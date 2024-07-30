const PropertyAmenityTemplate = (amenity,index,propertyId) => {
    return `
    <tr class="amenityForms" data-index="${index}">
    <input  type="hidden" class="hidden" name="amenityId" value="${amenity?.amenityId ?? 0}">
    <input type="hidden" name="propertyId" value="${amenity?.propertyId ?? propertyId}">
    <td class="amenityTable px-4 py-2" name="name">${amenity?.name ?? ""}</td>
    <td class="amenityForm hidden px-4 py-2">
    <input required type="text" name="name" placeholder="Amenity Name" class="px-3 py-2 border rounded flex-grow" value="${amenity?.name ?? ""}">
    <span class="text-red-500 text-xs italic"></span>
    </td>
    <td class="amenityTable px-4 py-2">${amenity?.description ?? ""}</td>
    <td class="amenityForm hidden px-4 py-2">
    <input required type="text" name="description" placeholder="Amenity Description" class="px-3 py-2 border rounded flex-grow" value="${amenity?.description ?? ""}">
    <span class="text-red-500 text-xs italic"></span>
    </td>
    <td class="amenityTable px-4 py-2">${amenity?.isPaid ? "Paid" : "Free"}</td>
    <td class="amenityForm hidden px-4 py-2">
        <label class="inline-flex items-center space-x-2">
            <input type="checkbox" name="isPaid" ${amenity?.isPaid ? 'checked' : ""} class="form-checkbox">
            <span>Is Paid</span>
        </label>
        <span class="text-red-500 text-xs italic"></span>
    </td>
    <td class="px-4 py-2 flex flex-col justify-center md:flex-row ">
        <button class="amenityForm hidden saveBtn text-green-500">Save <i class="fa-solid fa-check"></i></button>
        <button class="amenityTable editBtn text-green-500 ml-2">Edit <i class="fa-solid fa-pen"></i></button>
        <button class="deleteBtn remove-amenity text-red-500 ml-2">Delete <i class="fa-solid fa-trash-can m-auto"></i></button>
    </td>
</tr>
    `
}
export default PropertyAmenityTemplate;

    // <form>
    //         <tr data-index="${index}">
    //                 <input type="hidden" class="hidden" name="amenityId" value="${amenity?.amenityId ?? ""}">
    //                 <td class="amenityTable px-4 py-2" name="name">${amenity?.name ?? ""}</td>
    //                 <td class="amenityForm hidden px-4 py-2"><input type="text" name="name" placeholder="Amenity Name" class="px-3 py-2 border rounded flex-grow" value="${amenity?.name ?? ""}" ></td>
    //                 <td class="amenityTable px-4 py-2">${amenity?.description ?? ""}</td>
    //                 <td class="amenityForm hidden px-4 py-2"><input type="text" name="description" placeholder="Amenity Description" class="px-3 py-2 border rounded flex-grow"  value="${amenity?.description ?? ""}"></td>
    //                 <td class="amenityTable px-4 py-2">${amenity?.isPaid ? "Paid" : "Free"}</td>
    //                 <td class="amenityForm hidden px-4 py-2">
    //                     <label class="inline-flex items-center space-x-2">
    //                         <input type="checkbox" name="isPaid" ${amenity?.isPaid ? 'checked' : ""} class="form-checkbox">
    //                         <span>Is Paid</span>
    //                     </label>
    //                 </td>
    //                 <td class="px-4 py-2">
    //                     <button class="amenityForm hidden saveBtn text-green-500">Save</button>
    //                     <button class="amenityTable editBtn text-green-500 ml-2">Edit</button>
    //                     <button class="deleteBtn remove-amenity text-red-500 ml-2">Delete</button>
    //                 </td>
    //                 </tr>
    //                 </form>
// <div class="flex space-x-4 mb-2">
//                 <form>
//                     <input type="number" class="hidden" name="amenityId" value="${amenity?.amenityId ?? ""}">
//                     <input type="text" name="Name" placeholder="Amenity Name" class="px-3 py-2 border rounded flex-grow" value="${amenity?.name ?? ""}" >
//                     <input type="text" name="Description" placeholder="Amenity Description" class="px-3 py-2 border rounded flex-grow"  value="${amenity?.description ?? ""}">
                    
//                     <button type="button" class="remove-amenity bg-red-500 text-white px-3 py-2 rounded">Remove</button>
//                 </form>
//             </div>