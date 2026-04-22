import React, { useState, useEffect, useRef } from "react";
import { TaskService } from "../../../apiClient/TaskService";
import ImageUpload from "../image-upload";

const TaskOverlay = ({ isOpen, onClose, user, task, onSave }) => {
  const fileInputRef = useRef(null);
  const isEditMode = !!task;

  const [formData, setFormData] = useState({
    name: "",
    description: "",
    priority: "Low",
    finishDate: new Date().toISOString().split("T")[0],
    image: null,
    status: "NotStarted",
  });

  const [message, setMessage] = useState("");

  useEffect(() => {
    if (isOpen) {
      if (isEditMode) {
        const rawDate = task.finishDate || task.FinishDate || "";
        setFormData({
          id: task.id || task.Id,
          name: task.name || task.Name || "",
          description: task.description || task.Description || "",
          priority: task.priority || task.Priority || "Low",
          finishDate: rawDate.includes("T") ? rawDate.split("T")[0] : rawDate,
          image: task.image || task.Image || null,
          status: task.status || task.Status || "NotStarted",
        });
      } else {
        setFormData({
          name: "",
          description: "",
          priority: "Low",
          finishDate: new Date().toISOString().split("T")[0],
          image: null,
          status: "NotStarted",
        });
      }
      setMessage("");
    }
  }, [isOpen, task, isEditMode]);

  if (!isOpen) return null;

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleFileSelect = (file) => {
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () =>
        setFormData((prev) => ({ ...prev, image: reader.result }));
      reader.readAsDataURL(file);
    }
  };

  const validate = () => {
    if (!formData.name) {
      setMessage("Title field should be filled");
      return false;
    }
    if (!formData.description) {
      setMessage("Description field should be filled");
      return false;
    }
    return true;
  };

  const handleSubmit = async (e) => {
    if (e) e.preventDefault();
    if (!validate()) return;

    let cleanImage = formData.image;
    if (cleanImage && cleanImage.includes("base64,")) {
      cleanImage = cleanImage.split("base64,")[1];
    }

    try {
      if (isEditMode) {
        await TaskService.updateTask({ ...formData, image: cleanImage });
      } else {
        await TaskService.createTask(
          user.id,
          formData.name,
          formData.description,
          formData.priority,
          formData.finishDate,
          cleanImage,
        );
      }

      if (onSave) await onSave();
      onClose();
    } catch (err) {
      console.error("Submission error:", err);
      setMessage("An error occurred. Please try again.");
    }
  };

  return (
    <div className="edit-overlay">
      <div className="edit-modal-container">
        <div className="edit-modal-header">
          <h2 className="edit-modal-title">
            {isEditMode ? "Edit Task" : "Create Task"}
          </h2>
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
                onChange={handleInputChange}
              />
            </div>

            <div className="edit-input-group">
              <label>Date</label>
              <input
                type="date"
                name="finishDate"
                value={formData.finishDate}
                onChange={handleInputChange}
                className="date-input"
              />
            </div>

            <div className="edit-input-group">
              <label>Priority</label>
              <div className="priority-radio-group">
                {["Low", "Medium", "High", "Urgent"].map((p) => (
                  <label key={p} className="priority-option">
                    <span className={`dot ${p.toLowerCase()}`}></span>
                    <input
                      type="radio"
                      name="priority"
                      value={p}
                      checked={formData.priority === p}
                      onChange={handleInputChange}
                    />
                    {p}
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
                onChange={handleInputChange}
              />
            </div>

            {message && <p className="message">{message}</p>}
          </div>

          <ImageUpload
            image={formData.image}
            fileInputRef={fileInputRef}
            onFileSelect={handleFileSelect}
          />
        </div>

        <div className="edit-modal-footer">
          <button className="btn-done" onClick={handleSubmit}>
            Done
          </button>
        </div>
      </div>
    </div>
  );
};

export default TaskOverlay;
