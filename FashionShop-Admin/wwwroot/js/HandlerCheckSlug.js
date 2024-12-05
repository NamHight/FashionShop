function xuLySlug(nameProduct) {
    var result = nameProduct.toLowerCase();
    result = result.replace(/à|ả|ã|á|ạ|ă|ằ|ẳ|ẵ|ắ|ặ|â|ầ|ẩ|ẫ|ấ|ậ/gi, 'a');
    result = result.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    result = result.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    result = result.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    result = result.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    result = result.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    result = result.replace(/đ/gi, 'd');
    result = result.replace(/[^a-z0-9\s]/gi, '');
    result = result.replace(/\s+/gi, "-");
    return result;
}

function handleCheckSlug() {
    text = $("#ProductName").val(); // lấy giá trị trong trường productname
    text = xuLySlug(text); // xử lý tên productname thành slug
    $("#Slug").val(text); // chèn slug mới xử lý vào trường Slug
    // Kiểm tra xem slug mới tạo có tồn tại chưa
    $.ajax({
        method: "POST",
        url: "Products/CheckSlug",
        data: {
            slug: text
        },
        success: function (res) {
            if (res.statusCode == 0) {
                $("#errorSlug").text("Vui long nhap lai ten san pham, vi slug nay da ton tai");
                $("#ProductName").val("");
                $("#Slug").val("");
                $("#ProductName").focus();
            } else {
                $("#errorSlug").text("");
            }
        },
        error: function (err) {
            console.log(err)
        }
    })
}

document.addEventListener("DOMContentLoaded", function () {
    $('#ProductName').off('input').on('input', function () {
        handleCheckSlug();
    })
})