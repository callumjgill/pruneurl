import { Form, InputGroup } from "react-bootstrap";
import FormRow from "../FormRow";
import CopyToClipboardButton from "../buttons/CopyToClipboardButton";
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
    </FormRow>
  );
};

export default GeneratedUrlFormControl;
