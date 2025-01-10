import { Spinner } from "@material-tailwind/react";

export const CustomSpinner = () => {
  return (
    <>
      <div className="w-full mt-3 h-96 bg-slate-100 rounded py-2 flex justify-center items-center">
        <Spinner size="xxl" />
      </div>
    </>
  );
};
