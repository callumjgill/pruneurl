import { Button } from "react-bootstrap";
import ButtonSpinner from "./ButtonSpinner";

interface SubmitUrlButtonProps {
  submitting: boolean;
}

const SubmitUrlButton = (props: SubmitUrlButtonProps) => {
  const { submitting } = { ...props };

  return (
    <Button type="submit" disabled={submitting}>
      {submitting ? <ButtonSpinner /> : "Prune"}
    </Button>
  );
};

export default SubmitUrlButton;
