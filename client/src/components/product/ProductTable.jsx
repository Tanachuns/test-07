import React, {useEffect} from 'react';
import axios from "axios";
import Modal from "../shared/Modal.jsx";
import ConfirmModal from "../shared/ConfirmModal.jsx";

const ProductTable = () => {

    const [products, setProducts] = React.useState([]);
    const [qrModalOpen, setQrModalOpen] = React.useState(false);
    const [deleteModalOpen, setDeleteModalOpen] = React.useState(false);
    
    
    const getsProduct = ()=>{
        // axios.get('/product', {
        //     productCode:productCode
        // }).then(res => {
        //     //modal success
        //     console.log(res);
        //     setProducts(res.data.data);
        // }).catch(err => {
        //     //modal err
        //     console.log(err);
        // })
        
        setProducts([
            {
                productCode: "TTTTT-TTTTT-TTTTT-TTTTT-TTTTT-TTTTT"
            }
        ])
    }
    
    


    useEffect(getsProduct,[qrModalOpen,deleteModalOpen])
    
    
    return (
        <>
            <div className="overflow-x-auto rounded-box border border-base-content/5 bg-base-100">
                <table className="table">
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
                            <th>{index+1}</th>
                            <td>{item.productCode}</td>
                            <td><button onClick={()=>setQrModalOpen(true)} className="btn btn-sm btn-info">View</button></td>
                            <td><button onClick={()=>setDeleteModalOpen(true)} className="btn btn-sm btn-error">Delete</button></td>
                            <ConfirmModal open={deleteModalOpen} title={"ยืนยันการลบข้อมูล"} children={`ต้องการลบข้อมูล รหัสสินค้า ${item.productCode} หรือไม่?`} onConfirm={()=>{alert("nice"); setDeleteModalOpen(false)}} onClose={()=>setDeleteModalOpen(false)} />
                            <Modal open={qrModalOpen} title={"View Qrcode"}  onClose={()=>setQrModalOpen(false)} />
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            
        </>
    );
};

export default ProductTable;