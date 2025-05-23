import "./App.css";
import { useState, useEffect } from "react";
import { EmployeeForm } from "./components/EmployeeForm";
import { EmployeeGrid } from "./components/EmployeeGrid";

export default function App() {
  const [employees, setEmployees] = useState([]);
  const [editIndex, setEditIndex] = useState(null);
  const [formData, setFormData] = useState({});

  useEffect(() => {
    const stored = JSON.parse(localStorage.getItem("employees")) || [];
    setEmployees(stored);
  }, []);

  useEffect(() => {
    if (editIndex !== null) {
      setFormData(employees[editIndex]);
    } else {
      setFormData({});
    }
  }, [editIndex, employees]);

  const handleSave = (employee) => {
    const updated = [...employees];
    if (editIndex !== null) {
      updated[editIndex] = employee;
      setEditIndex(null);
    } else {
      updated.push(employee);
    }
    setEmployees(updated);
    localStorage.setItem("employees", JSON.stringify(updated));
    setFormData({});
  };

  const handleDelete = (index) => {
    const updated = [...employees];
    updated.splice(index, 1);
    setEmployees(updated);
    localStorage.setItem("employees", JSON.stringify(updated));
    if (editIndex === index) {
      setEditIndex(null);
    } else if (editIndex !== null && index < editIndex) {
      setEditIndex(editIndex - 1);
    }
  };

  const handleUpdate = (index) => {
    setEditIndex(index);
  };

  return (
    <div className="app-container">
      <h1 className="app-title">Employee Portal</h1>
      <EmployeeForm
        onSave={handleSave}
        formData={formData}
        setFormData={setFormData}
      />
      <EmployeeGrid
        employees={employees}
        onDelete={handleDelete}
        onUpdate={handleUpdate}
      />
    </div>
  );
}
