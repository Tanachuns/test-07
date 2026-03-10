import React from 'react';
import ProductForm from "../components/product/ProductForm.jsx";
import ProductTable from "../components/product/ProductTable.jsx";

const Home = () => {
    return (
        <div className="container m-5">
            <h1>Products</h1>
           <ProductForm/>
            <ProductTable/>
        </div>
    );
};

export default Home;