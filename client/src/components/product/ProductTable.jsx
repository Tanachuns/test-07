import React, {useEffect} from 'react';
import axios from "axios";
import Modal from "../shared/Modal.jsx";
import ConfirmModal from "../shared/ConfirmModal.jsx";
import {QRCodeCanvas} from "qrcode.react";

const ProductTable = () => {

    const [products, setProducts] = React.useState([]);
    const [qrModalOpen, setQrModalOpen] = React.useState(false);
    const [deleteModalOpen, setDeleteModalOpen] = React.useState(false);
    const [targetProduct, setTargetProduct] = React.useState(null);
    
    
    const getsProduct = ()=>{
        axios.get(import.meta.env.VITE_API_URL+'/product').then(res => {
            //modal success
            setProducts(res.data.data);
        }).catch(err => {
            //modal err
            console.log(err);
        })
        
        // setProducts([
        //     {
        //         productCode: "TTTTT-TTTTT-TTTTT-TTTTT-TTTTT-TTTTT"
        //     }
        // ])
    }

    const deleteProduct = (productCode)=>{
        axios.delete(import.meta.env.VITE_API_URL+'/product',{data:{
                productCode:targetProduct
            }}).then(res => {
            //modal success
            console.log(res);
            location.reload();
        }).catch(err => {
            //modal err
            console.log(err.response.data);
        })
    }
    
    useEffect(getsProduct,[qrModalOpen,deleteModalOpen])
    
    
    return (
        <>
            <div className="overflow-x-auto mt-5 rounded-box border border-base-content/5 bg-base-100">
                <table className="table m-3">
                    {/* head */}
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>รหัสสินค้า</th>
                        <th>View</th>
                        <th>Delete</th>
                    </tr>
                    </thead>
                    <tbody>
                    {/* row 1 */}
                    {products.map((item, index) => (
                            <tr key={index}>
                                <th>{item.id}</th>
                                <td>{item.productcode}</td>
                                <td><button onClick={()=> {
                                    setQrModalOpen(true);
                                    setTargetProduct(item.productcode);
                                }} className="btn btn-sm btn-info">QR</button></td>
                                <td><button onClick={()=> {
                                    setDeleteModalOpen(true);
                                    setTargetProduct(item.productcode);
                                }} className="btn btn-sm btn-error">ลบ</button></td>
                            
                            </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            <ConfirmModal open={deleteModalOpen} title={"ยืนยันการลบข้อมูล"} onConfirm={deleteProduct} onClose={()=>setDeleteModalOpen(false)} >
               <>
                   ต้องการลบข้อมูล รหัสสินค้า <br/>{targetProduct}<br/>หรือไม่?
               </>
            </ConfirmModal>
            <Modal open={qrModalOpen} title={"View Qrcode"}    onClose={()=>setQrModalOpen(false)} >
                <QRCodeCanvas value={targetProduct} />
            </Modal>
            
        </>
    );
};

export default ProductTable;