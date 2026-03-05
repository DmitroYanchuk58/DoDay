import React, { useState } from "react";

const TaskCard = ({
  id,
  title,
  description,
  priority,
  status,
  date,
  image,
  isVitalPage = false,
  onDelete,
  openEditTask,
}) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const statusClass = status?.toLowerCase().replace(/\s+/g, "-") || "";
  const priorityClass = priority?.toLowerCase() || "";

  return (
    <div className="task-card-container">
      <div className="task-card-main">
        <div className="task-card-header">
          <span className={`status-indicator-dot ${statusClass}`}></span>

          <div className="options-wrapper">
            <button
              className="three-dots-trigger"
              onClick={(e) => {
                // ВИПРАВЛЕНО: зупиняємо спливання події, щоб не відкрилися деталі таски
                e.stopPropagation();
                setIsMenuOpen(!isMenuOpen);
              }}
            >
              <svg className="icon icon-three-dots">
                <use xlinkHref="images/icons/dashboard-icons.svg#icon-three-dots"></use>
              </svg>
            </button>

            {isMenuOpen && (
              <div
                className="actions-dropdown"
                // Зупиняємо кліки всередині меню, щоб вони не прокидалися до картки
                onClick={(e) => e.stopPropagation()}
              >
                <div
                  className="action-item"
                  onClick={() => setIsMenuOpen(false)}
                >
                  {isVitalPage ? "Remove from Vital" : "Vital"}
                </div>
                <div
                  className="action-item"
                  onClick={(e) => {
                    // Передаємо івент, щоб викликати stopPropagation всередині DashboardContent
                    openEditTask(e);
                    setIsMenuOpen(false);
                  }}
                >
                  Edit
                </div>
                <div
                  className="action-item"
                  onClick={() => {
                    onDelete(id);
                    setIsMenuOpen(false);
                  }}
                >
                  Delete
                </div>
                <div
                  className="action-item finish-item"
                  onClick={() => setIsMenuOpen(false)}
                >
                  Finish
                </div>
              </div>
            )}
          </div>
        </div>

        <div className="task-card-body">
          <div className="task-text-content">
            <h3 className="task-main-title">{title}</h3>
            <p className="task-main-description">{description}</p>
          </div>

          {image && (
            <div className="task-thumbnail-wrapper">
              <img src={image} alt="task preview" className="task-card-image" />
            </div>
          )}
        </div>

        <div className="task-card-footer">
          <div className="meta-group">
            <span className="meta-label">Priority:</span>
            <span className={`meta-value p-${priorityClass}`}>{priority}</span>
          </div>

          <div className="meta-group">
            <span className="meta-label">Status:</span>
            <span className={`meta-value s-${statusClass}`}>{status}</span>
          </div>

          <div className="meta-date-stamp">Created on: {date}</div>
        </div>
      </div>
    </div>
  );
};

export default TaskCard;
