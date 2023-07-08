import { FormEvent, useEffect, useState } from "react";
import { Form, InputGroup } from "react-bootstrap";
import InvalidUrlFeedback from "./InvalidUrlFeedback";
import UrlFormControl from "./FormControls/UrlFormControl";
import FormRow from "./FormRow";
import SubmitUrlButton from "./buttons/SubmitUrlButton";
import ShortUrlFormControl from "./FormControls/ShortUrlFormControl";
import CopyToClipboardButton from "./buttons/CopyToClipboardButton";
import SubmitToastContainer from "./toasts/SubmitToastContainer";

const domain = window.location.host;
const dummyPrunedUrl = `${domain}/abc`;

const simulateSubmittingUrlToBackend = () => {
  return new Promise((resolve) => setTimeout(resolve, 2000));
};

const PruneUrlForm = () => {
  const [validated, setValidated] = useState<boolean>(false);
  const [submitting, setSubmitting] = useState<boolean>(false);
  const [submitted, setSubmitted] = useState<boolean>(false);
  const [latestStatusCode, setLatestStatusCode] = useState<number | undefined>(
    undefined,
  );

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
        setSubmitted(true);
        setLatestStatusCode(200);
      });
    }
  }, [submitting]);

  return (
    <>
      <Form noValidate validated={validated} onSubmit={handleSubmit}>
        <h1 className="text-center mb-4">{domain}</h1>
        <FormRow controlId={`${domain}-LongURL`}>
          <Form.Label>Input a long URL</Form.Label>
          <UrlFormControl
            required
            placeholder="https://www.example.com/Ah73764142rrvwxcqwed1r4r"
          />
          <InvalidUrlFeedback />
        </FormRow>
        <FormRow controlId={`${domain}-YourPrunedURL`}>
          <Form.Label>Input a desired pruned URL</Form.Label>
          <InputGroup hasValidation>
            <InputGroup.Text>{domain}/</InputGroup.Text>
            <ShortUrlFormControl placeholder="example (optional)" />
            <InvalidUrlFeedback />
          </InputGroup>
        </FormRow>
        <SubmitUrlButton submitting={submitting} />
        {submitted && (
          <FormRow controlId={`${domain}-PrunedUrl`}>
            <Form.Label>Your generated pruned URL:</Form.Label>
            <InputGroup>
              <Form.Control
                readOnly
                value={dummyPrunedUrl}
                aria-describedby="generatedUrlBlock"
              />
              <CopyToClipboardButton text={dummyPrunedUrl} />
            </InputGroup>
            <Form.Text id="generatedUrlBlock" muted>
              This pruned url will be available to use for 30 days, after which
              it shall expire.
            </Form.Text>
          </FormRow>
        )}
      </Form>
      <SubmitToastContainer
        latestStatusCode={latestStatusCode}
        onLatestStatusCodeAddedCallback={() => setLatestStatusCode(undefined)}
      />
    </>
  );
};

export default PruneUrlForm;
