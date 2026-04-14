import { useState, useEffect } from "react";
import { TaskService } from "../../apiClient/TaskService";

const EditTaskOverlay = ({ isOpen, task, onClose, onSave }) => {
  const [formData, setFormData] = useState({
    id: "",
    name: "",
    description: "",
    priority: "Low",
    date: "",
    dateCreated: "",
  });
  const [message, setMessage] = useState("");

  const checkValues = () => {
    if (!formData.name) {
      setMessage("Title field should be filled");
      return false;
    }
    if (!formData.description) {
      setMessage("Description field should be filled");
      return false;
    }
    setMessage("");
    return true;
  };

  useEffect(() => {
    if (task) {
      setFormData({
        id: task.id || task.Id,
        name: task.name || task.Name || "",
        description: task.description || task.Description || "",
        priority: task.priority || task.Priority || "Low",
        date: "",
        dateCreated: task.dateCreated || task.DateCreated || "",
      });
    }
  }, [task, isOpen]);

  if (!isOpen || !task) return null;

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handlePriorityChange = (priority) => {
    setFormData((prev) => ({ ...prev, priority }));
  };

  const handleUpdate = async (e) => {
    if (e) e.preventDefault();

    if (!checkValues()) {
      return;
    }

    try {
      await TaskService.updateTask(formData);

      if (onSave) {
        await onSave();
      }

      onClose();
    } catch (err) {
      console.error("Update error:", err);
    }
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
                name="name"
                value={formData.name}
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
                {["Extreme", "Moderate", "Low"].map((p) => (
                  <label key={p} className="priority-option">
                    <span className={`dot ${p.toLowerCase()}`}></span>
                    <span className="priority-text">{p}</span>
                    <input
                      className="priority-box"
                      type="radio"
                      checked={formData.priority === p}
                      onChange={() => handlePriorityChange(p)}
                    />
                  </label>
                ))}
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

            {message && <p className="message">{message}</p>}
          </div>

          <div className="edit-form-right">
            <label className="upload-label">Upload Image</label>
            <div className="upload-dropzone">
              <div className="upload-icon-box">
                <img src="images/icons/upload-image.svg" alt="upload" />
              </div>
              <p>Drag&Drop files here</p>
              <span>or</span>
              <button className="btn-browse" type="button">
                Browse
              </button>
            </div>
          </div>
        </div>

        <div className="edit-modal-footer">
          <button className="btn-done" onClick={handleUpdate}>
            Done
          </button>
        </div>
      </div>
    </div>
  );
};

export default EditTaskOverlay;
