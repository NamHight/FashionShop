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
  const [isOpen, setIsOpen] = React.useState(false);

  return (
    <div>
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
                  {Links.map(({ icon: Icon, title, href, badge }) => (
                    <List.Item key={title} href={href}>
                      <List.ItemStart>
                        <Icon className="h-[18px] w-[18px]" />
                      </List.ItemStart>

                      {title}

                      {badge && (
                        <List.ItemEnd>
                          <Chip size="sm" variant="ghost">
                            <Chip.Label>{badge}</Chip.Label>
                          </Chip>
                        </List.ItemEnd>
                      )}
                    </List.Item>
                  ))}

                  <hr className="-mx-3 my-3 border-secondary" />

                  <List.Item onClick={() => setIsOpen((cur) => !cur)}>
                    <List.ItemStart>
                      <MoreHorizCircle className="h-[18px] w-[18px]" />
                    </List.ItemStart>
                    More
                    <List.ItemEnd>
                      <NavArrowRight
                        className={`h-4 w-4 ${isOpen ? "rotate-90" : ""}`}
                      />
                    </List.ItemEnd>
                  </List.Item>

                  <Collapse open={isOpen}>
                    <List>
                      <List.Item>
                        <List.ItemStart>
                          <Folder className="h-[18px] w-[18px]" />
                        </List.ItemStart>
                        Spam
                      </List.Item>

                      <List.Item>
                        <List.ItemStart>
                          <UserXmark className="h-[18px] w-[18px]" />
                        </List.ItemStart>
                        Blocked
                      </List.Item>

                      <List.Item>
                        <List.ItemStart>
                          <Folder className="h-[18px] w-[18px]" />
                        </List.ItemStart>
                        Important
                      </List.Item>
                    </List>
                  </Collapse>

                  <hr className="-mx-3 my-3 border-secondary" />

                  <List.Item className="text-error hover:bg-error/10 hover:text-error focus:bg-error/10 focus:text-error">
                    <List.ItemStart>
                      <LogOut className="h-[18px] w-[18px]" />
                    </List.ItemStart>
                    Logout
                  </List.Item>
                </List>
              </Card.Body>
            </Card>
          </Drawer.Panel>
        </Drawer.Overlay>
      </Drawer>
    </div>
  );
}
