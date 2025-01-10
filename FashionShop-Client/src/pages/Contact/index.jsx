import React, { useState } from "react";
import {postAddContact} from "../../services/api/ContactService";
import { useMutation } from "@tanstack/react-query";
import { data } from "react-router";
const Contact = () => {
  const [message, setMessage] = useState(null);
  const mutationContact = useMutation({
    mutationKey: ['addContact'],
    mutationFn: async (data) => {
      const result = await postAddContact(data);
      return result;
    },onSuccess: (data) => {
      console.log(data);
      setMessage({
        type : "success",
        data : data.data,
      });
      
    },onError: error => {
      console.log(error);
      setMessage({
        type : "error",
        data : "Gửi liên hệ không thành công!",
      });
    }
  });
  const addContact = async (event) =>{
    event.preventDefault();
    const dataForm = Object.fromEntries(new FormData(event.target));
    console.log(dataForm);

    await mutationContact.mutateAsync(dataForm);

    setTimeout(() => {
      setMessage(null)
    },5000);
    event.target[0].value = "";
    event.target[1].value = "";
    event.target[2].value = null;
    event.target[3].value = "";
  }
  
  return (
    <div className="container mx-auto">
      <div className="grid grid-col-2 grid-flow-col gap-4">
        <div className="col-span-6">
          {
            message && (
              <div  style={{color : message.type === "success" ? "green" : "red"}}>
                {message.data}
              </div>
            )
          }
          <form method="post" onSubmit={addContact}>
            <div className="border-b border-gray-900/10 pb-12">
              <h2 className="text-base/7 font-semibold text-gray-900">
                Liên hệ
              </h2>
              <div className="mt-10 grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-6">
                <div className="col-span-6">
                  <label
                    htmlFor="fullname"
                    className="block text-sm/6 font-medium text-gray-900"
                  >
                    FullName
                  </label>
                  <div className="mt-2">
                    <div className="flex items-center rounded-md bg-white pl-3 outline outline-1 -outline-offset-1 outline-gray-300 focus-within:outline focus-within:outline-2 focus-within:-outline-offset-2 focus-within:outline-indigo-600">
                      <input
                        id="fullname"
                        name="fullname"
                        type="text"
                        placeholder=""
                        className="block min-w-0 grow py-1.5 pl-1 pr-3 text-base text-gray-900 placeholder:text-gray-400 focus:outline focus:outline-0 sm:text-sm/6"
                      />
                    </div>
                  </div>
                </div>
                <div className="col-span-6">
                  <label
                    htmlFor="email"
                    className="block text-sm/6 font-medium text-gray-900"
                  >
                    Email
                  </label>
                  <div className="mt-2">
                    <input
                      id="email"
                      name="email"
                      type="email"
                      autoComplete="email"
                      className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                    />
                  </div>
                </div>

                <div className="col-span-6">
                  <label
                    htmlFor="phone"
                    className="block text-sm/6 font-medium text-gray-900"
                  >
                    Phone
                  </label>
                  <div className="mt-2">
                    <input
                      id="phone"
                      name="phone"
                      type="number"
                      autoComplete="phone"
                      className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                    />
                  </div>
                </div>
                <div className="col-span-6">
                  <label
                    htmlFor="about"
                    className="block text-sm/6 font-medium text-gray-900"
                  >
                    Description
                  </label>
                  <div className="mt-2">
                    <textarea
                      id="description"
                      name="description"
                      rows={3}
                      className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                      defaultValue={""}
                    />
                  </div>
                </div>
              </div>
            </div>
            <div className="mt-6 flex items-center justify-end gap-x-6">
              <button
                type="submit"
                className="rounded-md bg-indigo-600 px-4 py-3 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
              >
                Save
              </button>
            </div>
          </form>
        </div>
        <div className="col-span-6"></div>
      </div>
    </div>
  );
};
export default Contact;
