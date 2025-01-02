function ImageProfile() {
  return (
    <div className="m-20 text-center">
      <div className="flex justify-center mb-5">
        <img
          src="https://th.bing.com/th?id=OIP.4akau9Zyzq-ioaE0S_YVrwHaHa&w=250&h=250&c=8&rs=1&qlt=90&o=6&dpr=1.3&pid=3.1&rm=2"
          alt="ảnh.jpg"
          width={200}
          className="rounded-full"
        />
      </div>
      <input type="file" name="image" id="image" hidden />
      <button className="border-2 text-lg px-5 py-3 rounded-full">Upload File</button>
    </div>
  );
}

export default ImageProfile;
