import { FormEvent, useState } from "react";
import { Button, Form, InputGroup } from "react-bootstrap";
import InvalidUrlFeedback from "./InvalidUrlFeedback";
import UrlFormControl from "./UrlFormControl";
import FormRow from "./FormRow";

const domain = window.location.host;

const PruneUrlForm = () => {
  const [validated, setValidated] = useState<boolean>(false);

  const handleSubmit = (event: FormEvent<HTMLFormElement>): void => {
    const form = event.currentTarget;
    if (!form.checkValidity()) {
      event.preventDefault();
      event.stopPropagation();
    }

    setValidated(true);
  };

  return (
    <Form noValidate validated={validated} onSubmit={handleSubmit}>
      <h1 className="text-center mb-4">{domain}</h1>
      <FormRow controlId={`${domain}-LongURL`}>
        <Form.Label>Long URL</Form.Label>
        <UrlFormControl
          required
          placeholder="https://www.example.com/Ah73764142rrvwxcqwed1r4r"
        />
        <InvalidUrlFeedback />
      </FormRow>
      <FormRow controlId={`${domain}-YourPrunedURL`}>
        <Form.Label>Your Pruned URL</Form.Label>
        <InputGroup hasValidation>
          <InputGroup.Text>{domain}/</InputGroup.Text>
          <UrlFormControl placeholder="example (optional)" />
          <InvalidUrlFeedback />
        </InputGroup>
      </FormRow>
      <Button type="submit">Prune</Button>
    </Form>
  );
};

export default PruneUrlForm;
