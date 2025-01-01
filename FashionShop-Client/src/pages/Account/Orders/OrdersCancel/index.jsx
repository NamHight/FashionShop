function OrdersCancel() {
  return <>
     <div className="bg-slate-100 rounded">
        <div className="p-5 flex">
          <div className="flex w-full">
            <div>
              <img
                src="https://th.bing.com/th/id/OIP.Ci-Qe31tIUR4AcmesfCB8AHaHa?w=194&h=194&c=7&r=0&o=5&dpr=1.3&pid=1.7"
                alt="ảnh.jpg"
                width={150}
              />
            </div>
            <div className="ml-5 text-xl w-full h-full">
              <div>
                <h1 className="font-bold">Alo con bò</h1>
              </div>
              <div>
                <p>Loại: abc</p>
                <p>Lượng x2</p>
              </div>
            </div>
          </div>
          <div className="w-36 text-xl flex justify-center items-center text-red-600 ">
            $9.000
          </div>
        </div>
        <div className="px-5 pb-5 flex">
          <div className="text-xl flex w-full">
            <div className="mx-2">
              <button className="border px-16 py-3 rounded bg-red-600 text-white">
                Đánh Giá
              </button>
            </div>
            <div className="mx-2">
              <button className="border border-slate-500 px-9 py-3 rounded">Huỷ Đơn Hàng</button>
            </div>
          </div>
          <div className="text-xl w-80 flex justify-center items-center">
            <p className="font-bold flex items-center">
              Thành Tiền:{" "}
              <span className="text-red-600 text-3xl font-normal ml-3">
                $9.000
              </span>
            </p>
          </div>
        </div>
      </div>
  </>;
}

export default OrdersCancel;
