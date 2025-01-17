import {Card, Typography } from "@material-tailwind/react";

function Content({ data }) {
  return (
    <div>
      <div className="grid grid-cols-4 gap-4 pb-5 px-5">
        {data.map((item) => (
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
        ))}
      </div>
    </div>
  );
}

export default Content;
