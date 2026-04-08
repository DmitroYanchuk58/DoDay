const FormInput = ({
  label,
  name,
  value,
  onChange,
  type = "text",
  fullWidth = false,
}) => (
  <div className={`form-group ${fullWidth ? "full-width" : ""}`}>
    <label>{label}</label>
    <input type={type} name={name} value={value} onChange={onChange} />
  </div>
);

export default FormInput;
