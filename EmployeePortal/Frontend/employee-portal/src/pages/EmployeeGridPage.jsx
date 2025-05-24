import { useEffect, useState } from "react";
import { EmployeeGrid } from "../components/EmployeeGrid";
import { useNavigate } from "react-router-dom";
import { getEmployees, deleteEmployee } from "../services/EmployeeService";

export default function EmployeeGridPage() {
  const [employees, setEmployees] = useState([]);
  const navigate = useNavigate();

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
  const handleDelete = async (id) => {
    try {
      await deleteEmployee(id);
      await loadEmployees();
    } catch (error) {
      console.error("Error deleting employee", error);
    }
  };

  const handleUpdate = (id) => {
    navigate(`/edit/${id}`);
  };
  return (
    <EmployeeGrid
      employees={employees}
      onDelete={handleDelete}
      onUpdate={handleUpdate}
    />
  );
}
