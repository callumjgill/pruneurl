import { Toast } from "react-bootstrap";
import SmallTimeAgo from "../TimeAgo/SmallTimeAgo";
import { useState } from "react";
import {
  getDescriptionFromStatusCode,
  isSuccess,
} from "../../utils/statusCodes";

interface SubmitToastProps {
  statusCode: number;
  onClose?: (event?: React.MouseEvent | React.KeyboardEvent) => void;
}

const SubmitToast = (props: SubmitToastProps) => {
  const { statusCode, onClose } = { ...props };
  const [description] = useState<string>(() =>
    getDescriptionFromStatusCode(statusCode),
  );
  const [success] = useState<boolean>(() => isSuccess(statusCode));
  const [className] = useState<string>(
    success ? "text-success" : "text-danger",
  );
  const [heading] = useState<string>(success ? "Success!" : "Error!");

  return (
    <Toast onClose={onClose}>
      <Toast.Header>
        <strong className={`me-auto ${className}`}>{heading}</strong>
        <SmallTimeAgo />
      </Toast.Header>
      <Toast.Body>{description}</Toast.Body>
    </Toast>
  );
};

export default SubmitToast;
