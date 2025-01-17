import { Select } from "@material-tailwind/react";
import { useState } from "react";
function SelectCategoryName({ categoriesdata, name}) {
  console.log("categoriesdataưgfhg", categoriesdata);
  const [category, setCategody] = useState('');
  console.log("categorycategory category", category);
  
  return (
    <div className="w-32">
      <Select color="success">
        <Select.Trigger placeholder={name} />
        <Select.List>
          {categoriesdata?.map((item) => (
            <Select.Option key={item.id} value={item.categoryName} onClick={(e) => setCategody(item.categoryName)}>{item.categoryName}</Select.Option>
          ))}
        </Select.List>
      </Select>
    </div>
  );
}

export default SelectCategoryName;
