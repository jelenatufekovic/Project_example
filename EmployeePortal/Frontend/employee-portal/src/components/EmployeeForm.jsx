import { Button } from "./Button";
import "../styles/EmployeeForm.css";

export const EmployeeForm = ({ onSave, initialData = {} }) => {
  const draft = JSON.parse(localStorage.getItem("employee") || "null");
  if (!draft && Object.keys(initialData).length > 0) {
    localStorage.setItem("employee", JSON.stringify(initialData));
  }
  const handleChange = (e) => {
    const { name, value } = e.target;
    const currentEmployee = JSON.parse(
      localStorage.getItem("employee") || "{}"
    );

    const employee = {
      ...currentEmployee,
      [name]: value,
    };

    localStorage.setItem("employee", JSON.stringify(employee));
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    const employee = JSON.parse(localStorage.getItem("employee"));
    if (employee != null) {
      onSave(employee);
      localStorage.removeItem("employee");
      e.target.reset();
    }
  };

  const data = draft || initialData;

  return (
    <form onSubmit={handleSubmit} className="employee-form">
      <input
        name="name"
        defaultValue={data.name || ""}
        onChange={handleChange}
        placeholder="Name"
        className="input"
      />
      <input
        name="email"
        defaultValue={data.email || ""}
        onChange={handleChange}
        placeholder="Email"
        className="input"
      />
      <input
        name="position"
        defaultValue={data.position || ""}
        onChange={handleChange}
        placeholder="Position"
        className="input"
      />
      <Button type="primary">Save</Button>
    </form>
  );
};
