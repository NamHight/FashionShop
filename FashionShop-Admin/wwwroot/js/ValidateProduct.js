document.addEventListener("DOMContentLoaded", function () {
    const image = ["jpg", "jpeg", "png", "gif", "bmp", "tiff", "webp"];
    $("#Banner").off("input").on("input", function () {
        var input_value = $(this).val();
        console.log(input_value);
        var path = input_value.split(".");
        console.log(path[1]);
        if (image.includes(path[1])) {
            $("#errorBanner1").text("");
            console.log("Nhap dung dinh dang roi");
        } else {
            $("#errorBanner1").text("Vui lòng nhập đúng định dạng là ảnh!");
            console.log("Nhap sai dinh dang anh roi");
        }
    })
})