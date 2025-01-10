import { Link } from "react-router";
import { List } from "@material-tailwind/react";

export function NavLinkBlog() {
  const Links = [
    { name: "Promotions", path: "/blog" },
    { name: "Articles", path: "/blog/article" },
  ];
  return (
    <>
      {Links.map((item, key) => {
        return (
          <List.Item
            key={key}
            className={"text-indigo-600 flex items-center pb-2 pr-2 border-b-2 uppercase"}
          >
            <Link
              to={item.path}
              className={
                "font-semibold inline-block p-3"
              }
            >
              {item.name}
            </Link>
          </List.Item>
        );
      })}
    </>
  );
}
