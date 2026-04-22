import React, { useState } from "react";
import taskDefaultImage from "../../images/task-default.png";

const TaskCard = ({
  task,
  isVitalPage = false,
  onVital,
  onFinish,
  onDelete,
  openEditTask,
}) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const { id, name, description, priority, status, dateCreated, image } = task;

  const statusClass = status?.toLowerCase().replace(/\s+/g, "-") || "";
  const priorityClass = priority?.toLowerCase() || "";
  const displayDate = dateCreated?.split("T")[0] ?? "No Date";

  const defaultImage = taskDefaultImage;

  return (
    <div className="task-card-container">
      <div className="task-card-main">
        <div className="task-card-header">
          <span className={`status-indicator-dot ${statusClass}`}></span>

          <div className="options-wrapper">
            <button
              className="three-dots-trigger"
              onClick={(e) => {
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
                onClick={(e) => e.stopPropagation()}
              >
                <div
                  className="action-item"
                  onClick={(e) => {
                    onVital(task);
                    setIsMenuOpen(false);
                  }}
                >
                  {isVitalPage ? "Remove from Vital" : "Vital"}
                </div>
                <div
                  className="action-item"
                  onClick={(e) => {
                    openEditTask(e, task);
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
                {onFinish && (
                  <div
                    className="action-item finish-item"
                    onClick={(e) => {
                      onFinish(task);
                      setIsMenuOpen(false);
                    }}
                  >
                    Finish
                  </div>
                )}
              </div>
            )}
          </div>
        </div>

        <div className="task-card-body">
          <div className="task-text-content">
            <h3 className="task-main-title">{name ?? "Untitled Task"}</h3>
            <p className="task-main-description">
              {description ?? "No description"}
            </p>
          </div>

          <div className="task-thumbnail-wrapper">
            <img
              src={
                image
                  ? image.startsWith("data:image")
                    ? image
                    : `data:image/png;base64,${image}`
                  : defaultImage
              }
              alt="task preview"
              className="task-card-image"
              onError={(e) => {
                e.target.src = defaultImage;
              }}
            />
          </div>
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

          <div className="meta-date-stamp">Created on: {displayDate}</div>
        </div>
      </div>
    </div>
  );
};

export default TaskCard;
