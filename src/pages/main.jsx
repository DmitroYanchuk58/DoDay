import { useState } from "react";
import { Link } from "react-router-dom";

import Header from "./components/main/header";
import Sidebar from "./components/main/sidebar";

const Main = ({}) => {
  return (
    <div className="main-page">
      <Header />
      <Sidebar />
    </div>
  );
};

export default Main;
