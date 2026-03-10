import React, {useEffect, useRef, useState} from 'react';


const ConfirmModal = ({ open, title, children, onClose,onConfirm }) => {
    const dialogRef = useRef(null);
    const [isSending, setSending] = useState(false);

    useEffect(() => {
        if (open) {
            dialogRef.current.showModal();
        } else {
            dialogRef.current.close();
        }
    }, [open]);
    
    const sendHandler = async () => {
        setSending(true);
        await onConfirm();
        setSending(false);
    }
    
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
                 <button className="btn btn-success" disabled={isSending} onClick={sendHandler}>
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