import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import HomePage from "./pages/HomePage";
import EmployeeGridPage from "./pages/EmployeeGridPage";
import AddEmployeePage from "./pages/AddEmployeePage";
import EditEmployeePage from "./pages/EditEmployeePage";
import "./App.css";
import "./styles/Link.css";
export default function App() {
  return (
    <Router>
      <div className="app-container">
        <nav className="navigation">
          <Link to="/" className="nav-link">
            Home
          </Link>
          <Link to="/employees" className="nav-link">
            Employees
          </Link>
          <Link to="/add" className="nav-link">
            Add Employee
          </Link>
        </nav>

        <main>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/employees" element={<EmployeeGridPage />} />
            <Route path="/add" element={<AddEmployeePage />} />
            <Route path="/edit/:id" element={<EditEmployeePage />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}
