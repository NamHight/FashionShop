import {
  Typography,
  Dialog,
  Button,
  IconButton,
  Radio,
} from "@material-tailwind/react";
import { IoMale, IoFemale, IoMaleFemale } from "react-icons/io5";
import { Xmark } from "iconoir-react";

export const FormUpdate = ({value}) => {
  return (
    <Dialog size="sm">
      <Dialog.Trigger
        as={Button}
        className="w-full border-0 bg-green-600 hover:bg-green-500"
      >
        Update
      </Dialog.Trigger>
      <Dialog.Overlay>
        <Dialog.Content>
          <Dialog.DismissTrigger
            as={IconButton}
            size="sm"
            variant="ghost"
            color="secondary"
            className="absolute right-2 top-2"
            isCircular
          >
            <Xmark className="h-5 w-5" />
          </Dialog.DismissTrigger>
          <Typography type="h5" className="rmb-1 text-center">
            Update Information
          </Typography>
          <form action="#" className="mt-6">
            <div class="relative z-0 w-full mb-5 group">
              <input
                type="text"
                name="name"
                id="name"
                class="block py-1.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                placeholder={value.customerName}
                required
              />
              <label
                for="name"
                class="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-1 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
              >
                My Name
              </label>
            </div>
            <div class="relative z-0 w-full mb-5 group">
              <input
                type="text"
                name="floating_email"
                id="floating_email"
                class="block py-1.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                placeholder=" "
                required
              />
              <label
                for="floating_email"
                class="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-1 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
              >
                Email address
              </label>
            </div>
            <div class="relative z-0 w-full mb-5 group">
              <input
                type="tel"
                pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}"
                name="floating_phone"
                id="floating_phone"
                class="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                placeholder=" "
                required
              />
              <label
                for="floating_phone"
                class="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
              >
                Phone number (123-456-7890)
              </label>
            </div>
            <div class="grid md:grid-cols-2 md:gap-6">
              <div class="relative z-0 w-full mb-5 group">
                <input
                  type="text"
                  name="Addreess"
                  id="Addreess"
                  class="block py-1.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                  placeholder=" "
                  required
                />
                <label
                  for="Addreess"
                  class="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-1 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
                >
                  Addreess
                </label>
              </div>
              <div class="relative z-0 w-full mb-5 group">
                <input
                  type="date"
                  name="Birthday"
                  id="Birthday"
                  class="block py-1.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                  placeholder=" "
                  required
                />
                <label
                  for="Birthday"
                  class="peer-focus:font-medium absolute text-sm text-gray-500 dark:text-gray-400 duration-300 transform -translate-y-6 scale-75 top-1 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 peer-focus:text-blue-600 peer-focus:dark:text-blue-500 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6"
                >
                  Birthday
                </label>
              </div>
            </div>
            <div class="grid md:grid-cols-2 md:gap-6">
              <div class="flex">
                <label
                  for="Gender"
                  class="mr-3 text-gray-500">
                  Gender
                </label>
                <Radio >
                  <div className="flex items-center gap-2">
                    <Radio.Item id="Male">
                      <Radio.Indicator />
                    </Radio.Item>
                    <Typography
                      as="label"
                      htmlFor="Male"
                      className="text-foreground flex items-center"
                    >
                      Male <IoMale className="text-blue-500 ml-1" />
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
                      Female <IoFemale className="text-red-500 ml-1" />
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
                      Another <IoMaleFemale className="ml-1" />
                    </Typography>
                  </div>
                </Radio>
              </div>
            </div>
            <Button
              isFullWidth
              className="w-10 border-0 bg-blue-600 hover:bg-blue-500 mt-6"
            >
              Save
            </Button>
          </form>
        </Dialog.Content>
      </Dialog.Overlay>
    </Dialog>
  );
};
