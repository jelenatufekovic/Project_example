import "../styles/Button.css";

export const Button = ({ children, onClick, type = "primary" }) => {
  return (
    <button onClick={onClick} className={`button ${type}`}>
      {children}
    </button>
  );
};
