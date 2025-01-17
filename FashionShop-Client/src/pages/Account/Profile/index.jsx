import { useAuth } from "../../../context/AuthContext";
import FormProfile from "./FormProfile";
import ImageProfile from "./ImageProfile";

function Profile() {
  const {user,isLoading,error} = useAuth();
  
  return (
    <>
      <div className="border-b py-2 bg-emerald-400 text-white rounded">
        <h1 className="text-2xl font-bold text-center ml-5">My Profile</h1>
      </div>
      <div className="grid grid-cols-6 py-10 bg-slate-100 mt-2 rounded">
        <div className="col-span-4">
          <FormProfile user={user}/>
        </div>
        <div className="col-span-2 text-start">
          <ImageProfile user={user}/>
        </div>
      </div>
    </>
  );
}

export default Profile;
