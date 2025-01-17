import Content from "./Content";
import { Filter } from "./Filter";

function Search() {
  return (
    <div className="bg-slate-100 w-full h-full mt-4 rounded-xl">
      <div className="m-5">
        <div>
          <Filter />
        </div>
        <div className="mt-2">
          <Content />
        </div>
      </div>
    </div>
  );
}

export default Search;
