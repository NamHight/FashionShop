import * as React from "react";
import { FaFilter } from "react-icons/fa";
import "./style.css";
import {
  Drawer,
  Button,
  Typography,
  IconButton,
  List,
  Chip,
  Card,
  Collapse,
  Input,
  Avatar,
} from "@material-tailwind/react";

import {
  Xmark,
  Archive,
  EmptyPage,
  Folder,
  LogOut,
  Mail,
  MoreHorizCircle,
  NavArrowRight,
  Pin,
  Search,
  SelectFace3d,
  SendDiagonal,
  Bin,
  UserXmark,
} from "iconoir-react";
import { RadioChecked } from "../../../components/RadioChecked";
import { getCategories } from "../../../services/api/CategoryService";
import { useQuery } from "@tanstack/react-query";
import Loading from "../../../components/Loading";
import { useNavigate, useSearchParams } from "react-router";

const Links = [
  {
    icon: Mail,

    title: "Inbox",

    href: "#",

    badge: 14,
  },

  {
    icon: SendDiagonal,

    title: "Sent",

    href: "#",
  },

  {
    icon: EmptyPage,

    title: "Drafts",

    href: "#",
  },

  {
    icon: Pin,

    title: "Pins",

    href: "#",
  },

  {
    icon: Archive,

    title: "Archive",

    href: "#",
  },

  {
    icon: Bin,

    title: "Trash",

    href: "#",
  },
];

export function Filter() {
  const {
    data: categories,
    isLoading: isCategoryLoading,
    isPending,
    isError,
  } = useQuery({
    queryKey: ["category"],
    queryFn: async () => {
      let result = await getCategories();
      return result;
    },
  });
  const [minPrice, setMinPrice] = React.useState(0);
  const [maxPrice, setMaxPrice] = React.useState(0);
  let [searchParams] = useSearchParams();
  const path = useNavigate();
  const productName = searchParams.get("searchProduct");

  const handleSubmit = (e) => {
    e.preventDefault()
    const form = new FormData();
    // const minPrice = form.get('minPrice');
    // const maxPrice = form.get('maxPrice');
    console.log(minPrice, "dsfs", maxPrice, "dsdf", productName)
    path(`/search?searchProduct=${productName}&minPrice=${minPrice}&maxPrice=${maxPrice}`)
    window.location.reload();
  }

  return (
    <div className="p-5">
      <Drawer>
        <Drawer.Trigger
          as={Button}
          className="bg-slate-100 border border-emerald-400 filter"
        >
          <FaFilter className="text-emerald-400 iconfilter size-5" />
        </Drawer.Trigger>
        <Drawer.Overlay>
          <Drawer.Panel placement="left" className="p-0">
            <Card className="border-none shadow-none">
              <Card.Header className="m-0 flex h-max items-center justify-center gap-2 px-3 pb-3 pt-4 bg-emerald-400 w-full rounded-none">
                <div className="w-32 h-14">
                  <Avatar src="assets/Logo.png" alt="Logo.png" className="" />
                </div>
              </Card.Header>

              <Card.Body className="p-3">
                <List className="mt-3">
                  <hr className="-mx-3 my-3 border-secondary" />
                  {isPending ? (
                    <Loading />
                  ) : (
                    <RadioChecked data={categories.data} />
                  )}
                  <List.Item className="text-error hover:bg-error/10 hover:text-error focus:bg-error/10 focus:text-error"></List.Item>
                </List>
                <hr className="-mx-3 my-3 border-secondary" />
                <List className="mt-3">
                  <form onSubmit={(e) => handleSubmit(e)} method="get">
                    <Input
                      placeholder="Min Price"
                      id="minPrice"
                      value={minPrice}
                      onChange={(e) => setMinPrice(e.target.value < 0 ? 1 : e.target.value)}
                      className="mb-4"
                    />
                    <Input placeholder="Max Price" className="mb-4" id="maxPrice"
                      value={maxPrice}
                      onChange={(e) => setMaxPrice(e.target.value < 0 ? 1 : e.target.value)}
                    />
                    <Button variant="outline">Outline</Button>
                  </form>
                </List>
              </Card.Body>
            </Card>
          </Drawer.Panel>
        </Drawer.Overlay>
      </Drawer>
    </div>
  );
}
