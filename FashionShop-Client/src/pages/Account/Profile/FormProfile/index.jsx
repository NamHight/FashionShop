import { Radio, Typography } from "@material-tailwind/react";
function FormProfile() {
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
          <Radio>
            <div className="flex items-center gap-2">
              <Radio.Item id="Male">
                <Radio.Indicator />
              </Radio.Item>
              <Typography as="label" htmlFor="Male" className="text-foreground">
                Male
              </Typography>
            </div>
            <div className="flex items-center gap-2">
              <Radio.Item id="Female">
                <Radio.Indicator />
              </Radio.Item>
              <Typography
                as="label"
                htmlFor="Female"
                className="text-foreground"
              >
                Female
              </Typography>
            </div>
            <div className="flex items-center gap-2">
              <Radio.Item id="Another">
                <Radio.Indicator />
              </Radio.Item>
              <Typography
                as="label"
                htmlFor="Another"
                className="text-foreground"
              >
                Another
              </Typography>
            </div>
          </Radio>
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
            />
          </div>
        </div>
        <div className="text-center">
          <button
            type="submit"
            className="border-2 px-5 py-3 rounded-full w-full"
          >
            Lưu
          </button>
        </div>
      </form>
    </div>
  );
}

export default FormProfile;
