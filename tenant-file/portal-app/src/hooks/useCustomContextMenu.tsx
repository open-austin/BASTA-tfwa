import React, { ReactElement, useState } from "react";

export function useCustomContextMenu(customMenu: ReactElement) {
  const [menu, setMenu] = useState(<></>);
  setMenu(customMenu);
  return menu;
}
