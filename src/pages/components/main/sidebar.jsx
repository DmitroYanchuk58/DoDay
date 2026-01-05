import React, { useState } from "react";

const Sidebar = () => {
  const [activeId, setActiveId] = useState(1);

  const menuItems = [
    { id: 1, label: "Dashboard", iconId: "icon-dashboard" },
    { id: 2, label: "Vital Task", iconId: "icon-vital-task" },
    { id: 3, label: "My Task", iconId: "icon-my-task" },
    { id: 4, label: "Task Categories", iconId: "icon-task-categories" },
    { id: 5, label: "Settings", iconId: "icon-settings" },
    { id: 6, label: "Help", iconId: "icon-help" },
  ];

  return (
    <aside className="sidebar">
      <div className="sidebar-profile">
        <div className="profile-avatar">
          <img src="images/profile_image.jpg" alt="Sundar Gurung" />
        </div>
        <h3 className="profile-name">Sundar Gurung</h3>
        <p className="profile-email">sundargurung360@gmail.com</p>
      </div>

      <nav className="sidebar-nav">
        {menuItems.map((item) => (
          <div
            key={item.id}
            className={`nav-item ${item.id === activeId ? "active" : ""}`}
            onClick={() => setActiveId(item.id)}
          >
            <svg className={`nav-icon ${item.iconId}`}>
              <use
                xlinkHref={`images/icons/sidebar-icons.svg#${item.iconId}`}
              ></use>
            </svg>
            <span>{item.label}</span>
          </div>
        ))}
      </nav>

      <div className="sidebar-logout">
        <div className="nav-item">
          <svg className={`nav-icon`}>
            <use xlinkHref={`images/icons/sidebar-icons.svg#icon-logout`}></use>
          </svg>
          <span>Logout</span>
        </div>
      </div>
    </aside>
  );
};

export default Sidebar;
