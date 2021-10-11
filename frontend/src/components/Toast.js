import React from "react";

import "react-toastify/dist/ReactToastify.css";
import { ToastContainer, cssTransition } from "react-toastify";
import "animate.css/animate.min.css";

const Toast = () => {
  const bounce = cssTransition({
    enter: "animate__animated animate__bounceIn",
    exit: "animate__animated animate__bounceOut",
  });

  return (
    <div>
      <ToastContainer
        className="toast-container"
        toastClassName="modified-toast"
        position="top-center"
        autoClose={6000}
      />
    </div>
  );
};

export default Toast;
