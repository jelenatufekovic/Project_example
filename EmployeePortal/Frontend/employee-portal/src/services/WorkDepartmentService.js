import axios from "axios";

const API_URL = "https://localhost:7285/api/workDepartment";

export const getPositions = () => {
  return axios.get(API_URL);
};
