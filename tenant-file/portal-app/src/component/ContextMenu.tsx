import React from "react";
import { useContextMenu } from "../hooks/useContextMenu";
import { Motion, spring } from "react-motion";
// import { BatchImageData } from "../..";

// import { useCustomContextMenu } from "../hooks/useCustomContextMenu";

interface CommandItem {
  displayText: string;
  commandFunc: Function;
}
interface ContextMenuProps {
  commands: CommandItem[] | null;
}
const ContextMenu: React.FC<ContextMenuProps> = ({ commands }) => {
  const { xPos, yPos, showMenu, targetClass } = useContextMenu();

  return (
    <Motion
      defaultStyle={{ opacity: 0 }}
      style={{ opacity: !showMenu ? spring(0) : spring(1) }}
    >
      {(interpolatedStyle) => (
        <>
          {showMenu && targetClass === "downloadableImage" ? (
            <div
              className="menu-container"
              style={{
                position: "fixed",
                height: "auto",
                width: "auto",
                padding: "5px 20px 5px 5px",
                backgroundColor: "grey",
                boxShadow: "1px 2px grey",
                color: "white",
                top: yPos,
                left: xPos,
                opacity: interpolatedStyle.opacity,
                zIndex: 999,
                borderRadius: "4px",
              }}
            >
              {commands?.map((item) => (
                <button
                  type={"button"}
                  className={"btn btn-link"}
                  onClick={() => item.commandFunc()}
                >
                  {item.displayText}
                </button>
              ))}
            </div>
          ) : (
            <></>
          )}
        </>
      )}
    </Motion>
  );
};

export default ContextMenu;
