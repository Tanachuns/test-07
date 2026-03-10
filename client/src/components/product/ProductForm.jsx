import React from 'react';
import axios from "axios";

const ProductForm = () => {
    
    const submitHandler = (e) => {
        e.preventDefault();
        const _productCode = e.target.productCode.value;
        if(!productCodeValidator(productCode)) {
            //modal err
            return; 
        }
        createProduct(_productCode);
    }
    
    const createProduct = (productCode)=>{
        axios.delete('/product', {
            productCode:productCode
        }).then(res => {
            //modal success
            console.log(res);
        }).catch(err => {
            //modal err
            console.log(err);
        })
    }
    
    const productCodeValidator = (code)=>{
        const regex = /^[A-Z0-9]{5}(-[A-Z0-9]{5}){5}$/;
        return code!=null &&  regex.test(code);
    }
    
    return (
        <form onSubmit={submitHandler}>
            <div className='productForm my-2'>
                <label className="input w-3/4">
                    <svg className="h-[1em] opacity-50" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                        <g
                            strokeLinejoin="round"
                            strokeLinecap="round"
                            strokeWidth="2.5"
                            fill="none"
                            stroke="currentColor"
                        >
                            <circle cx="11" cy="11" r="8"></circle>
                            <path d="m21 21-4.3-4.3"></path>
                        </g>
                    </svg>
                    <input type="search" name="productCode" className="grow" placeholder="Format XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX" />
                </label>
                <button className="btn btn-primary ml-2">ADD</button>
            </div>
        </form>
        
    );
};

export default ProductForm;