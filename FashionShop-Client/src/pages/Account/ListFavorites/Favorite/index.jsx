import { FaLocationDot, TbCategoryPlus } from "react-icons/fa6";
import { Tooltip, IconButton } from "@material-tailwind/react";
import { Card, Typography, Button } from "@material-tailwind/react";
function Favorite({ item }) {
  return (
    <div className="w-full">
      <Tooltip interactive placement="bottom">
        <Tooltip.Trigger className="hover:bg-slate-600 p-1 rounded w-full">
          <Card className="w-full">
            <Card.Header
              as="img"
              src={
                item.banner
                  ? "/assets/images/products/" + item.banner
                  : "/assets/Logo.png"
              }
              alt={item.banner}
              className="justify-items-center object-cover h-52 w-96 p-2.5 m-0"
            />
            <Card.Body>
              <div className="mb-2 flex items-center justify-between">
                <Typography type="h5">{item.productName}</Typography>
                <Typography type="h6" className="text-red-500">
                  ${item.price}
                </Typography>
              </div>
            </Card.Body>
            <Card.Footer>
              <div className="flex items-center justify-between">
                <Typography type="small" className="text-slate-500">
                  Loại: {item.categoryName}
                </Typography>
              </div>
            </Card.Footer>
          </Card>
        </Tooltip.Trigger>
        <Tooltip.Content className="flex flex-col bg-transparent shadow-none dark:bg-transparent">
          <Button isFullWidth color="warning" className="hover:text-red-500 w-full">
            Add to Cart
          </Button>
        </Tooltip.Content>
      </Tooltip>
    </div>
  );
}

export default Favorite;
