import React, { useState } from 'react';
import { useNavigate } from 'react-router';
import { getAllProducts } from '../../../services/api/ProductService';

const ReviewButton = ({ productId }) => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleReviewClick = async () => {
    setLoading(true);
  
    try {
      const allProducts = await getAllProducts();
      console.log("All Products:", allProducts);
  
      if (!Array.isArray(allProducts) || allProducts.length === 0) {
        console.error("No products found");
        setLoading(false);
        return;
      }
      const productMap = allProducts.reduce((acc, item) => {
        if (item && item.productId) {
          acc[item.productId] = item;
        }
        return acc;
      }, {});
  
      console.log('Product Map:', productMap);
      const product = productMap[productId];
      if (product) {
        const categorySlug = product?.category?.slug;
        const productSlug = product?.slug;
  
        if (categorySlug && productSlug) {
          navigate(`/${categorySlug}/${productSlug}`);
        } else {
          console.error('Category slug or Product slug is undefined');
        }
      } else {
        console.error('Product not found');
      }
    } catch (error) {
      console.error('Error fetching products:', error);
    } finally {
      setLoading(false); 
    }
  };
  

  return (
    <button
      onClick={handleReviewClick}
      className="border border-slate-500 px-9 py-3 rounded hover:bg-red-600 hover:text-white"
      disabled={loading}
    >
      {loading ? 'Loading...' : 'Reviews'}
    </button>
  );
};

export default ReviewButton;
