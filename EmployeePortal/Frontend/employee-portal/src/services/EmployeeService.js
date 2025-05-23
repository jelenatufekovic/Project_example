import axios from "axios";

const API_URL = "https://localhost:7285/api/employee";

export const getEmployees = () => {
  return axios.get(`${API_URL}/getAll`);
};

export const addEmployee = (employee) => {
  return axios.post(API_URL, employee);
};

export const updateEmployee = (id, employee) => {
  return axios.put(`${API_URL}/${id}`, employee);
};

export const deleteEmployee = (id) => {
  return axios.delete(`${API_URL}/${id}`);
};
