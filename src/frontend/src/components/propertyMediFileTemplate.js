
import UploadImage from '../../public/assets/Image/uploadImage.jpg'
const propertyMediFileTemplate = (mediaFile, index, propertyId) => {
    return `
    <tr data-index="${index}">
    <input type="hidden" name="mediaFileId" value="${mediaFile?.mediaFileId ?? 0}">
    <input type="hidden" name="url" value="${mediaFile?.url ?? ""}">
    <input type="hidden" name="propertyId" value="${mediaFile?.propertyId ?? propertyId}">
    <input type="hidden" name="type" value="${mediaFile?.type ?? "Image"}">
    <td class="mediaTable px-4 py-2" name="title">${mediaFile?.title ?? ""}</td>
    <td class="mediaForm hidden px-4 py-2">
        <input type="text" required name="title" placeholder="Media Title" class="px-3 py-2 border rounded flex-grow" value="${mediaFile?.title ?? ""}">
        <span class="text-red-500 text-xs italic"></span>
    </td>
    <td class="hidden px-4 py-2">
    </td>
    <td class="mediaTable px-4 py-2" name="url">${mediaFile?.url ? `<img src="${mediaFile.url}">` : ""}</td>
    <td class="mediaForm hidden px-4 py-2">
        <form class="uploadForm" enctype="multipart/form-data">
            <input required type="file" class="fileInput" name="file" accept="image/*,video/*" />
            <span class="text-red-500 text-xs italic"></span>
        </form>
    </td>
    <td class="px-4 py-2">
        <button class="mediaForm hidden saveBtn text-green-500">Save</button>
        <button class="mediaTable editBtn text-green-500 ml-2">Edit</button>
        <button class="deleteBtn remove-media text-red-500 ml-2">Delete</button>
        <button class="mediaTable refreshBtn remove-media ml-2 "><i class="fa-solid fa-arrow-rotate-right hover:rotate-90"></i></button>
    </td>
</tr>
    `;
}

export default propertyMediFileTemplate;