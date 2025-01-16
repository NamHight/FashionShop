import { useState } from "react";

function ImageProfile({user}) {
  const [file, setFile] = useState()
  function handleFile(event){
    setFile(event.target.files)
    console.log(event.target.files[0])
  }
  const handleUpload = (e) => {
    e.preventDefault()
    const form = new FormData(e.target);
    console.log('form', form.get('file'))
  }
  return (
    <div className="m-20 text-center avatar">
      
      <span className="flex justify-center mb-5 w-44 h-44">
        <img
          alt={user?.avatar}
          src={
            user.avatar
              ? "/assets/images/avatar/" + user.avatar
              : "/assets/images/Logo.png"
          }
          width={200}
          className="rounded-full size-36 object-cover border border-blue-500 ring-4 ring-blue-500/20 cursor-pointer"
        />
      </span>
      <form onSubmit={(e)=>handleUpload(e)}>
        <input type="file" name="file" onChange={(e) => handleFile(e)} hidden/>
        <button  className="border-2 text-lg px-5 py-3 rounded-full">Upload File</button>
      </form>
    </div>
  );
}

export default ImageProfile;
