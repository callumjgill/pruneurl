import { FormEvent, useEffect, useState } from "react";
import { Collapse, Form } from "react-bootstrap";
import UrlFormControl from "./FormControls/UrlFormControl";
import SubmitUrlButton from "./buttons/SubmitUrlButton";
import SubmitToastContainer from "./toasts/SubmitToastContainer";
import GeneratedUrlFormControl from "./FormControls/GeneratedUrlFormControl";
import { UrlResult } from "../middleware/API/DTOs";
import useApi from "../middleware/API/hooks/useApi";
import API from "../middleware/API/API";

const longUrlControlId = "LongURL";
const generatedUrlControlId = "ShortUrl";

const PruneUrlForm = () => {
  const { getApi } = useApi();

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
    setValidated(true);
    setSubmitting(formIsValid);
    if (!formIsValid) {
      event.stopPropagation();
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

  const handleApiResult = (result: UrlResult): void => {
    setSubmitting(false);
    setLatestStatusCode(result.statusCode);
    if (result.error !== undefined) {
      return;
    }

    setSubmitted(true);
    setGeneratedUrl(result.shortUrl);
  };

  useEffect(() => {
    if (!submitting) {
      return;
    }

    const api: API = getApi();
    api.pruneUrl(longUrl).then(handleApiResult);
  }, [getApi, longUrl, submitting]);

  return (
    <>
      <Form noValidate validated={validated} onSubmit={handleSubmit}>
        <h1 className="text-center mb-4">PruneURL</h1>
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
