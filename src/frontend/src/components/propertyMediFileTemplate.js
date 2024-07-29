const propertyMediFileTemplate = (mediaFile) => {
    return `
        <div class="flex space-x-4 mb-2">
            <Form>
                <input type="hidden" name="mediaFileId" value="${mediaFile?.mediaFileId ?? ""}">
                <input type="text" name="mediaTitle" placeholder="Media Title" class="px-3 py-2 border rounded flex-grow" value="${mediaFile?.title ?? ""}">
                <select name="mediaType" class="px-3 py-2 border rounded flex-grow">
                    <option value="" ${mediaFile?.type ?? 'selected'}>Select Media Type</option>
                    <option value="Image" ${mediaFile?.type === 'Image' ? 'selected' : ''}>Image</option>
                    <option value="Video" ${mediaFile?.type === 'Video' ? 'selected' : ''}>Video</option>
                </select>
                <form class="uploadForm" enctype="multipart/form-data">
                    <input type="file" class="fileInput" name="file" accept="image/*" />
                    <span class="file-url truncate">${mediaFile?.url ?? ""}</span>
                </form>
                <input type="hidden" name="mediaUrl" value="${mediaFile?.url ?? ""}" class="px-3 py-2 border rounded flex-grow">
                <button type="button" class="remove-media bg-red-500 text-white px-3 py-2 rounded">Remove</button>
            </Form>
        </div>
    `;
}

export default propertyMediFileTemplate;