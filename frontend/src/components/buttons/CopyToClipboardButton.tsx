import { Button } from "react-bootstrap";
import ButtonSpinner from "./ButtonSpinner";
import { useState } from "react";

interface CopyToClipboardButtonProps {
  text: string;
}

const CopyToClipboardButton = (props: CopyToClipboardButtonProps) => {
  const { text } = { ...props };

  const [copying, setCopying] = useState<boolean>(false);

  const handleClick = async () => {
    setCopying(true);
    await navigator.clipboard.writeText(text);
    setCopying(false);
  };

  return (
    <Button className="copy-button" variant="secondary" onClick={handleClick}>
      {copying ? <ButtonSpinner /> : "Copy"}
    </Button>
  );
};

export default CopyToClipboardButton;
