import { useCallback, useEffect, useState } from "react";
export const useContextMenu = () => {
  const [xPos, setXPos] = useState("0px");
  const [yPos, setYPos] = useState("0px");
  const [showMenu, setShowMenu] = useState(false);
  const [targetClass, setTargetClass] = useState("");

  const handleContextMenu = useCallback(
    (e) => {
      e.preventDefault();
      console.log(`e.target.className ${e.target.className}`)
      console.log(`e.target.className match  ${e.target.className === "downloadableImage"}`)
      setTargetClass(e.target.className);
      setXPos(`${e.clientX}px`);
      setYPos(`${e.clientY}px`);
      setShowMenu(true);
    },
    [setXPos, setYPos]
  );

  const handleClick = useCallback(() => {
    showMenu && setShowMenu(false);
  }, [showMenu]);

  useEffect(() => {
    document.addEventListener("click", handleClick);
    document.addEventListener("contextmenu", handleContextMenu);
    return () => {
      document.addEventListener("click", handleClick); 
      document.removeEventListener("contextmenu", handleContextMenu);
    };
  });

  return { xPos, yPos, showMenu, targetClass };
};
