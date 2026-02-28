import React, { useState } from "react";

const EditTaskOverlay = ({ isOpen, task, onClose, onSave }) => {
  const [formData, setFormData] = useState({
    title: task?.title || "",
    date: task?.date || "",
    priority: task?.priority || "Moderate",
    description: task?.description || "",
  });

  if (!isOpen) return null;

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handlePriorityChange = (priority) => {
    setFormData((prev) => ({ ...prev, priority }));
  };

  return (
    <div className="edit-overlay">
      <div className="edit-modal-container">
        <div className="edit-modal-header">
          <div className="edit-title-group">
            <h2 className="edit-modal-title">Edit Task</h2>
          </div>
          <button className="edit-go-back" onClick={onClose}>
            Go Back
          </button>
        </div>

        <div className="edit-modal-body">
          <div className="edit-form-left">
            <div className="edit-input-group">
              <label>Title</label>
              <input
                type="text"
                name="title"
                value={formData.title}
                onChange={handleChange}
              />
            </div>

            <div className="edit-input-group">
              <label>Date</label>
              <div className="date-input-wrapper">
                <input
                  className="date-input"
                  type="text"
                  name="date"
                  value={formData.date}
                  onChange={handleChange}
                />
                <img
                  src="images/icons/calendar-simple.svg"
                  alt="calendar"
                  className="date-icon"
                />
              </div>
            </div>

            <div className="edit-input-group">
              <label>Priority</label>
              <div className="priority-radio-group">
                <label className="priority-option">
                  <span className="dot extreme"></span>
                  <span className="priority-text">Extreme</span>
                  <input
                    className="priority-box"
                    type="radio"
                    checked={formData.priority === "Extreme"}
                    onChange={() => handlePriorityChange("Extreme")}
                  />
                </label>
                <label className="priority-option">
                  <span className="dot moderate"></span>
                  <span className="priority-text">Moderate</span>
                  <input
                    className="priority-box"
                    type="radio"
                    checked={formData.priority === "Moderate"}
                    onChange={() => handlePriorityChange("Moderate")}
                  />
                </label>
                <label className="priority-option">
                  <span className="dot low"></span>
                  <span className="priority-text">Low</span>
                  <input
                    className="priority-box"
                    type="radio"
                    checked={formData.priority === "Low"}
                    onChange={() => handlePriorityChange("Low")}
                  />
                </label>
              </div>
            </div>

            <div className="edit-input-group">
              <label>Task Description</label>
              <textarea
                name="description"
                placeholder="Start writing here..."
                value={formData.description}
                onChange={handleChange}
              ></textarea>
            </div>
          </div>

          <div className="edit-form-right">
            <label className="upload-label">Upload Image</label>
            <div className="upload-dropzone">
              <div className="upload-icon-box">
                <img src="images/icons/upload-image.svg" alt="upload" />
              </div>
              <p>Drag&Drop files here</p>
              <span>or</span>
              <button className="btn-browse">Browse</button>
            </div>
          </div>
        </div>

        <div className="edit-modal-footer">
          <button className="btn-done" onClick={() => onSave(formData)}>
            Done
          </button>
        </div>
      </div>
    </div>
  );
};

export default EditTaskOverlay;
