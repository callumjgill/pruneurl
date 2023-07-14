import { Button } from "react-bootstrap";
import ButtonSpinner from "./ButtonSpinner";
import { useRef, useState } from "react";
import CopyToClipboardTooltip from "../tooltips/CopyToClipboardTooltip";

interface CopyToClipboardButtonProps {
  text: string;
}

const CopyToClipboardButton = (props: CopyToClipboardButtonProps) => {
  const { text } = { ...props };

  const buttonRef = useRef<HTMLButtonElement | null>(null);
  const [copying, setCopying] = useState<boolean>(false);
  const [showTooltip, setShowTooltip] = useState<boolean>(false);

  const handleClick = async () => {
    setCopying(true);
    await navigator.clipboard.writeText(text);
    setCopying(false);
    setShowTooltip(true);
  };

  return (
    <>
      <Button
        ref={buttonRef}
        className="copy-button"
        variant="secondary"
        onClick={handleClick}
      >
        {copying ? <ButtonSpinner /> : "Copy"}
      </Button>
      <CopyToClipboardTooltip
        target={buttonRef.current}
        show={showTooltip}
        onTimeout={() => setShowTooltip(false)}
      />
    </>
  );
};

export default CopyToClipboardButton;
