import { Tabs } from "@material-tailwind/react";
import ListAll from "./ListAll/index";
import ListFavorite from "./ListFavorite/index";
import ListPurchased from "./ListPurchased/index";
function ListFavorites() {
  return (
    <>
      <Tabs defaultValue="All">
        <Tabs.List className="w-full">
          <Tabs.Trigger className="w-full font-bold text-lg" value="All">
            All
          </Tabs.Trigger>
          <Tabs.Trigger className="w-full font-bold text-lg" value="Favorite">
            Favorite
          </Tabs.Trigger>
          <Tabs.Trigger className="w-full font-bold text-lg" value="Purchased">
            Purchased
          </Tabs.Trigger>
          <Tabs.TriggerIndicator />
        </Tabs.List>
        <Tabs.Panel value="All">
          <ListAll />
        </Tabs.Panel>
        <Tabs.Panel value="Favorite">
          <ListFavorite />
        </Tabs.Panel>
        <Tabs.Panel value="Purchased">
          <ListPurchased />
        </Tabs.Panel>
      </Tabs>
    </>
  );
}

export default ListFavorites;
