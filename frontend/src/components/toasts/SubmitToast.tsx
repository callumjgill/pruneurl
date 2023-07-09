import { useState } from "react";
import { Fade, Toast } from "react-bootstrap";
import SmallTimeAgo from "../TimeAgo/SmallTimeAgo";
import {
  getDescriptionFromStatusCode,
  isSuccess,
} from "../../utils/statusCodes";

interface SubmitToastProps {
  className: string;
  statusCode: number;
  onCloseCallback: () => void;
}

const fadeDuration = 5000;

const SubmitToast = (props: SubmitToastProps) => {
  const { className, statusCode, onCloseCallback } = { ...props };

  const description = getDescriptionFromStatusCode(statusCode);
  const success = isSuccess(statusCode);
  const headingClassName = success ? "text-success" : "text-danger";
  const heading = success ? "Success!" : "Error!";

  const [show, setShow] = useState<boolean>(true);

  const handleOnClose = () => {
    setShow(false);
  };

  return (
    <Fade
      className={className}
      in={show}
      timeout={fadeDuration}
      appear
      onExited={onCloseCallback}
    >
      <div>
        <Toast show onClose={handleOnClose} animation={false}>
          <Toast.Header>
            <strong className={`me-auto ${headingClassName}`}>{heading}</strong>
            <SmallTimeAgo />
          </Toast.Header>
          <Toast.Body>{description}</Toast.Body>
        </Toast>
      </div>
    </Fade>
  );
};

export default SubmitToast;
