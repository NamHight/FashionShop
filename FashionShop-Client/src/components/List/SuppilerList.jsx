import React, { useEffect, useState } from "react";
import { getAllSuppliers } from "../../services/api/SuppilerService";
import Loading from "../Loading";
import SupplierCard from "./SuppilerCard";

const SupplierList = () => {
  const [suppliers, setSuppliers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchSuppliers = async () => {
      try {
        const data = await getAllSuppliers();
        setSuppliers(data);
      } catch (err) {
        setError("Unable to fetch suppliers.");
      } finally {
        setLoading(false);
      }
    };

    fetchSuppliers();
  }, []);

  if (loading) return <Loading />;
  if (error)
    return (
      <div className="bg-gray-100 px-4 text-center">
        <h1 className="text-4xl font-bold text-red-500">Error</h1>
        <p className="text-lg text-gray-700">{error}</p>
      </div>
    );

  return (
    <div className="max-w-screen-lg mx-auto py-8">
      <h1 className="text-3xl  text-blue-500 font-bold mb-6">Shipping Service Information</h1>
      <div className="space-y-4">
        {suppliers.map((supplier) => (
          <SupplierCard key={supplier.supplierId} supplier={supplier} />
        ))}
      </div>
    </div>
  );
};

export default SupplierList;
