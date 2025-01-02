import FormProfile from "./FormProfile";
import ImageProfile from "./ImageProfile";

function Profile() {
  return (
    <>
      <div className="border-b p-3 bg-slate-100 rounded">
        <h1 className="text-2xl font-bold ml-5">My Profile</h1>{" "}
      </div>
      <div className="grid grid-cols-5 py-10 bg-slate-100 mt-2 rounded">
        <div className="col-span-3">
          <FormProfile />
        </div>
        <div className="col-span-2 ">
          <ImageProfile />
        </div>
      </div>
    </>
  );
}

export default Profile;
