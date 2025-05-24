import employeeImage from "../assets/employee.png";
export default function HomePage() {
  return (
    <div className="home">
      <h1>Welcome to Employee Portal</h1>
      <img src={employeeImage} alt="Employee" />
    </div>
  );
}
