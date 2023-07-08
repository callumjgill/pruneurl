import { Toast } from "react-bootstrap";
import SmallTimeAgo from "../TimeAgo/SmallTimeAgo";
import { useEffect, useState } from "react";
import { getDescriptionFromStatusCode } from "../../utils/statusCodes";
import { SubmitToastProps } from "./SubmitToastProps";

interface SubmitFailedToastProps extends SubmitToastProps {
  errorStatusCode: number;
}

const SubmitFailedToast = (props: SubmitFailedToastProps) => {
  const { errorStatusCode, onClose } = { ...props };
  const [description, setDescription] = useState<string>(() =>
    getDescriptionFromStatusCode(errorStatusCode),
  );

  useEffect(() => {
    setDescription(getDescriptionFromStatusCode(errorStatusCode));
  }, [errorStatusCode]);

  return (
    <Toast onClose={onClose}>
      <Toast.Header>
        <strong className="me-auto text-danger">Error!</strong>
        <SmallTimeAgo />
      </Toast.Header>
      <Toast.Body>{description}</Toast.Body>
    </Toast>
  );
};

export default SubmitFailedToast;
