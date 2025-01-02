import React from 'react';
import {Button, List, Tooltip, Typography} from "@material-tailwind/react";
import {Link} from "react-router";

const Categories = () => {
    return (
            <List className="grid grid-row-3 gap-y-2 text-black w-36 p-0 m-0 relative">
                <List.Item as="div" href="#list-with-link p-0 m-0">
                    <Link to={"#wwww"} className={'w-full'}>Tesstss</Link>
                </List.Item>
                <Tooltip placement="right" interactive className={'p-0 m-0 left-2 absolute'}>
                    <Tooltip.Trigger as={Button} className={'w-full p-0'}>
                            <Link to={"#ssss"} className={'w-full'}>Tesst</Link>
                    </Tooltip.Trigger>
                    <Tooltip.Content className={'bg-white text-black'}>
                        <List className="grid grid-row-3 gap-y-2 text-black w-36">
                            <List.Item as="div" href="#list-with-link">
                                <Link to={"#wwww"} className={'w-full'}>Tesstss</Link>
                            </List.Item>
                            <Tooltip placement="right" interactive>
                                <Tooltip.Trigger as={Typography}>
                                    <List.Item as="div" to={'#'} className={'w-full'}>
                                        <Link to={"#ssss"} className={'w-full'}>Tesst</Link>
                                    </List.Item>
                                </Tooltip.Trigger>
                                <Tooltip.Content className={'bg-white text-black'}>
                                    Material Tailwind
                                </Tooltip.Content>
                            </Tooltip>
                            <List.Item as="a" href="#list-with-link">
                                Settings
                            </List.Item>
                        </List>
                        <Tooltip.Arrow />
                    </Tooltip.Content>
                </Tooltip>
                <List.Item as="a" href="#list-with-link">
                    Settings
                </List.Item>
            </List>
    );
};

export default Categories;