import React, { useState, useEffect } from "react";

const CategoryModal = ({
  isOpen,
  onClose,
  onSubmit,
  titlePrefix = "Add",
  titleMain = "Category",
  initialValue = "",
  submitLabel = "Create",
  inputLabel = "Category Name",
}) => {
  const [inputValue, setInputValue] = useState(initialValue);

  useEffect(() => {
    setInputValue(initialValue);
  }, [initialValue]);

  if (!isOpen) return null;

  const handleFormSubmit = (e) => {
    e.preventDefault();
    onSubmit(inputValue);
    setInputValue("");
  };

  return (
    <div className="modal-overlay">
      <div className="modal-container card">
        <div className="modal-header">
          <h3 className="category-title-text">
            {titlePrefix} {titleMain}
          </h3>
          <button className="go-back-link" onClick={onClose}>
            Go Back
          </button>
        </div>

        <div className="modal-body-content">
          <form onSubmit={handleFormSubmit}>
            <div className="form-group">
              <label>{inputLabel}</label>

              <div className="form-group-input">
                <input
                  type="text"
                  id="modalInput"
                  value={inputValue}
                  onChange={(e) => setInputValue(e.target.value)}
                  autoFocus
                  required
                />
              </div>
            </div>

            <div className="modal-footer-actions">
              <button type="submit" className="btn">
                {submitLabel}
              </button>
              <button type="button" className="btn" onClick={onClose}>
                Cancel
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default CategoryModal;
