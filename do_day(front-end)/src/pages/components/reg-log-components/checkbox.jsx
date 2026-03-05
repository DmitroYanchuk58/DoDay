import React from 'react';

const Checkbox = ({ id, text, checked, onChange }) => {
  return (
    <div className="checkbox-wrapper">
      <input
        type="checkbox"
        id={id}
        className="custom-checkbox"
        checked={checked}
        onChange={onChange}
      />
      <label htmlFor={id} className="checkbox-label">
        {text}
      </label>
    </div>
  );
};

export default Checkbox;