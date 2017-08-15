// Write your Javascript code.
function imagePreview(input) {
    if (input.files && input.files[0]) {
        var filerdr = new FileReader();
        filerdr.onload = function (e) {
            $('#imgDisplayArea').attr('src', e.target.result);
        };
        filerdr.readAsDataURL(input.files[0]);
    }
}