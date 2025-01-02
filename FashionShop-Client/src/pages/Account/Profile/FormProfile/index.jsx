import { Radio, Typography } from "@material-tailwind/react";
import { IoMale, IoFemale, IoMaleFemale  } from "react-icons/io5";
import { useState } from "react";
import { getCustomerById } from './../../../../services/api/CustomerService';
function FormProfile() {
  const [update, setUpdate] = useState(false);
  var test = getCustomerById(43)
  console.log(test);
  
  return (
    <div className="mx-28">
      <form action="/account" method="post" className="text-lg text-slate-700">
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
              placeholder="Tuấn..."
              className="block w-full rounded-md bg-white p-3 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
              disabled={!update}
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
              placeholder="Tuấn..."
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
              placeholder="Tuấn..."
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
              placeholder="Tuấn..."
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
          {!update ? <p className="text-xl flex items-center">Male <IoMale className="text-blue-500 ml-1" size={23}/></p> : <Radio>
            <div className="flex items-center gap-2">
              <Radio.Item id="Male">
                <Radio.Indicator />
              </Radio.Item>
              <Typography as="label" htmlFor="Male" className="text-foreground flex items-center">
                Male <IoMale className="text-blue-500 ml-1"/>
              </Typography>
            </div>
            <div className="flex items-center gap-2">
              <Radio.Item id="Female">
                <Radio.Indicator />
              </Radio.Item>
              <Typography
                as="label"
                htmlFor="Female"
                className="text-foreground flex items-center"
              >
                Female <IoFemale className="text-red-500 ml-1"/>
              </Typography>
            </div>
            <div className="flex items-center gap-2">
              <Radio.Item id="Another">
                <Radio.Indicator />
              </Radio.Item>
              <Typography
                as="label"
                htmlFor="Another"
                className="text-foreground flex items-center"
              >
                Another <IoMaleFemale className="ml-1"/>
              </Typography>
            </div>
          </Radio>}
        </div>
        <div className="my-5">
          <label
            htmlFor="Birthday"
            className="block text-xl text-gray-900 font-bold"
          >
            Birthday
          </label>
          <div className="mt-2">
            <input
              type="date"
              name="Birthday"
              id="Birthday"
              className="block w-full rounded-md bg-white p-3 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600"
              disabled={!update}
           />
          </div>
        </div>
        <div className="text-center flex">
          {!update ? (
            <button
              type="button"
              className="border-2 px-5 py-3 bg-yellow-400 w-full rounded-full"
              onClick={() => setUpdate(!update)}
            >
              Update
            </button>
          ) : (
            <button
              type="button"
              className="border-2 w-full px-5 py-3 rounded-full bg-green-500 text-white"
            >
              Lưu
            </button>
          )}
          {update && (
            <button type="button" className="border-2 w-full px-5 bg-red-500 text-white py-3 rounded-full" onClick={() => setUpdate(!update)}>
              Huỷ
            </button>
          )}
        </div>
      </form>
    </div>
  );
}

export default FormProfile;
