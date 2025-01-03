import React from "react";

const Blog = () => {
  return (
    <div className="container mx-auto">
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <div className="col-span-1">1</div>
        <div className="col-span-1">1</div>
        <div className="col-span-1">1</div>
        <div className="col-span-1">1</div>
        <div className="col-span-1">1</div>
        <div className="col-span-1">1</div>
      </div>
      <div className="flex items-center justify-end">
        <div className="flex items-center justify-between border-t border-gray-200 bg-white px-4 py-3 px-6">
          <div className="flex flex-1 items-center justify-between">
            <div>
              <a href="#" className="relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0">
                <span className="">Previous</span>
              </a>
              <a href="#" className="relative z-10 inline-flex items-center bg-indigo-600 px-4 py-2 text-sm font-semibold text-white focus:z-20 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">
                <span className="">1</span>
              </a>
              <a href="#" className="relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0">
                <span className="">2</span>
              </a>
              <a href="#" className="relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0">
                <span className="">Next</span>
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default Blog;
