import { useState, useEffect } from "react";

const CategoryModal = ({ config, onSubmit, onCancel }) => {
  const [value, setValue] = useState(config?.initialValue || "");
  const [message, setMessage] = useState("");

  const checkValues = () => {
    if (!value) {
      setMessage("Title field should be filled");
      return false;
    }
    setMessage("");
    return true;
  };

  useEffect(() => {
    setValue(config?.initialValue || "");
  }, [config?.initialValue]);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!checkValues()) {
      return;
    }
    if (value.trim()) {
      onSubmit(value);
      if (!config.initialValue) setValue("");
    }
  };

  return (
    <div className="task-categories-page">
      <div className="task-border">
        <div className="categories-card">
          <div className="categories-main-header">
            <h2>
              {config.titlePrefix} {config.titleMain}
            </h2>
            <button type="button" className="go-back-link" onClick={onCancel}>
              Go Back
            </button>
          </div>

          <form className="create-form" onSubmit={handleSubmit}>
            <div className="form-group">
              <label className="input-label">{config.inputLabel}</label>
              <input
                type="text"
                className="modal-input"
                value={value}
                onChange={(e) => setValue(e.target.value)}
                autoFocus
                required
                placeholder={`Enter ${config.inputLabel?.toLowerCase()}...`}
              />
            </div>

            <div className="modal-actions">
              <button type="submit" className="btn-modal btn-create">
                {config.submitLabel}
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
          {message && <p className="message">{message}</p>}
        </div>
      </div>
    </div>
  );
};

export default CategoryModal;
