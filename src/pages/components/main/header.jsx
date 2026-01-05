import React from "react";

const Header = () => {
  return (
    <header className="main-header">
      <div className="header-logo">
        <span className="accent-text">Dash</span>board
      </div>

      <div className="header-search">
        <input
          type="text"
          placeholder="Search your task here..."
          className="search-field"
        />
        <button className="search-submit">
          <img
            src="images/icons/search.svg"
            alt="search"
            width="20"
            height="20"
          />
        </button>
      </div>

      <div className="header-actions">
        <button className="action-icon">
          <img
            src="images/icons/notification.svg"
            alt="notification"
            width="20"
            height="20"
          />
        </button>
        <button className="action-icon">
          <img
            src="images/icons/calendar.svg"
            alt="calendar"
            width="20"
            height="20"
          />
        </button>

        <div className="header-date">
          <span className="date-day">Tuesday</span>
          <span className="date-full">20/06/2023</span>
        </div>
      </div>
    </header>
  );
};

export default Header;
