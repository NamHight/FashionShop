import React from "react";

const SupplierCard = ({ supplier }) => {
  return (
            <div
        className="rounded-lg shadow-lg overflow-hidden bg-cover bg-center"
        style={{
            backgroundImage: `
            linear-gradient(rgba(255, 255, 255, 0.5), rgba(255, 255, 255, 0.5)),
            url(${supplier.status === "active"
                ? '../assets/images/suppiler/shiping.png'
                : '../assets/images/suppiler/noship.png'
            })
            `,
            backgroundSize: 'contain', // Hiển thị toàn bộ ảnh mà không cắt
            backgroundRepeat: 'no-repeat',
            backgroundPosition: 'right', // Đặt vị trí ảnh nền
            // Đặt chiều cao phù hợp
        }}
        >
      <input type="checkbox" id={`accordion-${supplier.supplierId}`} className="peer hidden" />
      <label
        htmlFor={`accordion-${supplier.supplierId}`}
        className={`flex items-center justify-between p-4 text-white cursor-pointer hover:bg-opacity-90 transition-colors ${
            supplier.status === "active"
            ? "bg-green-600 hover:bg-green-700"
            : supplier.status === "inactive"
            ? "bg-red-600 hover:bg-red-700"
            : "bg-blue-600 hover:bg-blue-700"
        }`}
        >
        <span className="text-lg font-semibold">{supplier.supplierName}</span>
        <svg
          className="w-6 h-6 transition-transform peer-checked:rotate-180"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
        >
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" />
        </svg>
      </label>
      <div className="max-h-0 overflow-hidden transition-all duration-300 peer-checked:max-h-screen">
        <div className="p-4">
          <p className="text-gray-700 leading-relaxed">
            <strong>Contact Name:</strong> {supplier.contactName}
          </p>
          <p className="text-gray-700 leading-relaxed">
            <strong>Phone:</strong> {supplier.phone}
          </p>
          <p className="text-gray-700 leading-relaxed">
            <strong>Email:</strong> {supplier.email}
          </p>
          <p className="text-gray-700 leading-relaxed">
            <strong>Address:</strong> {supplier.address}
          </p>
          <p className={`text-sm font-bold ${supplier.status === "active" ? "text-green-600" : "text-red-600"}`}>
            Status: {supplier.status}
          </p>
        </div>
      </div>
    </div>
  );
};

export default SupplierCard;
