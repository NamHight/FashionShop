import axios from "axios";

const API_URL = "http://localhost:7068/api/Supplier";

export const getAllSuppliers = async (trackChanges = false) => {
  try {
    const response = await axios.get(`${API_URL}?trackChanges=${trackChanges}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching suppliers:", error);
    throw error;
  }
};
