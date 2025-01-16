import React, {useEffect} from 'react';
import { useLocation, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';


function Page404 () {
    const { param1 } = useParams();
    const location = useLocation();
    const errorPayment = location.state?.errorPayment;
    useEffect(()=>{
      if (errorPayment) {
        console.log("da vao 404", errorPayment, " ", decodeURIComponent(param1));
        toast.error(errorPayment); // đã khai báo toastcontainer trong layout
      }
    }, [errorPayment])
    return (
      <div className="w-full mt-3 h-96 bg-slate-100 rounded py-2 flex justify-center items-center">
        <div>
          {/* <p className="flex justify-center items-center">X</p> */}
          <p className="text-4xl font-bold text-slate-950">{decodeURIComponent(param1)}</p>
        </div>
      </div>
    );
  }
  
  export default Page404;