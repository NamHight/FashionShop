import { Radio, Typography } from "@material-tailwind/react";
import SelectCategoryName from "../SelectCategoryName";
import { useState } from "react";
export function RadioChecked({ data }) {
  console.log("categoriesdata", data);
  const [category, setCategory] = useState();
  console.log("categorydds", category);
  
  return (
    <div className="flex">
      <Radio>
        {data?.map((item) => (
          <div className="flex items-center gap-2">
            <Radio.Item id={item.name}>
              <Radio.Indicator />
            </Radio.Item>

            <Typography
              as="label"
              htmlFor={item.name}
              className=" gap-1 text-foreground"
              onClick={(e) => setCategory(e.target.value)}
            >
              <SelectCategoryName
                categoriesdata={item.categories}
                name={item.name}
              />
            </Typography>
          </div>
        ))}
      </Radio>
    </div>
  );
}
