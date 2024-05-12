import { useEffect, useState } from "react";
import { Overlay, Tooltip } from "react-bootstrap";
import { waitAsync } from "../../../../../utils/time";

interface CopyToClipboardTooltipProps {
  target: HTMLElement | null;
  show?: boolean;
  onTimeout?: () => void;
}

const CopyToClipboardTooltip = (props: CopyToClipboardTooltipProps) => {
  const { target, show, onTimeout } = { ...props };

  const [internalShow, setInternalShow] = useState<boolean>(show ?? false);

  useEffect(() => {
    setInternalShow(show ?? false);
    if (show) {
      waitAsync(5).then(() => setInternalShow(false));
    }
  }, [show]);

  return (
    <Overlay
      target={target}
      show={internalShow}
      placement="right"
      onExited={onTimeout}
    >
      <Tooltip>Copied!</Tooltip>
    </Overlay>
  );
};

export default CopyToClipboardTooltip;
