import "./App.css";
import { EmployeeForm } from "./components/EmployeeForm";
import { EmployeeGrid } from "./components/EmployeeGrid";
import { EmployeeEditForm } from "./components/EmployeeEditForm";
function App() {
  const storedEmployees = JSON.parse(localStorage.getItem("employees")) || [];
  const editIndex = localStorage.getItem("editIndex");
  const initialData =
    editIndex !== null ? storedEmployees[parseInt(editIndex, 10)] : {};

  const handleSave = (employee) => {
    const stored = JSON.parse(localStorage.getItem("employees")) || [];

    if (editIndex !== null) {
      stored[parseInt(editIndex, 10)] = employee;
      localStorage.removeItem("editIndex");
    } else {
      stored.push(employee);
    }

    localStorage.setItem("employees", JSON.stringify(stored));
    window.location.reload();
  };

  const handleDelete = (index) => {
    const stored = JSON.parse(localStorage.getItem("employees")) || [];
    stored.splice(index, 1);
    localStorage.setItem("employees", JSON.stringify(stored));
    const editIndex = localStorage.getItem("editIndex");
    if (editIndex !== null && parseInt(editIndex, 10) === index) {
      localStorage.removeItem("editIndex");
      localStorage.removeItem("employee");
    }
    window.location.reload();
  };

  const handleUpdate = (index) => {
    localStorage.setItem("editIndex", index);
    window.location.reload();
  };

  return (
    <div className="app-container">
      <h1 className="app-title">Employee Portal</h1>
      {editIndex === null ? (
        <EmployeeForm onSave={handleSave} />
      ) : (
        <EmployeeEditForm onSave={handleSave} initialData={initialData} />
      )}
      <EmployeeGrid
        employees={storedEmployees}
        onDelete={handleDelete}
        onUpdate={handleUpdate}
      />
    </div>
  );
}

export default App;
