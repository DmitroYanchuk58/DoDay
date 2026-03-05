const Input = ({ icon, alt, type = 'text', placeholder, value, onChange }) => {
  return (
    <div className={`input-wrapper`}>
      <div className="input-icon-container">
        <img src={icon} alt={alt} className="input-icon" />
      </div>
      <div className="input-field-container">
        <input 
          className="input-field" 
          type={type}
          placeholder={placeholder} 
          value={value}            
          onChange={onChange}      
        />
      </div>
    </div>
  );
};

export default Input;