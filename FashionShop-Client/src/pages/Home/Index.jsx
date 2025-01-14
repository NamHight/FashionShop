import React from 'react';
import ProductList from "../../components/List/ProductList";
import AutoBanner from '../../components/AutoBanner/AutoBanner';
import CategoriesList from '../../components/List/CategoryList';
const Home = () => {
    return (
    <div className='w-full'>
    <AutoBanner />
    <h1 className="text-3xl font-bold mb-6">Categories List</h1>
    <CategoriesList/>
    <ProductList />
    </div>
    );
};
export default Home;