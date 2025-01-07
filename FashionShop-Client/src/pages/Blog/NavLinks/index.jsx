import { Link } from "react-router";
import { List } from "@material-tailwind/react";

export function NavLinkBlog() {
    const Links = [
        { name: "Khuyến mãi", path: "/blog" },
        { name: "Bài viết", path: "/blog/article" },
      ];
  return (
    <>
      {Links.map((item, key) => {
        return (
          <>
            <List.Item
              key={key}
              className={"flex flex-1 items-center justify-between"}
            >
              <Link
                to={item.path}
                className={
                  "text-white hover:text-red-500 bg-neutral-500 px-8 py-4"
                }
              >
                {item.name}
              </Link>
            </List.Item>
          </>
        );
      })}
    </>
  );
}
