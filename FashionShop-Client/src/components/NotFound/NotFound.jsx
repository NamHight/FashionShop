function NotFound({ text, icon, classText }) {
  return (
    <div className="w-full mt-3 h-96 bg-slate-100 rounded py-2 flex justify-center items-center">
      <div>
        <p className="flex justify-center items-center">{icon}</p>
        <p className={classText}>{text}</p>
      </div>
    </div>
  );
}

export default NotFound;
