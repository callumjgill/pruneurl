import { Form, InputGroup } from "react-bootstrap";
import { UrlFormControlProps } from "./UrlFormControlProps";
import FormRow from "../FormRow";
import InvalidUrlFeedback from "../InvalidUrlFeedback";

const UrlFormControl = (props: UrlFormControlProps) => {
  const { domain } = { ...props };
  return (
    <FormRow controlId={`${domain}-YourPrunedURL`}>
      <Form.Label>Input a desired pruned URL</Form.Label>
      <InputGroup hasValidation>
        <InputGroup.Text>{domain}/</InputGroup.Text>
        <Form.Control type="text" placeholder="example (optional)" />
        <InvalidUrlFeedback />
      </InputGroup>
    </FormRow>
  );
};

export default UrlFormControl;
