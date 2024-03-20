import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App";

const container: HTMLElement | null = document.getElementById("root");
if (container === null) {
  throw new Error("Missing element with id 'root' from index.html!");
}

createRoot(container).render(
  <StrictMode>
    <App />
  </StrictMode>
);
