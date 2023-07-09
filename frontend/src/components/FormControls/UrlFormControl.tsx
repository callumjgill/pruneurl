import { Form } from "react-bootstrap";
import { UrlFormControlProps } from "./UrlFormControlProps";
import InvalidUrlFeedback from "../InvalidUrlFeedback";
import FormRow from "../FormRow";

const UrlFormControl = (props: UrlFormControlProps) => {
  const { domain } = { ...props };
  return (
    <FormRow controlId={`${domain}-LongURL`}>
      <Form.Label>Input a long URL</Form.Label>
      <Form.Control
        required
        type="url"
        placeholder="https://www.example.com/Ah73764142rrvwxcqwed1r4r"
      />
      <InvalidUrlFeedback />
    </FormRow>
  );
};

export default UrlFormControl;
