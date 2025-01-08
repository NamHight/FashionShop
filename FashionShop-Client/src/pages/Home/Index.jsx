import React from 'react';
import ProductList from "../../components/Product/ProductList";
import AutoBanner from '../../components/AutoBanner/AutoBanner';
import CardCategory from '../../components/Card/CardCategory';
const Home = () => {
    return (
    <div>
    <AutoBanner />
    <CardCategory/>
    <ProductList />
    </div>
    );
};
export default Home;