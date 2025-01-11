import React from 'react';
import {Link} from "react-router";
import {Typography} from "@material-tailwind/react";

const ProductSearch = ({productDetail}) => {
    return (
        <Link to={`${productDetail?.slug}`} className={'flex justify-start w-full items-center gap-2 hover:opacity-80'}>
                <img src={"https://dub.sh/TdSBP0D"} className={'object-fit size-14 rounded'}
                     alt={productDetail.productName}/>
            <div className={'flex flex-col ml-2'}>
                <Typography to={`${productDetail?.productName}`} className={'text-gray-600'} >{productDetail?.productName}</Typography>
                <span className={'text-red-500 font-sans'}>${productDetail?.price}</span>
            </div>
        </Link>
    );
};

export default ProductSearch;