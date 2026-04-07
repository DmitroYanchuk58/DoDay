import React, { useState } from "react";

const CreateCategory = ({ onCreate, onCancel, refreshTasks }) => {
  const [value, setValue] = React.useState("");

  return (
    <div className="task-categories-page">
      <div className="task-border">
        <div className="categories-card">
          <div className="categories-main-header">
            <h2>Create Categories</h2>
            <button className="go-back-link" onClick={onCancel}>
              Go Back
            </button>
          </div>

          <form className="create-form">
            <div className="form-group">
              <label htmlFor="categoryName" className="input-label">
                Category Name
              </label>
              <input
                type="text"
                id="categoryName"
                className="modal-input"
                value={value}
                onChange={(e) => setValue(e.target.value)}
                autoFocus
              />
            </div>

            <div className="modal-actions">
              <button
                className="btn-modal btn-create"
                onClick={() => onCreate(value)}
              >
                Create
              </button>
              <button
                type="button"
                className="btn-modal btn-cancel"
                onClick={onCancel}
              >
                Cancel
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};
export default CreateCategory;
