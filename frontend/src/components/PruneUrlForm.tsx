import { FormEvent, useState } from "react";
import { Button, Form, InputGroup, Row } from "react-bootstrap";

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
      <Row className="mb-3">
        <Form.Group controlId={`${domain}-LongURL`}>
          <Form.Label>Long URL</Form.Label>
          <Form.Control
            required
            type="url"
            placeholder="https://www.example.com/Ah73764142rrvwxcqwed1r4r"
          />
          <Form.Control.Feedback type="invalid">
            Please enter a valid url!
          </Form.Control.Feedback>
        </Form.Group>
      </Row>
      <Row className="mb-3">
        <Form.Group controlId={`${domain}-YourPrunedURL`}>
          <Form.Label>Your Pruned URL</Form.Label>
          <InputGroup hasValidation>
            <InputGroup.Text>{domain}/</InputGroup.Text>
            <Form.Control type="url" placeholder="example (optional)" />
            <Form.Control.Feedback type="invalid">
              Please enter a valid url!
            </Form.Control.Feedback>
          </InputGroup>
        </Form.Group>
      </Row>
      <Button type="submit">Prune</Button>
    </Form>
  );
};

export default PruneUrlForm;
