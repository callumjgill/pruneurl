import { FormEvent, useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import UrlFormControl from "./FormControls/UrlFormControl";
import SubmitUrlButton from "./buttons/SubmitUrlButton";
import ShortUrlFormControl from "./FormControls/ShortUrlFormControl";
import SubmitToastContainer from "./toasts/SubmitToastContainer";
import GeneratedUrlFormControl from "./FormControls/GeneratedUrlFormControl";

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
        <UrlFormControl domain={domain} />
        <ShortUrlFormControl domain={domain} />
        <SubmitUrlButton submitting={submitting} />
        {submitted && (
          <GeneratedUrlFormControl
            domain={domain}
            generatedUrl={dummyPrunedUrl}
          />
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
