import { FormEvent, useEffect, useRef, useState } from "react";
import { Collapse, Form } from "react-bootstrap";
import UrlFormControl from "./FormControls/UrlFormControl";
import SubmitUrlButton from "./buttons/SubmitUrlButton";
import SubmitToastContainer from "./toasts/SubmitToastContainer";
import GeneratedUrlFormControl from "./FormControls/GeneratedUrlFormControl";
import getAPI from "../middleware/API/getAPI";
import API from "../middleware/API/API";
import PruneUrlResult from "../middleware/API/DTOs/PruneUrlResult";

const domain = window.location.host;
const longUrlControlId = `${domain}-LongURL`;
const generatedUrlControlId = `${domain}-PrunedUrl`;

const PruneUrlForm = () => {
  const api = useRef<API>(getAPI());

  const [longUrl, setLongUrl] = useState<string>("");
  const [validated, setValidated] = useState<boolean>(false);
  const [submitting, setSubmitting] = useState<boolean>(false);
  const [submitted, setSubmitted] = useState<boolean>(false);
  const [latestStatusCode, setLatestStatusCode] = useState<number | undefined>(
    undefined,
  );
  const [generatedUrl, setGeneratedUrl] = useState<string | undefined>(
    undefined,
  );

  const handleSubmit = (event: FormEvent<HTMLFormElement>): void => {
    event.preventDefault();
    const form: HTMLFormElement = event.currentTarget;
    const formIsValid: boolean = form.checkValidity();
    if (!form.checkValidity()) {
      event.stopPropagation();
    }

    setValidated(true);
    setSubmitting(formIsValid);
    if (!formIsValid) {
      return;
    }

    const longUrlElement: HTMLInputElement | undefined =
      form.elements.namedItem(longUrlControlId) as HTMLInputElement;
    if (longUrlElement === undefined) {
      throw new Error("Could not find the long url element!");
    }

    const longUrlValue: string = longUrlElement.value;
    setLongUrl(longUrlValue);
  };

  const handleApiResult = (result: PruneUrlResult): void => {
    setSubmitting(false);
    const statusCode: number = result.error === undefined ? 200 : result.error;
    setLatestStatusCode(statusCode);
    if (result.error !== undefined) {
      return;
    }

    setSubmitted(true);
    setGeneratedUrl(`${domain}/${result.prunedUrl}`);
  };

  useEffect(() => {
    if (submitting) {
      api.current.pruneUrl(longUrl).then(handleApiResult);
    }
  }, [longUrl, submitting]);

  return (
    <>
      <Form noValidate validated={validated} onSubmit={handleSubmit}>
        <h1 className="text-center mb-4">{domain}</h1>
        <UrlFormControl controlId={longUrlControlId} />
        <SubmitUrlButton submitting={submitting} />
        <Collapse in={submitted} timeout={5000} mountOnEnter>
          <div>
            <GeneratedUrlFormControl
              generatedUrl={generatedUrl}
              controlId={generatedUrlControlId}
            />
          </div>
        </Collapse>
      </Form>
      <SubmitToastContainer
        latestStatusCode={latestStatusCode}
        onLatestStatusCodeAddedCallback={() => setLatestStatusCode(undefined)}
      />
    </>
  );
};

export default PruneUrlForm;
