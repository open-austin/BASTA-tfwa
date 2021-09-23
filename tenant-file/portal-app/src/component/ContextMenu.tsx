import React, { ReactElement } from "react";
import { useContextMenu } from "../hooks/useContextMenu";
import { Motion, spring } from "react-motion";
import { BatchImageData } from "../..";
interface ImageMenuProps {
  imageData: BatchImageData;
}
interface ContextMenuProps {
  menu: ReactElement;
  // menu: React.FC<ImageMenuProps>;
}
const ContextMenu: React.FC<ContextMenuProps> = ({ menu }) => {
  const { xPos, yPos, showMenu } = useContextMenu();

  return (
    <Motion
      defaultStyle={{ opacity: 0 }}
      style={{ opacity: !showMenu ? spring(0) : spring(1) }}
    >
      {(interpolatedStyle) => (
        <>
          {showMenu ? (
            <div
              className="menu-container"
              style={{
                top: yPos,
                left: xPos,
                opacity: interpolatedStyle.opacity,
              }}
            >
              {menu}
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
