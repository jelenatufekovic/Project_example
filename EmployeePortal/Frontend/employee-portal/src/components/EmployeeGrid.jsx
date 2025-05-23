import React from "react";
import "../styles/EmployeeGrid.css";
import { Button } from "./Button";

export const EmployeeGrid = ({ employees, onDelete, onUpdate }) => {
  return (
    <div className="employee-grid">
      {employees && employees.length > 0 ? (
        employees.map((emp, index) => (
          <div key={index} className="employee-card">
            <h2 className="employee-name">{emp.firstName} </h2>
            <h2 className="employee-name">{emp.lastName} </h2>
            <p>Email: {emp.email}</p>
            <p>
              Position:
              {emp.workDepartment?.name || "No position yet."}
            </p>
            <Button type="danger" onClick={() => onDelete(index)}>
              Delete
            </Button>
            <Button type="primary" onClick={() => onUpdate(index)}>
              Update
            </Button>
          </div>
        ))
      ) : (
        <div className="employee-card">No employees yet.</div>
      )}
    </div>
  );
};
