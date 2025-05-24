import { getEmployeeById, updateEmployee } from "../services/EmployeeService";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { EmployeeForm } from "../components/EmployeeForm";
import { useParams } from "react-router-dom";
import { getPositions } from "../services/WorkDepartmentService";

export default function EditEmployeePage() {
  const { id } = useParams();
  const [formData, setFormData] = useState({});
  const [positions, setPositions] = useState([]);
  const navigate = useNavigate();

  const getEmployee = async () => {
    try {
      const response = await getEmployeeById(id);
      setFormData(response.data);
    } catch (error) {
      console.error("Error loading employee.", error);
    }
  };
  useEffect(() => {
    getEmployee();
  }, [id]);

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
      await updateEmployee(id, employee);

      navigate("/employees");
    } catch (error) {
      console.error("Error saving employee", error);
    }
  };
  return (
    <div>
      <h2>Edit Employee</h2>
      <EmployeeForm
        onSave={handleSave}
        formData={formData}
        setFormData={setFormData}
        positions={positions}
        isEdit={true}
      />
    </div>
  );
}
