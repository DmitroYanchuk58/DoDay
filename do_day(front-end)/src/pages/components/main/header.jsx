import React, { useState } from "react";
import CalendarOverlay from "./calendar-overlay";
import NotificationOverlay from "./notification-overlay";

const Header = () => {
  // Стан для відображення календаря
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [isNotificationOpen, setIsNotificationOpen] = useState(false);

  const toggleCalendar = () => {
    setIsCalendarOpen(!isCalendarOpen);
  };

  const toggleNotification = () => {
    setIsNotificationOpen(!isNotificationOpen);
  };

  return (
    <header className="main-header">
      <div className="header-logo">
        <span className="accent-text">Do</span>Day
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
        <button className="action-icon" onClick={toggleNotification}>
          <img
            src="images/icons/notification.svg"
            alt="notification"
            width="20"
            height="20"
          />
        </button>
        <button className="action-icon" onClick={toggleCalendar}>
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

      <CalendarOverlay
        isOpen={isCalendarOpen}
        onClose={() => setIsCalendarOpen(false)}
      />

      <NotificationOverlay
        isOpen={isNotificationOpen}
        onClose={() => setIsNotificationOpen(false)}
      />
    </header>
  );
};

export default Header;
