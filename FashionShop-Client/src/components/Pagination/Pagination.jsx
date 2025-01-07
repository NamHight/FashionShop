import { Button, IconButton } from "@material-tailwind/react";
import { NavArrowRight, NavArrowLeft } from "iconoir-react";
import { useCartConText } from "../../context/CartContext";

export function Pagination() {
  const { page, setPage, totalPages } = useCartConText();

  const next = () => {
    if (page === totalPages) return;
    setPage(page + 1);
  };

  const prev = () => {
    if (page === 1) return;
    setPage(page - 1);
  };

  return (
    <div className="flex items-center justify-start gap-4 bg-gray-100 p-4 rounded-lg shadow-md w-fit">
      <Button
        variant="solid"
        onClick={prev}
        color="primary"
        className="flex items-center gap-2"
        disabled={page === 1}
      >
        <NavArrowLeft className="h-5 w-5" />
        Previous
      </Button>
      <span className="text-gray-700 font-medium">
        Page {page} - {totalPages}
      </span>
      <Button
        variant="solid"
        onClick={next}
        color="info"
        className="flex items-center gap-2"
        disabled={page === totalPages}
      >
        Next
        <NavArrowRight className="h-5 w-5" />
      </Button>
    </div>
  );
}
