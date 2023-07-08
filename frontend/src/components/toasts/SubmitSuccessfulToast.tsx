import { Toast } from "react-bootstrap";
import SmallTimeAgo from "../TimeAgo/SmallTimeAgo";
import { SubmitToastProps } from "./SubmitToastProps";

const SubmitSuccessfulToast = (props: SubmitToastProps) => {
  const { onClose } = { ...props };
  return (
    <Toast onClose={onClose}>
      <Toast.Header>
        <strong className="me-auto text-success">Success!</strong>
        <SmallTimeAgo />
      </Toast.Header>
      <Toast.Body>Your URL has been pruned!</Toast.Body>
    </Toast>
  );
};

export default SubmitSuccessfulToast;
