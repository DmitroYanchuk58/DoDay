import React, { useState } from "react";

const CalendarOverlay = ({ isOpen, onClose }) => {
  const today = new Date();
  const [currentDate, setCurrentDate] = useState(new Date());

  // Встановлюємо сьогоднішню дату як вибрану за дефолтом
  const [selectedDate, setSelectedDate] = useState(today);

  if (!isOpen) return null;

  const daysOfWeek = ["MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN"];
  const year = currentDate.getFullYear();
  const month = currentDate.getMonth();
  const monthName = currentDate.toLocaleString("en-US", { month: "long" });

  const firstDayOfMonth = new Date(year, month, 1).getDay();
  const offset = firstDayOfMonth === 0 ? 6 : firstDayOfMonth - 1;
  const daysInMonth = new Date(year, month + 1, 0).getDate();

  const dates = [];
  for (let i = 0; i < offset; i++) dates.push("");
  for (let d = 1; d <= daysInMonth; d++) dates.push(d);

  const changeMonth = (offset) => {
    setCurrentDate(new Date(year, month + offset, 1));
  };

  const handleDateClick = (day) => {
    if (day === "") return;
    const newSelectedDate = new Date(year, month, day);
    setSelectedDate(newSelectedDate);
  };

  const formatDate = (date) => {
    return date.toLocaleString("en-US", {
      month: "long",
      day: "numeric",
      year: "numeric",
    });
  };

  const isSelected = (day) => {
    return (
      day !== "" &&
      selectedDate.getDate() === day &&
      selectedDate.getMonth() === month &&
      selectedDate.getFullYear() === year
    );
  };

  return (
    <div className="calendar-modal-overlay">
      <div className="calendar-container">
        <div className="calendar-header">
          <h2 className="calendar-title">Calendar</h2>
          <button className="back-arrow-btn" onClick={onClose}>
            <img src="images/icons/calendar-back.svg" alt="back" />
          </button>
        </div>

        <div className="selected-date-section">
          <div className="selected-date-box">
            <span className="date-text">{formatDate(selectedDate)}</span>
            <div className="clear-date-group">
              <button
                className="clear-btn"
                onClick={() => setSelectedDate(today)}
              >
                X
              </button>
            </div>
          </div>
        </div>

        <div className="calendar-nav-row">
          <button className="nav-btn" onClick={() => changeMonth(-1)}>
            <img src="images/icons/left.svg" alt="prev" />
          </button>
          <div className="current-month-display">
            {monthName} {year}
          </div>
          <button className="nav-btn" onClick={() => changeMonth(1)}>
            <img src="images/icons/right.svg" alt="next" />
          </button>
        </div>

        <div className="days-grid-header">
          {daysOfWeek.map((day) => (
            <span key={day}>{day}</span>
          ))}
        </div>

        <div className="calendar-grid">
          {dates.map((date, index) => (
            <div
              key={index}
              onClick={() => handleDateClick(date)}
              className={`calendar-date 
                ${isSelected(date) ? "active-date" : ""} 
                ${date === "" ? "empty" : ""}`}
            >
              {date}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default CalendarOverlay;
