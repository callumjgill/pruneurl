import { Spinner } from "react-bootstrap";

const ButtonSpinner = () => {
  return (
    <Spinner animation="border" role="status" size="sm">
      <span className="visually-hidden">Loading...</span>
    </Spinner>
  );
};

export default ButtonSpinner;
