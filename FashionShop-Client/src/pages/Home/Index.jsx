import React from 'react';
import ProductList from "../../components/Product/ProductList";
import AutoBanner from '../../components/AutoBanner/AutoBanner';
import CardCategory from '../../components/Card/CardCategory';
const Home = () => {
    return (
    <body> 
    <AutoBanner />
    <CardCategory/>
    <ProductList />
    </body>
    );
};
export default Home;