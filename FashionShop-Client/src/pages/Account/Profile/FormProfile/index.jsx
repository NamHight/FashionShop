import { IoMale, IoFemale, IoMaleFemale } from "react-icons/io5";
import { useState } from "react";
import { FormUpdate } from "./FormUpdate";

function FormProfile({ user }) {
  const [update, setUpdate] = useState(false);
  const handleDate = (birth) => {
    if (!update) {
      return <p>{birth}</p>;
    } else {
      return (
        <input
          type="date"
          name="Birthday"
          id="Birthday"
          className="block w-full rounded-md bg-white p-3 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
          disabled={!update}
        />
      );
    }
  };
  return (
    <div className="mx-28">
      <div className="text-lg text-slate-700">
        <div className="my-5">
          <label
            htmlFor="Name"
            className="block text-xl text-gray-900 font-bold"
          >
            My Name
          </label>
          <div className="mt-2">
            <input
              type="text"
              name="Name"
              id="Name"
              className="block w-full rounded-md bg-white p-3 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
              disabled={!update}
              placeholder={user.customerName}
            />
          </div>
        </div>
        <div className="my-5">
          <label
            htmlFor="Email"
            className="block text-xl text-gray-900 font-bold"
          >
            Email
          </label>
          <div className="mt-2">
            <input
              type="email"
              name="Email"
              id="Email"
              placeholder={user.email}
              className="block w-full rounded-md bg-white p-3 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
              disabled={!update}
            />
          </div>
        </div>
        <div className="my-5">
          <label
            htmlFor="Phone"
            className="block text-xl text-gray-900 font-bold"
          >
            Phone
          </label>
          <div className="mt-2">
            <input
              type="text"
              name="Phone"
              id="Phone"
              placeholder={user.phone}
              className="block w-full rounded-md bg-white p-3 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
              disabled={!update}
            />
          </div>
        </div>
        <div className="my-5">
          <label
            htmlFor="Address"
            className="block text-xl text-gray-900 font-bold"
          >
            Address
          </label>
          <div className="mt-2">
            <input
              type="text"
              name="Address"
              id="Address"
              placeholder={user.address}
              className="block w-full rounded-md bg-white p-3 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
              disabled={!update}
            />
          </div>
        </div>
        <div className="my-5 flex">
          <label
            htmlFor="react"
            className="mr-5 block text-xl text-gray-900 font-bold"
          >
            Gender
          </label>
            <p className="text-xl flex items-center">
              {user.gender === "male" ? (
                <>
                  Male <IoMale className="text-blue-500 ml-1" size={23} />
                </>
              ) : user.gender === "female" ? (
                <>
                  Female <IoFemale className="text-blue-500 ml-1" size={23} />
                </>
              ) : (
                <>
                  Another
                  <IoMaleFemale className="text-blue-500 ml-1" size={23} />
                </>
              )}{" "}
            </p>
        </div>
        <div className="my-5 flex items-center">
          <label
            htmlFor="Birthday"
            className="block text-xl text-gray-900 font-bold mr-5"
          >
            Birthday
          </label>
          {handleDate(user.birth)}
        </div>
        <div className="text-center flex">
          <FormUpdate value={user}/>
        </div>
      </div>
    </div>
  );
}

export default FormProfile;
