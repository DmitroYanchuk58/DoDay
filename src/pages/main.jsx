import { useState } from "react";
import { Link } from "react-router-dom";

import Header from "./components/main/header";
import Sidebar from "./components/main/sidebar";
import DashboardContent from "./components/main/dashboard-content";

const Main = ({}) => {
  return (
    <div className="main-page">
      <Header />
      <div className="main-content">
        <Sidebar />
        <DashboardContent />
      </div>
    </div>
  );
};

export default Main;
