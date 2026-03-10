import React, {useRef, useState} from 'react';
import axios from "axios";
import Modal from "../shared/Modal.jsx";

const ProductForm = () => {
    const dialogRef = useRef(null);
    const [openCreateResultModal, setOpenCreateResultModal] = useState(false);
    const [createStatus,setStatus] = useState({});
    
    const submitHandler = (e) => {
        e.preventDefault();
        const _productCode = e.target.productCode.value;
        if(!productCodeValidator(_productCode)) {
            //modal err
            console
                .log("Error getting product code")
            return; 
        }
        createProduct(_productCode);
    }
    
    const createProduct = (productCode)=>{
        axios.post(import.meta.env.VITE_API_URL+'/product', {
            productCode:productCode
        }).then(res => {
            console.log(res.data);
            //setStatus(res.data);
            setStatus(res.data);
            setOpenCreateResultModal(true);

        }).catch(err => {
            if (err.response) {
                // Server responded with error status (4xx, 5xx)
                console.log(err.response.data);
                setStatus(err.response.data);
                setOpenCreateResultModal(true);
            }
            
        });
    }

   
    const productCodeValidator = (code)=>{
        const regex = /^[A-Z0-9]{5}(-[A-Z0-9]{5}){5}$/;
        return code!=null &&  regex.test(code);
    }
    
    const UpperHandler = (e) => {
        e.preventDefault();
        e.target.value = e.target.value.toUpperCase();
    }
    
    const closeHandler = ()=>{ 
        setOpenCreateResultModal(false); 
        if(createStatus.isSuccess){
            location.reload();
        }
    }
    
    return (
        <>
            <form onSubmit={submitHandler} className="w-full my-3">
                <input maxLength={35} minLength={35} type="search" name="productCode" className="grow input validator w-4/5" onChange={e=>UpperHandler(e)} pattern={"^[A-Z0-9]{5}(-[A-Z0-9]{5}){5}$"} placeholder="รหัสสินค้า"/>
                <button className="btn btn-primary ml-2">ADD</button>
                <p className="validator-hint">
                    รหัสสินค้าต้องมีความยาว 30 ตัวอักษร
                    <br/>เป็นตัวอักษรภาษาอังกฤษหรือคัวเลข
                    <br/>มีขีด(-) ขั้นทุกๆ 5 ตัวอักษร
                    <br/>ตัวอย่าง XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX
                </p>
            </form>
            <Modal open={openCreateResultModal} onClose={closeHandler} title={createStatus.isSuccess?"สร้างรายการสำเร็จ":"สร้างรายการผิดพลาด"}>
                {createStatus.isSuccess?"สร้างรายการสำเร็จ":createStatus.message}
            </Modal>
        </>
        
    );
};

export default ProductForm;