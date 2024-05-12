import { FormEvent, useCallback, useEffect, useState } from "react";
import { Collapse, Form } from "react-bootstrap";
import UrlFormControl from "./controls/UrlFormControl";
import SubmitUrlButton from "./buttons/SubmitUrlButton";
import GeneratedUrlFormControl from "./controls/GeneratedUrlFormControl";
import { UrlResult } from "../../../middleware/API/DTOs";
import useApi from "../../../middleware/API/hooks/useApi";
import API from "../../../middleware/API/API";
import { NotificationActions, useNotificationStore } from "../../notifications";

const longUrlControlId = "LongURL";
const generatedUrlControlId = "ShortUrl";

const PruneUrlForm = () => {
  const { getApi } = useApi();

  const [longUrl, setLongUrl] = useState<string>("");
  const [validated, setValidated] = useState<boolean>(false);
  const [submitting, setSubmitting] = useState<boolean>(false);
  const [submitted, setSubmitted] = useState<boolean>(false);
  const { addSuccess, addError } = useNotificationStore(
    (actions: NotificationActions) => ({
      addSuccess: actions.addSuccess,
      addError: actions.addError,
    }),
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

  const handleApiResult = useCallback(
    (result: UrlResult): void => {
      setSubmitting(false);
      if (result.statusCode !== 201) {
        const error: string = result.error
          ? `Status Code: ${result.statusCode}; Message: ${result.error.message}`
          : "An unknown error occurred submitting the form!";
        addError(error);
        return;
      }

      addSuccess("Your shortend URL has been generated!");
      setSubmitted(true);
      setGeneratedUrl(result.shortUrl);
    },
    [addError, addSuccess],
  );

  useEffect(() => {
    if (!submitting) {
      return;
    }

    const api: API = getApi();
    api.pruneUrl(longUrl).then(handleApiResult);
  }, [getApi, handleApiResult, longUrl, submitting]);

  return (
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
  );
};

export default PruneUrlForm;
