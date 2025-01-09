import React from "react";
const PaginationList = (props) => {
    const TotalPages = props.TotalPages
    const CurrentPage = props.CurrentPage;
    const HanndlePreOrNext = props.HanndlePreOrNext;
    const handlePage = props.handlePage;
    const pages = [];
    for (
      let i = Math.max(1, CurrentPage - 2);
      i <= Math.min(TotalPages, CurrentPage + 2);
      i++
    ) {
      pages.push(i);
    }
    return (
      <div className="flex items-center justify-end">
        <div className="flex items-center justify-between border-t border-gray-200 bg-white px-4 py-3 px-6">
          <div className="flex flex-1 items-center justify-between">
            <button
              onClick={() => {
                HanndlePreOrNext(false);
              }}
              className="inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
            >
              Previous
            </button>
            {pages &&
              pages.map((item) => {
                return (
                  <button
                    className={`${
                      item === CurrentPage ? "bg-blue-500" : ""
                    } inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-blue-500 focus:z-20 focus:outline-offset-0`}
                    onClick={() => {
                      handlePage(item);
                    }}
                    key={item}
                  >
                    {item}
                  </button>
                );
              })}

            <button
              onClick={() => {
                HanndlePreOrNext(true);
              }}
              className="inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
            >
              Next
            </button>
          </div>
        </div>
      </div>
    );
  };
  export default PaginationList;