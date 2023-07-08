import { FormEvent, useEffect, useState } from "react";
import { Form, InputGroup } from "react-bootstrap";
import InvalidUrlFeedback from "./InvalidUrlFeedback";
import UrlFormControl from "./UrlFormControl";
import FormRow from "./FormRow";
import SubmitUrlButton from "./buttons/SubmitUrlButton";

const domain = window.location.host;

const simulateSubmittingUrlToBackend = () => {
  return new Promise((resolve) => setTimeout(resolve, 2000));
};

const PruneUrlForm = () => {
  const [validated, setValidated] = useState<boolean>(false);
  const [submitting, setSubmitting] = useState<boolean>(false);

  const handleSubmit = (event: FormEvent<HTMLFormElement>): void => {
    event.preventDefault();
    const form = event.currentTarget;
    const formIsValid = form.checkValidity();
    if (!form.checkValidity()) {
      event.stopPropagation();
    }

    setValidated(true);
    setSubmitting(formIsValid);
  };

  useEffect(() => {
    if (submitting) {
      simulateSubmittingUrlToBackend().then(() => {
        setSubmitting(false);
      });
    }
  }, [submitting]);

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
      <SubmitUrlButton submitting={submitting} />
    </Form>
  );
};

export default PruneUrlForm;
