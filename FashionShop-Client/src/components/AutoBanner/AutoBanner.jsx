import React, { useEffect, useState } from "react";
import "../AutoBanner/AutoBanner.css";
const AutoBanner = () => {
  const images = [
    "/assets/images/advertisement/quangcao1.png",
    "/assets/images/advertisement/quangcao2.png",
    "/assets/images/advertisement/quangcao3.png",
    "/assets/images/advertisement/quangcao4.png",
  ];

  const [currentIndex, setCurrentIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
    }, 5000); // Chuyển ảnh sau mỗi 5 giây

    return () => clearInterval(interval); // Dọn dẹp interval khi component bị unmount
  }, [images.length]);

  return (
    <div className="banner-container">
      <img
        src={images[currentIndex]}
        alt={`Advertisement ${currentIndex + 1}`}
        className="banner-image"
      />
    </div>
  );
};

export default AutoBanner;
