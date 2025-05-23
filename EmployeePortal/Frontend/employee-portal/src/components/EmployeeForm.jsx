import { Button } from "./Button";
import "../styles/EmployeeForm.css";

export const EmployeeForm = ({ onSave, formData, setFormData }) => {
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  const handleSubmit = (e) => {
    e.preventDefault();

    if (formData.name || formData.email || formData.position) {
      onSave(formData);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="employee-form">
      <input
        name="name"
        value={formData.name || ""}
        onChange={handleChange}
        placeholder="Name"
        className="input"
      />
      <input
        type="email"
        name="email"
        value={formData.email || ""}
        onChange={handleChange}
        placeholder="Email"
        className="input"
      />
      <input
        name="position"
        value={formData.position || ""}
        onChange={handleChange}
        placeholder="Position"
        className="input"
      />
      <Button type="primary">Save</Button>
    </form>
  );
};
