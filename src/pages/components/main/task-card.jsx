import React from "react";

const TaskCard = ({
  title = "",
  description = "",
  priority = "",
  status = "",
  date = "",
  image,
  type = "full",
}) => {
  const statusClass = status ? status.toLowerCase().replace(/\s+/g, "-") : "";
  const priorityClass = priority ? priority.toLowerCase() : "";

  return (
    <div className={`task-card ${type === "compact" ? "compact-card" : ""}`}>
      <div className="task-header">
        <div className="task-title-row">
          {status && <span className={`status-dot ${statusClass}`}></span>}
          <h3 className="task-title">{title}</h3>
        </div>
        <div className="more-options">
          <svg className="icon icon-three-dots">
            <use xlinkHref="images/icons/dashboard-icons.svg#icon-three-dots"></use>
          </svg>
        </div>
      </div>

      <div className="task-content">
        <div className="task-text-info">
          <p className="task-description">{description}</p>
        </div>

        {image && (
          <div className="task-image-container">
            <img src={image} alt="task visual" className="task-card-img" />
          </div>
        )}
      </div>

      <div className="task-footer">
        {priority && (
          <div className="meta-item">
            <span className="meta-label">Priority:</span>
            <span className={`meta-value priority-${priorityClass}`}>
              {priority}
            </span>
          </div>
        )}

        {status && (
          <div className="meta-item">
            <span className="meta-label">Status:</span>
            <span className={`meta-value status-text-${statusClass}`}>
              {status}
            </span>
          </div>
        )}

        {date && <div className="meta-date">Created on: {date}</div>}
      </div>
    </div>
  );
};

export default TaskCard;
