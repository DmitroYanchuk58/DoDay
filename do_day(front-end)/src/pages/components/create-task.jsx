import { useState } from "react";
import { TaskService } from "../../apiClient/TaskService";

const CreateTaskOverlay = ({ isOpen, onClose, user, onTaskCreated }) => {
  const [name, setName] = useState();
  const [description, setDescription] = useState("");
  const [message, setMessage] = useState("");

  if (!isOpen) return null;

  const checkValues = () => {
    if (!name) {
      setMessage("Title field should be filled");
      return false;
    }
    if (!description) {
      setMessage("Description field should be filled");
      return false;
    }
    setMessage("");
    return true;
  };

  const createTask = async (e) => {
    if (e) e.preventDefault();

    if (!checkValues()) {
      return;
    }

    try {
      const result = await TaskService.createTask(user.id, name, description);
      if (onTaskCreated) {
        await onTaskCreated();
      }
      setName("");
      setDescription("");
      onClose();
    } catch (err) {
      const errorDetail =
        err.response?.data?.detail || err.message || "Undefited error";
    }
  };

  return (
    <div className="edit-overlay">
      <div className="edit-modal-container">
        <div className="edit-modal-header">
          <div className="edit-title-group">
            <h2 className="edit-modal-title">Create Task</h2>
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
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </div>

            <div className="edit-input-group">
              <label>Date</label>
              <div className="date-input-wrapper">
                <input className="date-input" type="text" name="date" />
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
                  <input className="priority-box" type="radio" />
                </label>
                <label className="priority-option">
                  <span className="dot moderate"></span>
                  <span className="priority-text">Moderate</span>
                  <input className="priority-box" type="radio" />
                </label>
                <label className="priority-option">
                  <span className="dot low"></span>
                  <span className="priority-text">Low</span>
                  <input className="priority-box" type="radio" />
                </label>
              </div>
            </div>

            <div className="edit-input-group">
              <label>Task Description</label>
              <textarea
                placeholder="Start writing here..."
                value={description}
                onChange={(e) => setDescription(e.target.value)}
              ></textarea>
              {message && <p className="message">{message}</p>}
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
          <button className="btn-done" onClick={(e) => createTask()}>
            Done
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateTaskOverlay;
