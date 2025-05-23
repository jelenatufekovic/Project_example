import { Button } from "./Button";
export const EmployeeEditForm = ({ onSave, initialData = {} }) => {
  const handleChange = (e) => {
    const { name, value } = e.target;
    const current = JSON.parse(localStorage.getItem("employee")) || initialData;

    const updated = {
      ...current,
      [name]: value,
    };

    localStorage.setItem("employee", JSON.stringify(updated));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const employee = JSON.parse(localStorage.getItem("employee"));
    if (employee) {
      onSave(employee);
      localStorage.removeItem("employee");
    }
  };

  const draft = JSON.parse(localStorage.getItem("employee") || "null");
  const data = draft || initialData;

  return (
    <form onSubmit={handleSubmit} className="employee-form">
      <h3>Edit Employee</h3>
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
      <Button variant="primary">Update</Button>
    </form>
  );
};
