const Button = ({ text = "Register", onClick, className, disabled, type }) => {
  return (
    <button
      onClick={onClick}
      className="red-button"
      disabled={disabled}
      type={type}
    >
      {text}
    </button>
  );
};

export default Button;
