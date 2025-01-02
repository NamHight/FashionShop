export default function ListFavorite() {
  return (
    <>
      <div className="bg-slate-100 rounded">
        <div className="p-5 flex">
          <div className="flex w-full">
            <div>
              <img
                src="https://th.bing.com/th/id/OIP.Ci-Qe31tIUR4AcmesfCB8AHaHa?w=194&h=194&c=7&r=0&o=5&dpr=1.3&pid=1.7"
                alt="ảnh.jpg"
                width={150}
              />
            </div>
            <div className="ml-5 text-xl w-full h-full">
              <div>
                <h1 className="font-bold">Alo con bò</h1>
              </div>
              <div>
                <p>Loại: abc</p>
                <p>Lượng x2</p>
              </div>
            </div>
          </div>
          <div className="w-36 text-xl flex justify-center items-center text-red-600 ">
            $9.000
          </div>
        </div>
      </div>
      <div className="mt-5 bg-slate-100 rounded">
        <div className="p-5 flex">
          <div className="flex w-full">
            <div>
              <img
                src="https://th.bing.com/th/id/OIP.Ci-Qe31tIUR4AcmesfCB8AHaHa?w=194&h=194&c=7&r=0&o=5&dpr=1.3&pid=1.7"
                alt="ảnh.jpg"
                width={150}
              />
            </div>
            <div className="ml-5 text-xl w-full h-full">
              <div>
                <h1 className="font-bold">Alo con bò</h1>
              </div>
              <div>
                <p>Loại: abc</p>
                <p>Lượng x2</p>
              </div>
            </div>
          </div>
          <div className="w-36 text-xl flex justify-center items-center text-red-600 ">
            $9.000
          </div>
        </div>
      </div>
    </>
  );
}
