import { Card, Typography, Button } from "@material-tailwind/react";
function Favorite({ item }) {
  return (
    <div className="w-full">
      <Card className="w-full">
        <Card.Header>
          <img
            src={
              item.banner
                ? "/assets/images/products/" + item.banner
                : "/assets/Logo.png"
            }
            alt={item.banner}
            className="h-60 w-96 object-center shadow-sm shadow-black/25"
          />
        </Card.Header>
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
    </div>
  );
}

export default Favorite;
