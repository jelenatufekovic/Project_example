import "./App.css";
import { useState, useEffect } from "react";
import { EmployeeForm } from "./components/EmployeeForm";
import { EmployeeGrid } from "./components/EmployeeGrid";
import {
  getEmployees,
  addEmployee,
  updateEmployee,
  deleteEmployee,
} from "./services/EmployeeService";
import { getPositions } from "./services/WorkDepartmentService";

export default function App() {
  const [employees, setEmployees] = useState([]);
  const [editIndex, setEditIndex] = useState(null);
  const [formData, setFormData] = useState({});
  const [positions, setPositions] = useState([]);

  const loadPositionsDropdown = async () => {
    try {
      const response = await getPositions();
      setPositions(response.data);
    } catch (error) {
      console.error("Error loading positions", error);
    }
  };
  useEffect(() => {
    loadPositionsDropdown();
  }, []);

  const loadEmployees = async () => {
    try {
      const response = await getEmployees();
      setEmployees(response.data);
    } catch (error) {
      console.error("Error loading employees", error);
    }
  };

  useEffect(() => {
    loadEmployees();
  }, []);

  const handleSave = async (employee) => {
    try {
      if (editIndex !== null) {
        await updateEmployee(employees[editIndex].id, employee);
        setEditIndex(null);
      } else {
        await addEmployee(employee);
      }
      setFormData({});
      await loadEmployees();
    } catch (error) {
      console.error("Error saving employee", error);
    }
  };

  const handleDelete = async (index) => {
    try {
      const employeeId = employees[index].id;
      await deleteEmployee(employeeId);
      await loadEmployees();
      if (editIndex === index) setEditIndex(null);
      else if (editIndex !== null && index < editIndex)
        setEditIndex(editIndex - 1);
    } catch (error) {
      console.error("Error deleting employee", error);
    }
  };

  const handleUpdate = (index) => {
    setEditIndex(index);
  };

  useEffect(() => {
    if (editIndex !== null) {
      setFormData(employees[editIndex]);
    } else {
      setFormData({});
    }
  }, [editIndex, employees]);

  return (
    <div className="app-container">
      <h1 className="app-title">Employee Portal</h1>
      <EmployeeForm
        onSave={handleSave}
        formData={formData}
        setFormData={setFormData}
        positions={positions}
      />
      <EmployeeGrid
        employees={employees}
        onDelete={handleDelete}
        onUpdate={handleUpdate}
      />
    </div>
  );
}
