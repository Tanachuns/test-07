import React, {useEffect, useRef} from 'react';


const Modal = ({ open, title, children, onClose }) => {
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
             <div className="p-4 w-fit m-auto">
                    {children}
             </div>

             {/* Footer */}
             <div className="modal-action">
                 <button className="btn" onClick={onClose}>
                     ปิด
                 </button>
             </div>

         </div>
     </dialog>
     </div>
        
    );
};

export default Modal;