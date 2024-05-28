import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import "./index.css"


const divDoIndex = document.getElementById("root");
const root = createRoot(divDoIndex);

root.render(
    <BrowserRouter><App /></BrowserRouter>
);