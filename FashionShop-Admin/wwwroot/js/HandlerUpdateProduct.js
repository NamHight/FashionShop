
if (typeof jQuery === "undefined") {
    throw new Error("jQuery plugins need to be before this file");
}

document.addEventListener("DOMContentLoaded", function () {
    var oldValue = null; // khởi tạo biến lưu giá trị cũ của thẻ select
    function handleUpdateProduct() {
        //  Khi DataTables vẽ lại, các phần tử select được thay thế, nhưng container cha (bảng) vẫn giữ nguyên.
        //Gắn sự kiện vào container cha sẽ đảm bảo tất cả các phần tử con mới được tạo đều kế thừa sự kiện.
        $('#myDataTable').on("mouseenter", "select", function () {
            //e.preventDefault();
            oldValue = $(this).val();
            console.log("gia tri cu la", oldValue);
        })

        $('#myDataTable').on("change", "select", function (e) {
            e.preventDefault(); // ngăn chặn đổi giá trị mới cho thẻ select trong trường hợp người dùng k xác nhận cập nhật
            const select = $(this);
            var idSelect = select.attr('id');
            var idProduct = null;
            var newData = select.val();
            var action = null;

            if (idSelect.includes("category-")) {
                idProduct = idSelect.slice(9);
                action = "UpdateCategoryId";
            } else {
                idProduct = idSelect.slice(7);
                action = "UpdateStatus";
            }

            Swal.fire({      //Phần 1 sau khi người dùng thao tác thay đổi dữ liệu bắt sự kiện gọi lên bảng thông báo với các mục sau
                title: "Xác nhận thay đổi",
                text: "Bạn có muốn thay đổi dữ liệu này thành: " + newData + "?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes",
                cancelButtonText: "No",
            }).then((result) => {   // Phần 2 xử lý nếu người dùng đồng ý hoặc không đồng ý
                if (result.isConfirmed) {
                    if (action === "UpdateStatus") {
                        var oldClass = select.attr('class').slice(6);
                        if (newData == "available") {
                            select.removeClass(oldClass).addClass("bg-success");
                        } else if (newData == "watting") {
                            select.removeClass(oldClass).addClass("bg-warning");
                        } else {
                            select.removeClass(oldClass).addClass("bg-danger");
                        }
                    }
                    $.ajax({
                        method: "POST",
                        url: `Products/${action}`,
                        data: {
                            newData: newData,
                            idProduct: idProduct,
                        },
                        success: function (res) {
                            console.log(res);
                        },
                        error: function (err) {
                            console.log(err);
                        },
                    })
                    Swal.fire(    // sau khi xử lý xong như lưu vào database thì hiển thị thông báo kết quả
                        "Thành công!",
                        "Dữ liệu đã được thay đổi.",
                        "success"
                    );
                } else {  // người dùng không đồng ý sẽ vào else
                    select.val(oldValue); // Khôi phục giá trị cũ
                    Swal.fire("Đã hủy", "Dữ liệu không thay đổi.", "error");  // thông báo khi người dùng không đồng ý
                }
            })
        })
    }

    // Khởi tạo dataTable với element table
    const table = $('#myDataTable')
        .addClass('nowrap')
        .dataTable({
            ordering: false,
            responsive: true,
            columnDefs: [
                { targets: [-1, -3], className: 'dt-body-right' }
            ]
        });

    $('.deleterow').on('click', function () {
        var tablename = $(this).closest('table').DataTable();
        tablename
            .row($(this)
                .parents('tr'))
            .remove()
            .draw();
    });

    // Gọi hàm gắn sự kiện ngay sau khi bảng được tải lần đầu
    handleUpdateProduct();

    // Gắn lại sự kiện mỗi khi bảng được vẽ lại, bao nhiêu hàm xử lý thì cần gọi lại bấy nhiêu hàm trong sự kiện draw
    table.on('draw', function () {
        handleUpdateProduct();
    });
})