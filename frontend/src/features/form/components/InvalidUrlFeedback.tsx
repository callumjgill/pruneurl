import { Form } from "react-bootstrap";

const InvalidUrlFeedback = () => {
  return (
    <Form.Control.Feedback type="invalid">
      Please enter a valid url!
    </Form.Control.Feedback>
  );
};

export default InvalidUrlFeedback;
