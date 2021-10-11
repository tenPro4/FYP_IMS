import React from "react";
import ReactDOM from "react-dom";
import "scss/volt.scss";
import "@fortawesome/fontawesome-free/css/all.css";
import "react-datetime/css/react-datetime.css";
import ScrollToTop from "components/ScrollToTop";
import HomePage from "pages/HomePage";
import { Provider } from "react-redux";
import store from "store";
import Toast from "components/Toast";

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <Toast />
      <HomePage />
    </Provider>
  </React.StrictMode>,
  document.getElementById("root")
);
