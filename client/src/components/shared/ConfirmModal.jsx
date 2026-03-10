import React, {useEffect, useRef} from 'react';


const ConfirmModal = ({ open, title, children, onClose,onConfirm }) => {
    const dialogRef = useRef(null);

    useEffect(() => {
        if (open) {
            dialogRef.current.showModal();
        } else {
            dialogRef.current.close();
        }
    }, [open]);
    return (
     <div >
         <dialog ref={dialogRef} className="modal">
         <div className="modal-box">

             {/* Header */}
             <h3 className="font-bold text-lg">{title}</h3>

             {/* Body */}
             <div className="py-4">
                 {children}
             </div>

             {/* Footer */}
             <div className="modal-action">
                 <button className="btn btn-success" onClick={onConfirm}>
                     ตกลง
                 </button>
                 <button className="btn btn-error" onClick={onClose}>
                     ยกเลิก
                 </button>
             </div>

         </div>
     </dialog>
     </div>
        
    );
};

export default ConfirmModal;