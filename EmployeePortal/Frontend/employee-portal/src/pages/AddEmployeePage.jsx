import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { EmployeeForm } from "../components/EmployeeForm";
import { getPositions } from "../services/WorkDepartmentService";
import { addEmployee } from "../services/EmployeeService";
export default function AddEmployeePage() {
  const [formData, setFormData] = useState({});
  const [positions, setPositions] = useState([]);
  const navigate = useNavigate();

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

  const handleSave = async (employee) => {
    try {
      await addEmployee(employee);

      navigate("/employees");
    } catch (error) {
      console.error("Error saving employee", error);
    }
  };
  return (
    <div>
      <h2>Add New Employee</h2>
      <EmployeeForm
        onSave={handleSave}
        formData={formData}
        setFormData={setFormData}
        positions={positions}
        isEdit={false}
      />
    </div>
  );
}
