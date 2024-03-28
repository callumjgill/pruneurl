import { Button } from "react-bootstrap";
import ButtonSpinner from "./ButtonSpinner";

interface SubmitUrlButtonProps {
  submitting: boolean;
}

const SubmitUrlButton = (props: SubmitUrlButtonProps) => {
  const { submitting } = { ...props };

  return (
    <Button className="submit-button" type="submit" disabled={submitting}>
      {submitting ?
        <ButtonSpinner />
      : "Submit"}
    </Button>
  );
};

export default SubmitUrlButton;
