import { Button } from "./Button";
import "../styles/EmployeeForm.css";
import { useNavigate } from "react-router-dom";

export const EmployeeForm = ({
  onSave,
  formData,
  setFormData,
  positions,
  isEdit,
}) => {
  const navigate = useNavigate();
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  const handleSubmit = (e) => {
    e.preventDefault();

    if (formData.name || formData.email || formData.workDepartment) {
      onSave(formData);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="employee-form">
      <input
        name="firstName"
        value={formData.firstName || ""}
        onChange={handleChange}
        placeholder="First name"
        className="input"
      />
      <input
        name="lastName"
        value={formData.lastName || ""}
        onChange={handleChange}
        placeholder="Last name"
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
      <select
        name="workDepartment"
        value={formData.workDepartmentId || ""}
        onChange={(e) =>
          setFormData({ ...formData, workDepartmentId: e.target.value })
        }
        className="input"
      >
        <option value="">Select a position</option>
        {positions.map((pos) => (
          <option key={pos.id} value={pos.id}>
            {pos.name}
          </option>
        ))}
      </select>
      <Button type="primary">Save</Button>
      {isEdit && (
        <Button type="primary" onClick={() => navigate("/employees")}>
          Cancel
        </Button>
      )}
    </form>
  );
};
