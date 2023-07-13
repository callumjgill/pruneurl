import { Form, InputGroup } from "react-bootstrap";
import FormRow from "../FormRow";
import CopyToClipboardButton from "../buttons/CopyToClipboardButton";
import { InfoCircle } from "react-bootstrap-icons";
import { UrlFormControlProps } from "./UrlFormControlProps";

interface GeneratedUrlFormControlProps extends UrlFormControlProps {
  generatedUrl?: string;
}

const GeneratedUrlFormControl = (props: GeneratedUrlFormControlProps) => {
  const { controlId, generatedUrl } = { ...props };
  return (
    <FormRow controlId={controlId}>
      <Form.Label>Your generated pruned URL:</Form.Label>
      <InputGroup>
        <Form.Control
          readOnly
          value={generatedUrl}
          aria-describedby="generatedUrlBlock"
        />
        <CopyToClipboardButton text={generatedUrl ?? ""} />
      </InputGroup>
      <Form.Text id="generatedUrlBlock" muted>
        <div className="form-expiry-text-container">
          <InfoCircle />
          <span>This pruned url will expire in 30 days.</span>
        </div>
      </Form.Text>
    </FormRow>
  );
};

export default GeneratedUrlFormControl;
