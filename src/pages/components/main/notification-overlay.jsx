import React from "react";

const NotificationsOverlay = ({ isOpen, onClose }) => {
  if (!isOpen) return null;

  const notifications = [
    {
      id: 1,
      text: "Complete the UI design of Landing Page for FoodVentures.",
      time: "2h",
      priority: "High",
      image: "images/task_image1.png",
    },
    {
      id: 2,
      text: "Complete the UI design of Landing Page for Travel Days.",
      time: "2h",
      priority: "High",
      image: "images/task_image1.png",
    },
    {
      id: 3,
      text: "Complete the Mobile app design for Pet Warden.",
      time: "2h",
      priority: "Extremely High",
      image: "images/task_image1.png",
    },
    {
      id: 4,
      text: "Complete the entire design for Juice Slider.",
      time: "2h",
      priority: "High",
      image: "images/task_image1.png",
    },
    {
      id: 5,
      text: "Complete the entire design for Juice Slider.",
      time: "2h",
      priority: "High",
      image: "images/task_image1.png",
    },
    {
      id: 6,
      text: "Complete the entire design for Juice Slider.",
      time: "2h",
      priority: "High",
      image: "images/task_image1.png",
    },
    {
      id: 7,
      text: "Complete the entire design for Juice Slider.",
      time: "2h",
      priority: "High",
      image: "images/task_image1.png",
    },
    {
      id: 8,
      text: "Complete the entire design for Juice Slider.",
      time: "2h",
      priority: "High",
      image: "images/task_image1.png",
    },
  ];

  return (
    <div className="notification-modal-overlay">
      <div className="notification-container">
        {/* Header */}
        <div className="notification-header">
          <h2 className="notification-title">Notifications</h2>
          <button className="back-btn" onClick={onClose}>
            <span className="back-icon-arrow">
              <img src="images/icons/calendar-back.svg" alt="back" />
            </span>
          </button>
        </div>

        <div className="notification-date-label">Today</div>

        {/* Scrollable List */}
        <div className="notification-list">
          {notifications.map((item) => (
            <div key={item.id} className="notification-item">
              <div className="notification-content">
                <p className="notification-text">
                  {item.text} <span className="timestamp">{item.time}</span>
                </p>
                <p className="priority-label">
                  Priority:{" "}
                  <span
                    className={`priority-value ${item.priority.toLowerCase().replace(" ", "-")}`}
                  >
                    {item.priority}
                  </span>
                </p>
              </div>
              <img
                src={item.image}
                alt="task preview"
                className="task-thumbnail"
              />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default NotificationsOverlay;
